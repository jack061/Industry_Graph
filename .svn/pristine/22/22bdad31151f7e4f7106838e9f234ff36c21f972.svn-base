using JFine.Busines.SysConfig;
using JFine.Domain.Models.SysConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JFine.Web.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 错误页面
        /// </summary>
        /// <param name="number">错误编码</param>
        /// <param name="title">标题</param>
        /// <param name="message">信息</param>
        /// <returns></returns>
        public ActionResult Error(string number, string title, string message)
        {
            ViewBag.number = number;
            ViewBag.title = title;
            ViewBag.message = message;
            return View();
        }

        /// <summary>
        /// 维护页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Maintenance()
        {
            SysConfigBLL sysConfigBLL = new SysConfigBLL();
            BaseSysConfigEntity sysConfig = sysConfigBLL.GetCurrentConfig();
            if (sysConfig != null)
            {
                ViewBag.message = sysConfig.MaintenanceMessage;
            }
            else 
            {
                 ViewBag.message = "系统正在维护中，请稍后访问。";
            }           
            return View();
        }

    }
}