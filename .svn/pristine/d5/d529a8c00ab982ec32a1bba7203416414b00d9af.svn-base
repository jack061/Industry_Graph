/********************************************************************************
**文 件 名:VMWHomeController
**命名空间:JFine.Plugins.Busines.VMW
**描    述:
**
**
**版 本 号:V1.0.0.0
**创 建 人:此代码由T4模板自动生成
**创建日期:2019-02-19 09:50:01
**修 改 人:
**修改日期:
**修改描述:
**
**
**版权所有: ©为之团队
*********************************************************************************/
using JFine.Common.UI;
using JFine.Common.Json;
using JFine.Web.Base.MVC.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

namespace JFine.Plugins.VMW.Areas.VMW.Controllers
{
    [HandlerPlugin("VMW")]
	/// <summary>
	/// VMWHomeController
	/// </summary>	
	public class VMWHomeController:JFControllerBase
	{

        #region View
        //
        // GET: /VMW/
        public override ActionResult Index()
        {
            return View("~/Plugins/JFine.VMW/Views/VMWHome/Index.cshtml");
        }
        
        //
        // GET: /VMW/
        public ActionResult VideoIndex()
        {
            return View("~/Plugins/JFine.VMW/Views/VMWHome/VideoIndex.cshtml");
        }

        /// <summary>
        /// Form表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize]
        public override ActionResult Form()
        {
            ViewBag.Id = Request["keyValue"];
            return View();
        }

        #endregion

        #region 数据获取
        
        #endregion

        #region 数据处理

        
        #endregion

    }
}

