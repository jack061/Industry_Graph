/********************************************************************************
**文 件 名:VMWWarnTypeController
**命名空间:JFine.Plugins.VMW.Busines.VMW
**描    述:
**
**
**版 本 号:V1.0.0.0
**创 建 人:此代码由T4模板自动生成
**创建日期:2019-06-15 14:33:00
**修 改 人:
**修改日期:
**修改描述:
**
**
**版权所有: ©为之团队
*********************************************************************************/
using JFine.Plugins.VMW.Busines.VMW;
using JFine.Common.UI;
using JFine.Common.Json;
using JFine.Plugins.VMW.Domain.Models.VMW;
using JFine.Web.Base.MVC.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

namespace JFine.Plugins.VMW.Areas.VMW.Controllers
{	
	/// <summary>
	/// VMWWarnTypeController
	/// </summary>	
	public class VMWWarnTypeController:JFControllerBase
	{
		 private VMWWarnTypeBLL vMWWarnTypeBll = new VMWWarnTypeBLL();

        #region View
        //
        // GET: /VMW/
        public override ActionResult Index()
        {
            return View("~/Plugins/JFine.VMW/Views/VMWWarnType/Index.cshtml");
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
            return View("~/Plugins/JFine.VMW/Views/VMWWarnType/Form.cshtml");
        }

        /// <summary>
        /// WarnCamera
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize]
        public ActionResult WarnCamera()
        {
            return View("~/Plugins/JFine.VMW/Views/VMWWarnType/WarnCamera.cshtml");
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
            var data = vMWWarnTypeBll.GetList(queryJson).ToList();
            return Content(data.ToJson());
        }
        
        /// <summary>
        /// 场景下的摄像头 
        /// </summary>
        /// <param name="queryJson">查询字符串</param>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult WarnCameraList(string typeCode)
        {
            VMWCameraBLL cameraBLL = new VMWCameraBLL();
            var datalist = cameraBLL.GetListBySql(" and  UsageCode like '%" + typeCode + "%' ").ToList();
            var data = new
            {
                rows = datalist,
                total = datalist.Count,
                page = 1,
                records = datalist.Count
            };
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
                rows = vMWWarnTypeBll.GetPageList(pagination, queryJson),
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
            var data = vMWWarnTypeBll.GetForm(keyValue);
            return Content(data.ToJson());
        }
        #endregion

        #region 数据处理
        

        /// <summary>
        /// 保存功能表单
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="VMWWarnTypeEntity">功能实体</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult SaveForm(string keyValue, VMWWarnTypeEntity vMWWarnTypeEntity)
        {
            vMWWarnTypeBll.SaveForm(keyValue, vMWWarnTypeEntity);
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
            vMWWarnTypeBll.DeleteForm(keyValue);
            return Success("删除成功。");
        }

        #endregion

    }
}

