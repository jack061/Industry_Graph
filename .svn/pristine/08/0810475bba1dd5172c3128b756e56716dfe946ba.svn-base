using JFine.Plugins.VMW.Domain.Models.VMW;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
/********************************************************************************
**文 件 名:Class1
**命名空间:JFine.Plugins.VMW.VMWHub
**描    述:
**
**版 本 号:V1.0.0.0
**创 建 人:superjoy
**创建日期:2019-02-26 15:53:22
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

namespace JFine.Plugins.VMW.VMWHub
{
    [HubName("vmw")]
    public class WarnHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }

        /// <summary>
        /// 发送预警
        /// </summary>
        /// <param name="warnInfo"></param>
        public void warn(VMWWarningEntity warnInfo)
        {
            // 客户端调用.通知预警信息
            Clients.All.warn(warnInfo);

        }

    }
}
