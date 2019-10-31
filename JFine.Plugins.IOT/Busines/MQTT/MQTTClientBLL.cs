
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
using System.Threading;
using JFine.Plugins.IOT.IOTHubs;
using JFine.Domain.Models.SystemManage;
using JFine.Busines.SystemManage;
using MQTTnet;
using MQTTnet.Diagnostics;
using MQTTnet.Protocol;
using MQTTnet.Client;
using System.Threading.Tasks;
using System.Diagnostics;
using JFine.Plugins.IOT.Busines.IOT;
using JFine.Plugins.IOT.Domain.Models.IOT;
using JFine.Plugins.IOT.Domain.Models.MQTT;
using Newtonsoft.Json.Linq;
using JFine.Common.Config;

namespace JFine.Plugins.IOT.Busines.MQTT
{	
	/// <summary>
    /// ModbusBLL
	/// </summary>	
    public class MQTTClientBLL
	{
        //MQTT服务是否被启动过
        private static bool flag = false;
        //MqttClient
        private static MqttClient mqttClient = null;
        public static int ClientState = 0;//-1:启动异常；0:未启动；1：已启动；2：已停止
        //发送消息队列
        public static Queue<String> messageQueue = new Queue<String>();

        /// <summary>
        /// 启动MQTT服务
        /// </summary>
        public void MQTTClientStart()
        {

            if (ClientState == -1 || ClientState == 0) 
            {
                Task.Run(async () => { await StartMqttClient(); });
                PublishQueueMessage();
            }
            else if (ClientState == 2) 
            {
                Task.Run(async () => { await StartMqttClient(); });
            }
            
        }

        /// <summary>
        /// 停止MQTT服务
        /// </summary>
        public void MQTTClientStop()
        {

            if (ClientState == 1)
            {
                mqttClient.DisconnectAsync();
                ClientState = 2;
            }

        }

        private async Task StartMqttClient()
        {
             try
                {
                    var optionsBuilder = new MqttClientOptionsBuilder()
                                .WithClientId("Client_admin")                // clientid是设备id
                                .WithTcpServer(ConfigHelper.GetValue("MQTT_HOST"),int.Parse( ConfigHelper.GetValue("MQTT_PORT")))         
                                .WithCredentials("admin", "admin_admin")      
                                                        //.WithTls()//服务器端没有启用加密协议，这里用tls的会提示协议异常
                                .WithCleanSession(false)
                                .WithKeepAlivePeriod(TimeSpan.FromSeconds(2000)); 
                    if (mqttClient == null)
                    {
                            // Start a MQTT Client.
                            mqttClient = new MqttFactory().CreateMqttClient() as MqttClient;
                            mqttClient.ApplicationMessageReceived += MqttClient_ApplicationMessageReceived;
                            mqttClient.Connected += MqttClient_ClientConnected;
                            mqttClient.Disconnected += MqttClient_ClientDisconnected;

                            Task.Run(async () => { await mqttClient.ConnectAsync(optionsBuilder.Build()); });
                
                    }
                    else if (ClientState == 2)
                    {
                        Task.Run(async () => { await mqttClient.ConnectAsync(optionsBuilder.Build()); });
                    }
                }
             catch (Exception ex)
             {
                 ClientState = -1;
                 //记录日志
                 LogEntity logEntity = new LogEntity();
                 logEntity.Category = "服务";
                 logEntity.OperateType = "MQTT客户端连接异常";
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
                 return;
             }
            
             Trace.TraceInformation("MQTTClient连接成功！");
        }
        private static void MqttClient_ClientConnected(object sender, EventArgs e)
        {
            ClientState = 1;
            Trace.TraceInformation("MQTTClient连接成功！");
        }

        private static void MqttClient_ClientDisconnected(object sender, EventArgs e)
        {
            ClientState = 0;
            Trace.TraceInformation("MQTTClient连接断开！");
        }

        

        private static void MqttClient_ApplicationMessageReceived(object sender, MqttApplicationMessageReceivedEventArgs e)
        {
            try
            {
                var payloadString = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                var payload = payloadString.ToJObject();
                //查询条件
                if (!payload["tags"].IsEmpty())
                {
                    var tags = payload["tags"];
                    
                }
            }
            catch (Exception ex)
            {

                //记录日志
                LogEntity logEntity = new LogEntity();
                logEntity.Category = "服务";
                logEntity.OperateType = "MQTT Client接收数据异常";
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


        /// <summary>
        /// 检测数值队列，保存到数据库
        /// </summary>
        private void PublishQueueMessage()
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
                    int queueCount = messageQueue.Count;
                    if (queueCount > 0)
                    {
                        //List<beatValEntity> valList = new List<beatValEntity>();
                        sql.Clear();
                        int deCount = queueCount > 50 ? 50 : queueCount;
                        while (deCount > 0)
                        {
                           

                            if (ClientState == 1)
                            {
                                String message = messageQueue.Dequeue();//出队
                                mqttClient.PublishAsync(new MqttApplicationMessageBuilder().WithTopic("reload")
                                .WithPayload(message)
                                .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce)
                                .WithRetainFlag(false)
                                .Build());

                                deCount = deCount - 1;
                            }

                            
                        }

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
    }
}
