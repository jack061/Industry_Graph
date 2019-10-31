/********************************************************************************
**文 件 名:ModbusHub
**命名空间:JFine.Plugins.IOT.IOTHubs
**描    述:
**
**版 本 号:V1.0.0.0
**创 建 人:superjoy
**创建日期:2019-06-26 15:48:25
**修 改 人:
**修改日期:
**修改描述:
**版权所有: ©为之团队
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JFine.Common.Json;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Collections;

namespace JFine.Plugins.IOT.IOTHubs
{
    [HubName("devicewarn")]
    public class WarnHub : Hub
    {
        IHubContext hub = GlobalHost.ConnectionManager.GetHubContext<WarnHub>();

        public void Hello()
        {
            Clients.All.hello();
        }

        /// <summary>
        /// 设备预警
        /// </summary>
        /// <param name="WarnJson">预警信息--json</param>
        public void DeviceWarn(String WarnJson)
        {
            // 客户端调用.返回数据
            hub.Clients.All.DeviceWarn(WarnJson);

        }

        /// <summary>
        /// 设备预警处理
        /// </summary>
        /// <param name="deviceCode"></param>
        /// <param name="flag"></param>
        public void DealWarn(String deviceCode,int flag)
        {
            // 客户端调用.返回数据
            hub.Clients.All.DealWarn(deviceCode, flag);

        }
    }
}