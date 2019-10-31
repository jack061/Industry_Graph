using System;
using System.Web.Mvc;
using   JFine.Web.Base.MVC.Handler;

namespace JFine.Plugins.Test.Controllers
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
    }
}