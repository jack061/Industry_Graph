
using JFine.Busines.SystemManage;
using JFine.Code.Online;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JFine.Web.Base.MVC.Handler;

namespace JFine.Web.Areas.SysManage.Controllers
{
    [HandlerIsSystem]
    /// <summary>
    /// 服务器信息
    /// </summary>
    public class ServerInfoController : JFControllerBase
    {
        // GET: SysManage/ServerInfo
        public override ActionResult Index()
        {
            OnlineUserModel onliner = OnlineUser.Operator.GetCurrent();
            if (onliner.IsSystem)
            {
                var serverInfo = ServerInfo.getServerInfo();
                return View(serverInfo);
            }
            return Content("当前用户权限不足");
            
        }
    }
}