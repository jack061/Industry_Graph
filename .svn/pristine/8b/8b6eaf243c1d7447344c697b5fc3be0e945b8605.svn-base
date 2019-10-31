using JFine.Cache.Redis;
using JFine.Plugins.IOT.IOTHubs;
using Newtonsoft.Json.Converters;
using JFine.Common.Json;
using JFine.Common.Extend;
/********************************************************************************
**文 件 名:DealDataBLL
**命名空间:JFine.Plugins.IOT.Busines.HLD
**描    述:
**
**版 本 号:V1.0.0.0
**创 建 人:superjoy
**创建日期:2019-09-30 17:29:58
**修 改 人:
**修改日期:
**修改描述:
**版权所有: ©为之团队
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JFine.Domain.Models.SystemManage;
using JFine.Busines.SystemManage;
using JFine.Plugins.IOT.Busines.IOT;
using JFine.Plugins.IOT.Domain.Models.IOT;
using Newtonsoft.Json.Linq;

namespace JFine.Plugins.IOT.Busines.HLD
{
    public class DealDataBLL
    {
        private TCPGatewayValuesBLL gatewayValBLL = new TCPGatewayValuesBLL();
        private static IOT_Order_SecBLL order_SecBll = new IOT_Order_SecBLL();
        //采集数值队列
        private static Queue<TCPGatewayValuesEntity> gateWayValQueue = new Queue<TCPGatewayValuesEntity>();
        private static HLD_CheckHub checkHub = new HLD_CheckHub();

        //网关列表
       // public static List<MQTTGatewayEntity> gatewayList = new List<MQTTGatewayEntity>();

        public void StartDeal()
        {
            //MQTTGatewayBLL mQTTGatewayBll = new MQTTGatewayBLL();
           // gatewayList = mQTTGatewayBll.GetList().ToList();

            DealData();
            StartValQueue();
        }

        /// <summary>
        /// 获取Redis数据并处理
        /// </summary>
        private void DealData()
        {

            ThreadPool.QueueUserWorkItem((a) =>
            {
               // IsoDateTimeConverter timeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss.fff" };
                while (true)
                {
                    try
                    {
                        RedisHelper redisHelper = new RedisHelper(14);
                        var jsonString = redisHelper.ListLeftPop(IOTConstant.TCP_CURRENTVALUE_KEY);
                        if (!String.IsNullOrWhiteSpace(jsonString)) 
                        {
                            TCPGatewayValuesEntity valueEntity = JsonHelper.ToObject<TCPGatewayValuesEntity>(jsonString);
                            if (valueEntity != null)
                            {
                                SaveDataAndNotice(valueEntity);
                            }
                        }
                        
                    }
                    catch (Exception ex)
                    {

                        //记录日志
                        LogEntity logEntity = new LogEntity();
                        logEntity.Category = "服务";
                        logEntity.OperateType = "处理数据异常";
                        logEntity.CreateUserId = "HLD";
                        logEntity.CreateUserName = "HLD";
                        logEntity.Module = "HLD";
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
        private static void SaveDataAndNotice(TCPGatewayValuesEntity gatewayVale)
        {
            DateTime dt = DateTime.Now;
            //GatewayValEntity gatewayVal = new GatewayValEntity();
            gatewayVale.Id = Sequence.Sequence.getSnowflakeID();
            //gatewayVal.BindId = gateway.Id;

            gatewayVale.CreateDate = dt;
            gateWayValQueue.Enqueue(gatewayVale);
            checkHub.SendResult(gatewayVale.DeviceCode, gatewayVale.Value.Value, dt);
            //更新订单数量
            order_SecBll.UpdateOrderQuantity("", gatewayVale.Value.Value.ToString());
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
                                TCPGatewayValuesEntity val = gateWayValQueue.Dequeue();//出队
                                //valList.Add(val);
                                sql.Append("insert Tcp_Gateway_Values(Id,BindId,Ip,DeviceCode,ParameterValue,Value,ParameterDate,Year,Month,DAY,CreateDate) VALUES('");
                                sql.Append(val.Id + "','");
                                sql.Append(val.BindId + "','");
                                sql.Append(val.Ip + "','");
                                sql.Append(val.DeviceCode + "','");
                                sql.Append(val.ParameterValue + "','");
                                sql.Append(val.Value + "','");
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
                        logEntity.OperateType = "HLD插入数据异常";
                        logEntity.CreateUserId = "HLD";
                        logEntity.CreateUserName = "HLD";
                        logEntity.Module = "HLD";
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
        
    }
}
