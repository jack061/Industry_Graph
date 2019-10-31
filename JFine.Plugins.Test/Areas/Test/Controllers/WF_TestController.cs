/********************************************************************************
**文 件 名:WF_TestController
**命名空间:JFine.Busines.Test
**描    述:
**
**
**版 本 号:V1.0.0.0
**创 建 人:此代码由T4模板自动生成
**创建日期:2018-09-11 18:49:13
**修 改 人:
**修改日期:
**修改描述:
**
**
**版权所有: ©为之团队
*********************************************************************************/
using JFine.Busines.Test;
using JFine.Common.UI;
using JFine.Common.Json;
using JFine.Domain.Models.Test;
using JFine.Web.Base.MVC.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using JFine.WorkFlow.Business;

namespace JFine.Plugins.Test.Areas.Test.Controllers
{
    [HandlerPlugin("Test")]
	/// <summary>
	/// WF_TestController
	/// </summary>	
	public class WF_TestController:JFControllerBase
	{
		 private WF_TestBLL wF_TestBll = new WF_TestBLL();

        #region View
        //
        // GET: /Test/
        public override ActionResult Index()
        {
            return View("~/Plugins/JFine.Test/Views/WF_Test/Index.cshtml");
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
            return View("~/Plugins/JFine.Test/Views/WF_Test/Form.cshtml");
        }

        /// <summary>
        /// Form表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize]
        public ActionResult Form2()
        {
            ViewBag.Id = Request["keyValue"];
            return View("~/Plugins/JFine.Test/Views/WF_Test/Form2.cshtml");
        }

        /// <summary>
        /// Form表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize]
        public override ActionResult Details()
        {
            ViewBag.Id = Request["keyValue"];
            return View("~/Plugins/JFine.Test/Views/WF_Test/Details.cshtml");
        }

        #endregion

        #region 数据获取
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
                rows = wF_TestBll.GetPageList(pagination, queryJson),
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
            var data = wF_TestBll.GetForm(keyValue);
            return Content(data.ToJson());
        }
        #endregion

        #region 数据处理
        

        /// <summary>
        /// 保存功能表单
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="WF_TestEntity">功能实体</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult SaveForm(string keyValue, WF_TestEntity wF_TestEntity)
        {
           
            wF_TestBll.SaveForm(keyValue, wF_TestEntity);
            return Success("保存成功。", wF_TestEntity.Id);
        }

        /// <summary>
        /// 提交功能表单
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="WF_TestEntity">功能实体</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult SubmitForm(string keyValue, WF_TestEntity wF_TestEntity)
        {

            wF_TestBll.SubmitForm(keyValue, wF_TestEntity);
            return Success("保存成功。", wF_TestEntity.Id);
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
            wF_TestBll.DeleteForm(keyValue);
            return Success("删除成功。");
        }

        #endregion

    }
}

