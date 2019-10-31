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
    [HubName("orderBusHub")]
    public class OrderHub : Hub
    {
        IHubContext hub = GlobalHost.ConnectionManager.GetHubContext<OrderHub>();
      /// <summary>
      /// 刷新订单看板
      /// </summary>
        public void UpdateOrderBoard(string message)
        {
            // 客户端调用.返回数据
            hub.Clients.All.UpdateOrderBoard(message);

        }
    }
}
