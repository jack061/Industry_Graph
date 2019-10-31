using JFine.Web.Base.MVC.Handler;
/********************************************************************************
**文 件 名:TestController
**命名空间:JFine.Plugins.Test.Areas.Test.Controllers
**描    述:
**
**版 本 号:V1.0.0.0
**创 建 人:superjoy
**创建日期:2018-06-25 13:35:24
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
using System.Web.Mvc;

namespace JFine.Plugins.Test.Areas.Test.Controllers
{
    [HandlerPlugin("Test")]
    public class TestController : JFControllerBase
    {
        // GET
        public override ActionResult Index()
        {
            ViewBag.title = "TEST TEST TEST";
            return
            View("~/Plugins/JFine.Test/Views/Test/Index.cshtml");
        }
        public ActionResult IndexM()
        {
            ViewBag.title = "首页";
            return
            View("~/Plugins/JFine.Test/Views/Test/IndexM.cshtml");
        }
    }
}