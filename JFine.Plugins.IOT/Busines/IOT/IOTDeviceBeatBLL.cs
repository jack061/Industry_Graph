
/********************************************************************************
**文 件 名:IOTDeviceBeatBLL
**命名空间:JFine.Plugins.IOT.Busines.IOT
**描    述:
**
**
**版 本 号:V1.0.0.0
**创 建 人:此代码由T4模板自动生成
**创建日期:2019-07-25 14:26:58
**修 改 人:
**修改日期:
**修改描述:
**
**
**版权所有: ©为之团队
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JFine.Plugins.IOT.Domain.Models.IOT;
using JFine.Plugins.IOT.Domain.IRepository.IOT;
using JFine.Plugins.IOT.Domain.Repository.IOT;
using JFine.Cache;
using JFine.Common.UI;
using JFine.Common.Extend;
using JFine.Common.Json;
using JFine.Data.Common;
using System.Linq.Expressions;
using System.Data.Common;
using System.Data;
using System.Threading;
using JFine.Domain.Models.SystemManage;
using JFine.Busines.SystemManage;
using JFine.Plugins.IOT.IOTHubs;

namespace JFine.Plugins.IOT.Busines.IOT
{	
	/// <summary>
	/// IOTDeviceBeatBLL
	/// </summary>	
	public class IOTDeviceBeatBLL
	{
	    private IIOTDeviceBeatRepository service=new IOTDeviceBeatRepository();
        //队列保存是否已启动
        private static bool flag = false;
        //节拍记录队列
        public static Queue<IOTDeviceBeatEntity> beatValQueue = new Queue<IOTDeviceBeatEntity>();
        //节拍Temp
        public static Dictionary<string, IOTDeviceBeatEntity> beatValTempList = new Dictionary<string, IOTDeviceBeatEntity>();

        private static ModbusHub modbusHub = new ModbusHub();
		

        #region 数据获取

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IOTDeviceBeatEntity> GetList()
        {
            return service.GetList();
        }

		/// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IOTDeviceBeatEntity> GetListBySql( string sqlWhere)
        {
			return service.GetListBySql(sqlWhere);
        }

		/// <summary>
        /// 列表--分页
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<IOTDeviceBeatEntity> GetPageListBySql(Pagination pagination, string queryJson)
        {
            var sqlWhere = new StringBuilder();
            var queryParam = queryJson.ToJObject();
			 List<DbParameter> parameter =  new List<DbParameter>();
            //查询条件
            if (!queryParam["keyword"].IsEmpty())
            {
                string keyword = queryParam["keyword"].ToString();
                sqlWhere.Append(" AND (Code like @keyword or Name like @keyword)");
				parameter.Add(DbParameters.CreateDbParameter("@keyword","%"+ keyword +"%",DbType.AnsiString));
            }
            
            return service.GetPageListBySql(pagination, sqlWhere.ToString(),parameter);
        }

		/// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IOTDeviceBeatEntity> GetList( string queryJson)
        {
             var expression = LinqExtensions.True<IOTDeviceBeatEntity>();
            var queryParam = queryJson.ToJObject();
            //查询条件
            if (!queryParam["DeviceCode"].IsEmpty())
            {
                string DeviceCode = queryParam["DeviceCode"].ToString();
                expression = expression.And(t => t.DeviceCode.Equals(DeviceCode));
            }
			return service.GetList(expression);
        }

        /// <summary>
        /// 列表--分页
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<IOTDeviceBeatEntity> GetPageList(Pagination pagination, string queryJson)
        {
			var expression = LinqExtensions.True<IOTDeviceBeatEntity>();
            var queryParam = queryJson.ToJObject();
             //查询条件
            if (!queryParam["keyword"].IsEmpty())
            {
                string keyord = queryParam["keyword"].ToString();
               // expression = expression.And(t => t.Name.Contains(keyord));
            }
            return service.GetPageList(pagination, expression);
        }

        /// <summary>
        /// 实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public IOTDeviceBeatEntity GetForm(string keyValue)
        {
            return service.GetForm(keyValue);
        }
        #endregion

        #region 数据处理

        

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, IOTDeviceBeatEntity iOTDeviceBeatEntity)
        {
            try
            {
                service.SaveForm(keyValue, iOTDeviceBeatEntity);
            }
            catch (Exception)
            {
                throw;
            }
        }

		/// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void DeleteForm(string keyValue)
        {
            try
            {
                service.DeleteForm(keyValue);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///  合模---处理节拍
        /// </summary>
        /// <param name="gatewayId"></param>
        /// <param name="deviceCode"></param>
        /// <param name="value"></param>
        public void MouldOff(string gatewayId,string deviceCode,string value,DateTime dt)
        {
            string startOff = "1";//合模开始
            int exitShot = 1;//存在射胶信号；
            try
            {
                if (startOff.Equals(value)) 
                {
                    if (beatValTempList.ContainsKey(deviceCode))
                    {
                        IOTDeviceBeatEntity beatVal = beatValTempList[deviceCode];
                        if (beatVal != null)
                        {
                            //DateTime dt = DateTime.Now;
                            
                            if (exitShot == beatVal.Shot)
                            {
                                beatVal.EndTime = dt;
                                TimeSpan ts = dt - (DateTime)beatVal.StartTime;
                                beatVal.IntervalTime = (decimal)ts.TotalSeconds;
                                beatValQueue.Enqueue(beatVal);
                                modbusHub.SendQuantitySignal(deviceCode,beatVal.IntervalTime.Value, dt);

                                //重新开始统计
                                IOTDeviceBeatEntity beatVal_temp = new IOTDeviceBeatEntity();
                                beatVal_temp.Id = Sequence.Sequence.getSnowflakeID();
                                beatVal_temp.BindId = gatewayId;
                                beatVal_temp.DeviceCode = deviceCode;
                                beatVal_temp.StartTime = dt;
                                beatVal_temp.CreateDate = DateTime.Now;
                                beatVal_temp.Shot = 0;
                                beatValTempList[deviceCode] = beatVal_temp;
                            }
                            else 
                            {
                                //不存在射胶信号，重新开始统计
                                beatVal.Id = Sequence.Sequence.getSnowflakeID();
                                beatVal.StartTime = dt;
                                beatVal.CreateDate = dt;
                                beatValTempList[deviceCode] = beatVal;
                            }
                            
                        }
                        //beatValTempList.Remove(deviceCode);
                    }
                    else 
                    {
                        //DateTime dt = DateTime.Now;
                        IOTDeviceBeatEntity beatVal = new IOTDeviceBeatEntity();
                        beatVal.Id = Sequence.Sequence.getSnowflakeID();
                        beatVal.BindId = gatewayId;
                        beatVal.DeviceCode = deviceCode;
                        beatVal.StartTime = dt;
                        beatVal.CreateDate = dt;
                        //beatValQueue.Enqueue(beatVal);
                        beatValTempList.Add(deviceCode, beatVal);
                    }
                    
                }
            }
            catch (Exception)
            {
                
            }
        }

        /// <summary>
        ///  射胶---处理节拍
        /// </summary>
        /// <param name="gatewayId"></param>
        /// <param name="deviceCode"></param>
        /// <param name="value"></param>
        public void Shot(string gatewayId, string deviceCode, string value)
        {
            string shot = "1";//射胶
            try
            {
                if (shot.Equals(value))
                {
                    if (beatValTempList.ContainsKey(deviceCode))
                    {
                        IOTDeviceBeatEntity beatVal = beatValTempList[deviceCode];
                        if (beatVal != null)
                        {
                            beatVal.Shot = 1;
                            beatValTempList[deviceCode] = beatVal;
                        }
                        
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        /// 检测数值队列，保存到数据库
        /// </summary>
        public void StartValQueue()
        {
            if (flag) 
            {
                return;
            }
            ThreadPool.QueueUserWorkItem((a) =>
            {
                StringBuilder sql = new StringBuilder();
                while (true)
                {
                    int queueCount = beatValQueue.Count;
                    if (queueCount > 0)
                    {
                        //List<beatValEntity> valList = new List<beatValEntity>();
                        sql.Clear();
                        int deCount = queueCount > 50 ? 50 : queueCount;
                        while (deCount > 0)
                        {
                            IOTDeviceBeatEntity val = beatValQueue.Dequeue();//出队
                            //valList.Add(val);
                            sql.Append("insert IOT_DeviceBeat(Id,BindId,DeviceCode,StartTime,EndTime,IntervalTime,Year,Month,DAY,Hour,CreateDate) VALUES('");
                            sql.Append(val.Id + "','");
                            sql.Append(val.BindId + "','");
                            sql.Append(val.DeviceCode + "','");
                            sql.Append(((DateTime)val.StartTime).ToString("yyyy-MM-dd HH:mm:ss.fff") + "','");
                            sql.Append(((DateTime)val.EndTime).ToString("yyyy-MM-dd HH:mm:ss.fff") + "','");
                            sql.Append(val.IntervalTime + "','");
                            sql.Append(((DateTime)val.StartTime).Year + "','");
                            sql.Append(((DateTime)val.StartTime).Month + "','");
                            sql.Append(((DateTime)val.StartTime).Day + "','");
                            sql.Append(((DateTime)val.StartTime).Hour + "','");
                            sql.Append(((DateTime)val.CreateDate).ToString("yyyy-MM-dd HH:mm:ss.fff") + "');");
                            deCount = deCount - 1;
                        }
                        //入库
                        SaveList(sql);

                    }
                    else
                    {
                        //如果队列中没有数据，休眠10秒.
                        Thread.Sleep(10000);
                    }
                }
            });

            flag = true;
        }

        public void SaveList(StringBuilder sql)
        {
            try
            {
                service.SaveList(sql);
            }
            catch (Exception e)
            {
                //记录日志
                LogEntity logEntity = new LogEntity();
                logEntity.Category = "节拍";
                logEntity.OperateType = "写数据";
                logEntity.CreateUserId = "";
                logEntity.CreateUserName = "System";
                logEntity.Module = "";
                logEntity.ModuleId = "";
                logEntity.ExecuteResult = 1;
                logEntity.Description = "写数据异常";
                logEntity.CreateDate = DateTime.Now;
                logEntity.IPAddress = "";
                logEntity.Host = "";
                logEntity.Browser = "";
                logEntity.SourceContentJson = e.Message;
                logEntity.Mark = "";
                logEntity.AddLog();
            }
        }

        #endregion

    }
}
