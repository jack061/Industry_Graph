/********************************************************************************
**文 件 名:MQTTDealDataBLL
**命名空间:JFine.Plugins.IOT.Busines.MQTT
**描    述:
**
**版 本 号:V1.0.0.0
**创 建 人:superjoy
**创建日期:2019-08-02 18:04:06
**修 改 人:
**修改日期:
**修改描述:
**版权所有: ©为之团队
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JFine.Plugins.IOT.Domain.Models.MQTT;
using JFine.Plugins.IOT.Busines.MQTT;
using JFine.Cache.Redis;
using JFine.Plugins.IOT.Domain.Models.IOT;
using JFine.Plugins.IOT.Busines.IOT;
using JFine.Domain.Models.SystemManage;
using System.Threading;

using JFine.Plugins.IOT.IOTHubs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using JFine.Common.Json;
using JFine.Common.Extend;
using JFine.Busines.SystemManage;
using JFine.Data.Repository;
using Newtonsoft.Json.Converters;

namespace JFine.Plugins.IOT.Busines.MQTT
{
    public class MQTTDealDataBLL
    {
        private GatewayValBLL gatewayValBLL = new GatewayValBLL();
        //采集数值队列
        private static Queue<GatewayValEntity> gateWayValQueue = new Queue<GatewayValEntity>();
        private static ModbusHub modbusHub = new ModbusHub();

        //网关列表
        public static List<MQTTGatewayEntity> gatewayList = new List<MQTTGatewayEntity>();
        //网关Tag列表
        public static List<MQTTGatewayTagEntity> gatewayTagList = new List<MQTTGatewayTagEntity>();

        private static IOTDeviceBeatBLL iOTDeviceBeatBLL = new IOTDeviceBeatBLL();

        public void StartDeal() 
        {
            MQTTGatewayBLL mQTTGatewayBll = new MQTTGatewayBLL();
            MQTTGatewayTagBLL mQTTGatewayTagBll = new MQTTGatewayTagBLL();
            gatewayList = mQTTGatewayBll.GetList().ToList();
            gatewayTagList = mQTTGatewayTagBll.GetList().ToList();

            DealData();
            StartValQueue();
            iOTDeviceBeatBLL.StartValQueue();
        }

        /// <summary>
        /// 获取Redis数据并处理
        /// </summary>
        private void DealData()
        {

            ThreadPool.QueueUserWorkItem((a) =>
            {
                IsoDateTimeConverter timeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss.fff" };
                while (true)
                {
                    try
                    {
                        RedisHelper redisHelper = new RedisHelper(15);
                        var payloadString = redisHelper.ListLeftPop(IOTConstant.MQTT_GATEVALUE_KEY);
                        var payload = payloadString.ToJObject();
                        string ClientId = "";
                        if (payload["ClientId"].IsEmpty())
                        {
                            continue;
                        }
                        ClientId = payload["ClientId"].ToString();
                        var gateway = gatewayList.FirstOrDefault(t => t.ClientId.Equals(ClientId));
                        if (gateway == null)
                        {
                            continue;
                        }
                        List<MQTTGatewayTagEntity> tagList_temp = gatewayTagList.Where(t => t.BindId.Equals(gateway.Id)).ToList();

                        if (tagList_temp == null)
                        {
                            continue;
                        }

                        //查询条件
                        if (!payload["tags"].IsEmpty())
                        {
                            var tags = payload["tags"];
                            DateTime dt = DateTime.Now;
                            if (!payload["timestamp"].IsEmpty())
                            {
                                //var timestampStr = JsonConvert.SerializeObject(payload["timestamp"], timeConverter).Replace("\"","");
                                //DateTime timestamp = DateTime.Parse(timestampStr);
                                DateTime timestamp = DateTime.Parse(payload["timestamp"].ToString().Replace("\"", ""));
                                dt = timestamp;
                                //网关设备时间与当前差8个小时
                                //dt = dt.AddHours(8);
                            }
                            foreach (var tagEntity in tagList_temp)
                            {
                                if (!tags[tagEntity.ParameterCode].IsEmpty())
                                {
                                    if (tagEntity.ParameterType != null && IOTConstant.SWITCH_VALUE == tagEntity.ParameterType)
                                    {
                                        #region 开关量处理
                                        //ModbusHub modbusHub = new ModbusHub();
                                        if (ModbusHub.modbusData.ContainsKey(tagEntity.DeviceCode + "@_@" + tagEntity.ParameterCode))
                                        {
                                            string preValue = ModbusHub.modbusData[tagEntity.DeviceCode + "@_@" + tagEntity.ParameterCode].ToString();
                                            if (!(preValue.Equals(tags[tagEntity.ParameterCode].ToString())))
                                            {
                                                SaveDataAndNotice(tagEntity, gateway, tags, dt);
                                                SignalDeal(tagEntity, gateway, tags, dt);
                                            }
                                        }
                                        else
                                        {
                                            SaveDataAndNotice(tagEntity, gateway, tags, dt);
                                            SignalDeal(tagEntity, gateway, tags, dt);
                                        }
                                        #endregion


                                    }
                                    else if (tagEntity.ParameterType != null && IOTConstant.ANALOG_VALUE == tagEntity.ParameterType)
                                    {
                                        #region 模拟量
                                        SaveDataAndNotice(tagEntity, gateway, tags, dt);
                                        #endregion
                                    }


                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                        //记录日志
                        LogEntity logEntity = new LogEntity();
                        logEntity.Category = "服务";
                        logEntity.OperateType = "MQTT接收数据异常";
                        logEntity.CreateUserId = "MQTT";
                        logEntity.CreateUserName = "MQTT";
                        logEntity.Module = "MQTT";
                        logEntity.ModuleId = "";
                        logEntity.ExecuteResult = -1;
                        logEntity.Description = ex.Message;
                        logEntity.CreateDate = DateTime.Now;
                        logEntity.SourceContentJson = ex.StackTrace;
                        logEntity.Mark = "";

                        logEntity.AddLog();
                    }
                }
            });
        }

        /// <summary>
        /// 保存数据并发送数据到前端
        /// </summary>
        /// <param name="tagEntity"></param>
        /// <param name="gateway"></param>
        /// <param name="tags"></param>
        /// <param name="dt"></param>
        private static void SaveDataAndNotice(MQTTGatewayTagEntity tagEntity, MQTTGatewayEntity gateway, JToken tags, DateTime dt)
        {
            GatewayValEntity gatewayVal = new GatewayValEntity();
            gatewayVal.Id = Sequence.Sequence.getSnowflakeID();
            gatewayVal.BindId = gateway.Id;
            gatewayVal.DeviceCode = tagEntity.DeviceCode;
            gatewayVal.ParameterCode = tagEntity.ParameterCode;
            gatewayVal.ParameterName = tagEntity.ParameterName;
            gatewayVal.ParameterValue = tags[tagEntity.ParameterCode].ToString();
            gatewayVal.ParameterType = tagEntity.ParameterType;
            gatewayVal.ParameterCategory = tagEntity.ParameterCategory;
            gatewayVal.ParameterDate = dt;
            gatewayVal.CreateDate = DateTime.Now;
            gateWayValQueue.Enqueue(gatewayVal);
            ModbusHub.modbusData[gatewayVal.DeviceCode + "@_@" + gatewayVal.ParameterCode] = gatewayVal.ParameterValue;
            //redis 更新
            RedisHelper redisHelper = new RedisHelper(15);
            redisHelper.HashSet(IOTConstant.MQTT_CURRENTVALUE_KEY, gatewayVal.DeviceCode + "@_@" + gatewayVal.ParameterCode, gatewayVal.ParameterValue);

            modbusHub.SendDeviceData(gatewayVal.DeviceCode, gatewayVal.ParameterType.Value, gatewayVal.ParameterCategory.Value, gatewayVal.ParameterCode, gatewayVal.ParameterValue, dt);
        }

        /// <summary>
        /// 信号处理
        /// </summary>
        /// <param name="tagEntity">网关参数</param>
        /// <param name="gateway">网关数据</param>
        /// <param name="tags">信号数据</param>
        private static void SignalDeal(MQTTGatewayTagEntity tagEntity, MQTTGatewayEntity gateway, JToken tags, DateTime dt)
        {
            if (tagEntity.ParameterCategory != null)
            {
                switch (tagEntity.ParameterCategory)
                {
                    // 合模
                    case (int)IOTConstant.ParameterCategory.MouldOff:
                        iOTDeviceBeatBLL.MouldOff(gateway.Id, tagEntity.DeviceCode, tags[tagEntity.ParameterCode].ToString(), dt);
                        break;
                    //射胶
                    case (int)IOTConstant.ParameterCategory.Shot:
                        iOTDeviceBeatBLL.Shot(gateway.Id, tagEntity.DeviceCode, tags[tagEntity.ParameterCode].ToString());
                        break;
                    //马达开
                    case (int)IOTConstant.ParameterCategory.MotorOn:
                        IOTDeviceBLL iOTDeviceBLL = new IOTDeviceBLL();
                        iOTDeviceBLL.MotorOn(gateway.Id, tagEntity.DeviceCode, tags[tagEntity.ParameterCode].ToString());
                        break;
                    //马达关
                    case (int)IOTConstant.ParameterCategory.MotorOff:
                        IOTDeviceBLL iOTDeviceBLL_OFF = new IOTDeviceBLL();
                        iOTDeviceBLL_OFF.MotorOff(gateway.Id, tagEntity.DeviceCode, tags[tagEntity.ParameterCode].ToString());
                        break;
                    default:
                        break;


                }
            }
        }


        /// <summary>
        /// 检测数值队列，保存到数据库
        /// </summary>
        private void StartValQueue()
        {

            ThreadPool.QueueUserWorkItem((a) =>
            {
                StringBuilder sql = new StringBuilder();
                while (true)
                {
                    try
                    {
                        int queueCount = gateWayValQueue.Count;
                        if (queueCount > 0)
                        {
                            //List<GatewayValEntity> valList = new List<GatewayValEntity>();
                            sql.Clear();
                            int deCount = queueCount > 50 ? 50 : queueCount;
                            while (deCount > 0)
                            {
                                GatewayValEntity val = gateWayValQueue.Dequeue();//出队
                                //valList.Add(val);
                                sql.Append("insert Gateway_Values(Id,BindId,DeviceCode,ParameterCode,ParameterName,ParameterValue,ParameterType,ParameterCategory,ParameterDate,Year,Month,DAY,CreateDate) VALUES('");
                                sql.Append(val.Id + "','");
                                sql.Append(val.BindId + "','");
                                sql.Append(val.DeviceCode + "','");
                                sql.Append(val.ParameterCode + "','");
                                sql.Append(val.ParameterName + "','");
                                sql.Append(val.ParameterValue + "','");
                                sql.Append(val.ParameterType + "','");
                                sql.Append(val.ParameterCategory + "','");
                                sql.Append(((DateTime)val.ParameterDate).ToString("yyyy-MM-dd HH:mm:ss.fff") + "','");
                                sql.Append(((DateTime)val.ParameterDate).Year + "','");
                                sql.Append(((DateTime)val.ParameterDate).Month + "','");
                                sql.Append(((DateTime)val.ParameterDate).Day + "','");
                                sql.Append(((DateTime)val.CreateDate).ToString("yyyy-MM-dd HH:mm:ss.fff") + "');");
                                deCount = deCount - 1;
                            }
                            //入库
                            // gatewayValBLL.SaveList(valList);
                            gatewayValBLL.SaveList(sql);

                        }
                        else
                        {
                            //如果队列中没有数据，休眠10秒.
                            Thread.Sleep(10000);
                        }
                    }
                    catch (Exception ex)
                    {

                        //记录日志
                        LogEntity logEntity = new LogEntity();
                        logEntity.Category = "服务";
                        logEntity.OperateType = "MQTT插入数据异常";
                        logEntity.CreateUserId = "MQTT";
                        logEntity.CreateUserName = "MQTT";
                        logEntity.Module = "MQTT";
                        logEntity.ModuleId = "";
                        logEntity.ExecuteResult = -1;
                        logEntity.Description = ex.Message;
                        logEntity.CreateDate = DateTime.Now;
                        logEntity.SourceContentJson = ex.StackTrace;
                        logEntity.Mark = "";

                        logEntity.AddLog();
                    }
                }
            });
        }


        /// <summary>
        /// 保存网关
        /// </summary>
        /// <param name="id"></param>
        public void SaveGateWay2List(MQTTGatewayEntity mQTTGatewayEntity)
        {
            RedisHelper redisHelper = new RedisHelper(IOTConstant.MQTT_REDIS_DB_INDEX);
            var gateway = gatewayList.FirstOrDefault(t => t.Id.Equals(mQTTGatewayEntity.Id));
           
            if (gateway != null)
            {
                redisHelper.ListRemove(IOTConstant.MQTT_GATEWAY_KEY, RedisHelper.Serialize(gateway));

                gateway.ClientId = mQTTGatewayEntity.ClientId;
                gateway.UserName = mQTTGatewayEntity.UserName;
                gateway.PWD = mQTTGatewayEntity.PWD;
                gateway.Topic = mQTTGatewayEntity.Topic;
            }
            else
            {
                gateway = mQTTGatewayEntity;
                gatewayList.Add(mQTTGatewayEntity);
            }

            redisHelper.ListRightPush<MQTTGatewayEntity>(IOTConstant.MQTT_GATEWAY_KEY, gateway);
            NoticeMqttServerReload();
            
        }

        /// <summary>
        /// 删除网关
        /// </summary>
        /// <param name="id"></param>
        public void DeleteGateWayFromList(String id)
        {
            RedisHelper redisHelper = new RedisHelper(IOTConstant.MQTT_REDIS_DB_INDEX);
            var gateway = gatewayList.FirstOrDefault(t => t.Id.Equals(id));
            redisHelper.ListRemove(IOTConstant.MQTT_GATEWAY_KEY, RedisHelper.Serialize(gateway));

            var taglist = gatewayTagList.Where(t => t.BindId.Equals(id));
            foreach (var tag in taglist) 
            {
                redisHelper.ListRemove(IOTConstant.MQTT_GATEWAY_TAG_KEY, RedisHelper.Serialize(tag));
            }

            gatewayList.RemoveAll(t => t.Id.Equals(id));
            gatewayTagList.RemoveAll(t => t.BindId.Equals(id));
            NoticeMqttServerReload();
        }

        /// <summary>
        /// 保存Tag
        /// </summary>
        /// <param name="id"></param>
        public void SaveTag2List(MQTTGatewayTagEntity mQTTGatewayTagEntity)
        {
            RedisHelper redisHelper = new RedisHelper(IOTConstant.MQTT_REDIS_DB_INDEX);
            var tag = gatewayTagList.FirstOrDefault(t => t.Id.Equals(mQTTGatewayTagEntity.Id));
            if (tag != null)
            {
                redisHelper.ListRemove(IOTConstant.MQTT_GATEWAY_TAG_KEY, RedisHelper.Serialize(tag));
                tag.DeviceCode = mQTTGatewayTagEntity.DeviceCode;
                tag.ParameterCode = mQTTGatewayTagEntity.ParameterCode;
                tag.ParameterName = mQTTGatewayTagEntity.ParameterName;
                tag.MinValue = mQTTGatewayTagEntity.MinValue;
                tag.MaxValue = mQTTGatewayTagEntity.MaxValue;
                tag.warnFlag = mQTTGatewayTagEntity.warnFlag;
                tag.Sort = mQTTGatewayTagEntity.Sort;
            }
            else
            {
                tag = mQTTGatewayTagEntity;
                gatewayTagList.Add(mQTTGatewayTagEntity);
            }
            redisHelper.ListRightPush<MQTTGatewayTagEntity>(IOTConstant.MQTT_GATEWAY_TAG_KEY, tag);
            NoticeMqttServerReload();
        }

        /// <summary>
        /// 删除Tag
        /// </summary>
        /// <param name="id"></param>
        public void DeleteTagFromList(String id)
        {
            RedisHelper redisHelper = new RedisHelper(IOTConstant.MQTT_REDIS_DB_INDEX);
            var tag = gatewayTagList.FirstOrDefault(t => t.Id.Equals(id));
            redisHelper.ListRemove(IOTConstant.MQTT_GATEWAY_TAG_KEY, RedisHelper.Serialize(tag));
            gatewayTagList.RemoveAll(t => t.Id.Equals(id));
            NoticeMqttServerReload();
        }

        public void NoticeMqttServerReload() 
        {
            try
            {
                MQTTClientBLL MQTTClientBLL = new MQTTClientBLL();
                MQTTClientBLL.MQTTClientStart();
                MQTTClientBLL.messageQueue.Enqueue("{\"reload\":1}");
            }
            catch (Exception e)
            {
                
                
            }
            
        }

    }
}
