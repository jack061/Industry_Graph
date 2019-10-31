/********************************************************************************
**文 件 名:IOTProductController
**命名空间:JFine.Plugins.IOT.Busines.IOT
**描    述:
**
**
**版 本 号:V1.0.0.0
**创 建 人:此代码由T4模板自动生成
**创建日期:2019-08-06 13:28:38
**修 改 人:
**修改日期:
**修改描述:
**
**
**版权所有: ©为之团队
*********************************************************************************/
using JFine.Plugins.IOT.Busines.IOT;
using JFine.Common.UI;
using JFine.Common.Json;
using JFine.Plugins.IOT.Domain.Models.IOT;
using JFine.Web.Base.MVC.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

namespace JFine.Plugins.IOT.Areas.IOT.Controllers
{	
	/// <summary>
	/// IOTProductController
	/// </summary>	
	public class IOTProductController:JFControllerBase
	{
		 private IOTProductBLL iOTProductBll = new IOTProductBLL();

        #region View
        //
        // GET: /IOT/
        public override ActionResult Index()
        {
            return View("~/Plugins/JFine.IOT/Views/IOTProduct/Index.cshtml");
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
            return View("~/Plugins/JFine.IOT/Views/IOTProduct/Form.cshtml");
        }

        /// <summary>
        /// Details表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize]
        public override ActionResult Details()
        {
            ViewBag.Id = Request["keyValue"];
            return View("~/Plugins/JFine.IOT/Views/IOTProduct/Details.cshtml");
        }

        /// <summary>
        /// Details2表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize]
        public ActionResult Details2()
        {
            ViewBag.Id = Request["keyValue"];
            return View("~/Plugins/JFine.IOT/Views/IOTProduct/Details2.cshtml");
        }

        #endregion

        #region 数据获取


        /// <summary>
        /// 功能列表 
        /// </summary>
        /// <param name="queryJson">查询字符串</param>
        /// <returns>下拉选择Json</returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetSelectJson(string queryJson)
        {
            var data = iOTProductBll.GetList(queryJson).ToList();
            return Content(data.ToJson());
        }


		/// <summary>
        /// 列表 
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <returns></returns>
		[HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string queryJson)
        {
            var data = new
            {
                rows = iOTProductBll.GetPageList(pagination, queryJson),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// 功能实体 返回对象Json
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = iOTProductBll.GetForm(keyValue);
            return Content(data.ToJson());
        }
        #endregion

        #region 数据处理
        

        /// <summary>
        /// 保存功能表单
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="IOTProductEntity">功能实体</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult SaveForm(string keyValue, IOTProductEntity iOTProductEntity)
        {
            iOTProductBll.SaveForm(keyValue, iOTProductEntity);
            return Success("保存成功。");
        }

		/// <summary>
        /// 删除功能
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            iOTProductBll.DeleteForm(keyValue);
            return Success("删除成功。");
        }

        #endregion

    }
}

