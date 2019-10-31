
/********************************************************************************
**文 件 名:ModbusGatewayBLL
**命名空间:JFine.Plugins.IOT.Busines.Modbus
**描    述:
**
**
**版 本 号:V1.0.0.0
**创 建 人:此代码由T4模板自动生成
**创建日期:2019-06-24 17:55:26
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
using JFine.Plugins.IOT.Domain.Models.Modbus;
using JFine.Plugins.IOT.Domain.IRepository.Modbus;
using JFine.Plugins.IOT.Domain.Repository.Modbus;
using JFine.Cache;
using JFine.Common.UI;
using JFine.Common.Extend;
using JFine.Common.Json;
using JFine.Busines.SystemManage;
using JFine.Data.Common;
using System.Linq.Expressions;
using System.Data.Common;
using System.Data;
using System.Net;
using System.Net.Sockets;
using HslCommunication.ModBus;
using System.Threading;
using JFine.Plugins.IOT.IOTHubs;
using JFine.Domain.Models.SystemManage;
using JFine.Busines.SystemManage;
using JFine.Plugins.IOT.Busines.IOT;
using JFine.Plugins.IOT.Domain.Models.IOT;

namespace JFine.Plugins.IOT.Busines.Modbus
{	
	/// <summary>
    /// ModbusBLL
	/// </summary>	
    public class ModbusBLL
	{
        private GatewayValBLL gatewayValBLL = new GatewayValBLL();
        private static Queue<GatewayValEntity> gateWayValQueue = new Queue<GatewayValEntity>(); 
        public static List<ModbusGatewayEntity> gatewayList = new List<ModbusGatewayEntity>();
        public static List<ModbusGatewayParaEntity> gatewayParaList = new List<ModbusGatewayParaEntity>();
        private static Dictionary<string, Thread> threadList = new Dictionary<string, Thread>();
        private static Dictionary<string,ModbusTcpNet> modbusList = new Dictionary<string,ModbusTcpNet>();

        private static bool flag = false ; 
        
        public void Start_test()
        {
            ModbusTcpNet busTcpClient = new ModbusTcpNet("192.168.0.20", 502, 0);
            busTcpClient.ConnectServer();           
            HslCommunication.OperateResult<byte[]> read = busTcpClient.Read("x=4;0", 10);
            if (read.IsSuccess)
            {
                // 共返回20个字节，每个数据2个字节，高位在前，低位在后
                // 在数据解析前需要知道里面到底存了什么类型的数据，所以需要进行一些假设：
                // 前两个字节是short数据类型
                short value1 = busTcpClient.ByteTransform.TransInt16(read.Content, 0);
                // 接下来的2个字节是ushort类型
                ushort value2 = busTcpClient.ByteTransform.TransUInt16(read.Content, 2);
                // 接下来的4个字节是int类型
                int value3 = busTcpClient.ByteTransform.TransInt32(read.Content, 4);
                // 接下来的4个字节是float类型
                //float value4 = busTcpClient.ByteTransform.TransFloat(read.Content, 8);
                // 接下来的全部字节，共8个字节是规格信息
                string speci = Encoding.ASCII.GetString(read.Content, 12, 8);

                // 已经提取完所有的数据
            }

        }

        /// <summary>
        /// 一键启动
        /// </summary>
        public void OnekeyStart()
        {
            ModbusGatewayBLL modbusGatewayBLL = new ModbusGatewayBLL();
            ModbusGatewayParaBLL modbusGatewayParaBll = new ModbusGatewayParaBLL();
            List<ModbusGatewayEntity> gatewayList_temp = modbusGatewayBLL.GetList().ToList();
            gatewayParaList = modbusGatewayParaBll.GetList().ToList();
            foreach (var gateway in gatewayList_temp) 
            {
                var findList = gatewayList.Where(t => t.Id.Equals(gateway.Id)).ToList();
                if (!(findList.Count > 0))
                {
                    //创建ModbusClient
                    try
                    {
                        ModbusTcpNet busTcpClient = new ModbusTcpNet(gateway.Ip, (int)gateway.Port, 0);
                        busTcpClient.ConnectServer();
                        modbusList.Add(gateway.Id, busTcpClient);
                        gateway.Status = 0;
                        gatewayList.Add(gateway);
                    }
                    catch (Exception e)
                    {
                        gateway.Status = -1;
                        gatewayList.Add(gateway);
                        //记录日志
                        LogEntity logEntity = new LogEntity();
                        logEntity.Category = "Modbus";
                        logEntity.OperateType = "Modbus客户端创建";
                        logEntity.CreateUserId = "";
                        logEntity.CreateUserName = "Modbus";
                        logEntity.Module = "";
                        logEntity.ModuleId = "";
                        logEntity.ExecuteResult = -1;
                        logEntity.Description = "Modbus客户端创建异常";
                        logEntity.CreateDate = DateTime.Now;
                        logEntity.IPAddress = "";
                        logEntity.Host = "";
                        logEntity.Browser = "";
                        logEntity.SourceContentJson = e.Message;
                        logEntity.Mark = "";
                        logEntity.AddLog();
                    }
                    
                }
            }

            foreach (var gateway in gatewayList)
            {
                //3、创建线程
                if (!(threadList.ContainsKey(gateway.Id)) && gateway.Status == 0)
                {
                    ThreadStart threadStart = new ThreadStart(ReadData);
                    Thread t = new Thread(threadStart);
                    t.Name = gateway.Id;
                    t.IsBackground = true;
                    try
                    {
                        t.Start();
                        threadList.Add(gateway.Id, t);
                        gateway.Status = 1;
                    }
                    catch (Exception e)
                    {
                        //记录日志
                        LogEntity logEntity = new LogEntity();
                        logEntity.Category = "Modbus";
                        logEntity.OperateType = "Modbus线程启动";
                        logEntity.CreateUserId = "";
                        logEntity.CreateUserName = "Modbus";
                        logEntity.Module = "";
                        logEntity.ModuleId = "";
                        logEntity.ExecuteResult = -1;
                        logEntity.Description = "Modbus线程启动异常";
                        logEntity.CreateDate = DateTime.Now;
                        logEntity.IPAddress = "";
                        logEntity.Host = "";
                        logEntity.Browser = "";
                        logEntity.SourceContentJson = e.Message;
                        logEntity.Mark = "";
                        logEntity.AddLog();
                    }    
                }
            }

            if (!flag) 
            {
                StartValQueue();
                flag = true;
            }
        }

        /// <summary>
        /// 一键挂起
        /// </summary>
        public void OnekeySuspend()
        {
            foreach (var thread in threadList)
            {
                var th = thread.Value;
                ThreadState tState = th.ThreadState;
                if (tState == (ThreadState.Background | ThreadState.Running) || tState == ThreadState.Background || tState == (ThreadState.Background | ThreadState.WaitSleepJoin)) 
                {
                    try
                    {
                        th.Suspend();
                        var gateway = gatewayList.FirstOrDefault(t => t.Id.Equals(thread.Key));
                        if (gateway != null) 
                        {
                            gateway.Status = 2;
                        }
                    }
                    catch (Exception e)
                    {
                        
                       
                    }                   
                }
            }
        }
        /// <summary>
        /// 一键恢复
        /// </summary>
        public void OnekeyRecover()
        {
            foreach (var thread in threadList)
            {
                var th = thread.Value;
                ThreadState tState = th.ThreadState;
                if (th.ThreadState == (ThreadState.Background | ThreadState.Suspended))
                {
                    try
                    {
                        th.Resume();
                        var gateway = gatewayList.FirstOrDefault(t => t.Id.Equals(thread.Key));
                        if (gateway != null)
                        {
                            gateway.Status = 1;
                        }
                    }
                    catch (Exception e)
                    {


                    }
                    
                }
            }
        }

        /// <summary>
        /// 启动
        /// </summary>
        /// <param name="Id"></param>
        public void Start(string Id)
        {
            if (threadList.ContainsKey(Id))
            {
                var th = threadList[Id];
                ThreadState tState = th.ThreadState;
                if ( tState == ThreadState.Unstarted || tState == (ThreadState.Background | ThreadState.Unstarted))
                {
                    th.Start();
                }
                else 
                {
                    throw new Exception("当前线程不能做启动操作。");
                }
            }
            else 
            {
                throw new Exception("获取线程信息失败。");
            }
        }

        /// <summary>
        /// 挂起
        /// </summary>
        /// <param name="Id"></param>
        public void Suspend(string Id)
        {
            if (threadList.ContainsKey(Id)) 
            {
                var th = threadList[Id];
                ThreadState tState = th.ThreadState;
                if (tState == (ThreadState.Background | ThreadState.Running) || tState == ThreadState.Background || tState == (ThreadState.Background | ThreadState.WaitSleepJoin)) 
                {
                    th.Suspend();
                    var gateway = gatewayList.FirstOrDefault(t => t.Id.Equals(Id));
                    if (gateway != null)
                    {
                        gateway.Status = 2;
                    }
                }
            }
        }
        /// <summary>
        /// 恢复
        /// </summary>
        /// <param name="Id"></param>
        public void Recover(string Id)
        {
            if (threadList.ContainsKey(Id))
            {
                var th = threadList[Id];
                ThreadState tState = th.ThreadState;
                if (th.ThreadState == (ThreadState.Background | ThreadState.Suspended))
                {
                    th.Resume();
                    var gateway = gatewayList.FirstOrDefault(t => t.Id.Equals(Id));
                    if (gateway != null)
                    {
                        gateway.Status = 1;
                    }
                }
            }
        }

        private void ReadData()
        {
            string tId = Thread.CurrentThread.Name;
            ThreadState thState = Thread.CurrentThread.ThreadState;
            ModbusTcpNet busTcpClient = modbusList[tId];
            var gateway = gatewayList.FirstOrDefault(t => t.Id.Equals(tId));
            var gatewayPara = gatewayParaList.Where(t => t.BindId.Equals(tId)).ToList();
            gatewayPara = gatewayPara.OrderBy(t=>t.Sort).ToList();
            ModbusHub modbusHub = new ModbusHub();
            if (busTcpClient != null && gateway != null && gatewayPara != null)
            {
                while (true) 
                {
                    HslCommunication.OperateResult<byte[]> read = busTcpClient.Read("x=" + gateway.FunCode + ";" + gateway.StartAddr, (ushort)gateway.AddrLength);
                    if (read.IsSuccess)
                    {
                        // 共返回2*gateway.AddrLength个字节，每个数据2个字节，高位在前，低位在后
                        int index = 0;
                        DateTime dt = DateTime.Now;
                        for (var i = 0; i < gateway.AddrLength; i++)
                        {
                            try
                            {
                                // string value = busTcpClient.ByteTransform.TransString(read.Content, index, (int)gatewayPara[i].Databit, Encoding.UTF8);
                                ushort value = busTcpClient.ByteTransform.TransUInt16(read.Content, index);
                                index += (int)gatewayPara[i].Databit;
                                GatewayValEntity gatewayVal = new GatewayValEntity();
                                gatewayVal.Id = Sequence.Sequence.getSnowflakeID();
                                gatewayVal.BindId = gateway.Id;
                                gatewayVal.DeviceCode = gatewayPara[i].DeviceCode;
                                gatewayVal.ParameterType = gatewayPara[i].ParameterType;
                                gatewayVal.ParameterCategory = gatewayPara[i].ParameterCategory;
                                gatewayVal.ParameterCode = gatewayPara[i].ParameterCode;
                                gatewayVal.ParameterValue = value.ToString();
                                gatewayVal.CreateDate = dt;
                                gateWayValQueue.Enqueue(gatewayVal);
                                ModbusHub.modbusData[gatewayVal.DeviceCode + "@_@" + gatewayVal.ParameterCode] = gatewayVal.ParameterValue;
                                modbusHub.SendDeviceData(gatewayVal.DeviceCode, gatewayVal.ParameterType.Value, gatewayVal.ParameterCategory.Value, gatewayVal.ParameterCode, gatewayVal.ParameterValue, dt);
                            }
                            catch (Exception)
                            {


                            }

                        }

                    }                  
                    Thread.Sleep((int)(gateway.Period*1000));
                }
                
            }
        }

        /// <summary>
        /// 检测数值队列，保存到数据库
        /// </summary>
        public void StartValQueue()
        {
            
            ThreadPool.QueueUserWorkItem((a) =>
            {
                StringBuilder sql = new StringBuilder();
                while (true)
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
                            sql.Append("insert Gateway_Values(Id,BindId,DeviceCode,ParameterCode,ParameterName,ParameterValue,ParameterType,ParameterCategory,Year,Month,DAY,CreateDate) VALUES('");
                            sql.Append(val.Id + "','");
                            sql.Append(val.BindId + "','");
                            sql.Append(val.DeviceCode + "','");
                            sql.Append(val.ParameterCode + "','");
                            sql.Append(val.ParameterName + "','");
                            sql.Append(val.ParameterValue + "','");
                            sql.Append(val.ParameterType + "','");
                            sql.Append(val.ParameterCategory + "','");
                            sql.Append(((DateTime)val.CreateDate).Year + "','");
                            sql.Append(((DateTime)val.CreateDate).Month + "','");
                            sql.Append(((DateTime)val.CreateDate).Day + "','");
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
            });
        }
    }
}
