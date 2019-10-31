/********************************************************************************
**文 件 名:SysActionController
**命名空间:JFine.Busines.SysManage
**描    述:
**
**
**版 本 号:V1.0.0.0
**创 建 人:此代码由T4模板自动生成
**创建日期:2019-06-17 18:08:54
**修 改 人:
**修改日期:
**修改描述:
**
**
**版权所有: ©为之团队
*********************************************************************************/
using JFine.Busines.SysManage;
using JFine.Common.UI;
using JFine.Common.Json;
using JFine.Domain.Models.SysManage;
using JFine.Web.Base.MVC.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Transactions;

namespace JFine.Web.Areas.SysManage.Controllers
{
    [HandlerVistLog]
    [HandlerIsSystem]
	/// <summary>
	/// SysActionController
	/// </summary>	
	public class SysActionController:JFControllerBase
	{
		 private SysActionBLL sysActionBll = new SysActionBLL();

        #region View
        //
        // GET: /SysManage/
        public override ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Form表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public override ActionResult Form()
        {
            ViewBag.Id = Request["keyValue"];
            return View();
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
                rows = sysActionBll.GetPageList(pagination, queryJson),
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
            var data = sysActionBll.GetForm(keyValue);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 同步Action
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SyncAction()
        {
            var route = System.Web.Routing.RouteTable.Routes;
            int count = route.Count;
            ///TODO:同步项目内的ACTION;(未实现)
            
            return Success("同步完成。");
        }
        #endregion

        #region 数据处理
        

        /// <summary>
        /// 保存功能表单
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="SysActionEntity">功能实体</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult SaveForm(string keyValue, SysActionEntity sysActionEntity)
        {
            sysActionBll.SaveForm(keyValue, sysActionEntity);
            return Success("保存成功。");
        }

		/// <summary>
        /// 删除功能
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            SysActionRightBLL sysActionRightBll = new SysActionRightBLL();
            using (TransactionScope ts = new TransactionScope())
            {
                sysActionBll.DeleteForm(keyValue);
                sysActionRightBll.DeleteBindData(keyValue);
                ts.Complete();
            }
            sysActionRightBll.DelActionFromCache(keyValue);
            return Success("删除成功。");
        }

        #endregion

    }
}

