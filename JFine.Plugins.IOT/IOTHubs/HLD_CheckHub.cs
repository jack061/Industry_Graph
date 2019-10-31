using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
/********************************************************************************
**文 件 名:HLD_CheckHub
**命名空间:JFine.Plugins.IOT.IOTHubs
**描    述:
**
**版 本 号:V1.0.0.0
**创 建 人:superjoy
**创建日期:2019-09-30 17:33:01
**修 改 人:
**修改日期:
**修改描述:
**版权所有: ©为之团队
*********************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFine.Plugins.IOT.IOTHubs
{
    [HubName("checkbus")]
    public class HLD_CheckHub : Hub
    {
        public static Hashtable modbusData = new Hashtable();
        IHubContext hub = GlobalHost.ConnectionManager.GetHubContext<HLD_CheckHub>();

        public void Hello()
        {
            Clients.All.hello();
        }

        /// <summary>
        /// 发送结果
        /// </summary>
        /// <param name="deviceCode">设备编码</param>
        /// <param name="result">结果：0：不合格；1：合格；-1：没有检测数据</param>
        /// <param name="dataTime"></param>
        public void SendResult(string deviceCode, int result, DateTime dataTime)
        {
            // 客户端调用.返回数据
            hub.Clients.All.ReceiveResult(deviceCode, result, dataTime);

        }
    }
}
