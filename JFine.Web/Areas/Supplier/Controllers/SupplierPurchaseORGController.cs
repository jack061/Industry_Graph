
//------------------------------------------------------------------------------
//     此代码由T4模板自动生成
//	   生成时间 2018-03-06 11:32:47
//    ©为之团队
//------------------------------------------------------------------------------

using JFine.Common.UI;
using JFine.Common.Json;
using JFine.Domain.Models.Supplier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using JFine.Busines.Supplier;
using JFine.Util;
using JFine.Web.Base.MVC.Handler;

namespace JFine.Web.Areas.Supplier.Controllers
{
    /// <summary>
    /// SupplierPurchaseORGController
    /// </summary>	
    public class SupplierPurchaseORGController : JFControllerBase
    {
        private SupplierPurchaseORGBLL supplierPurchaseORGBll = new SupplierPurchaseORGBLL();

        #region View
        //
        // GET: /Supplier/
        public override ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Supplier/
        public ActionResult ApprovalIndex()
        {
            return View();
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
                rows = supplierPurchaseORGBll.GetPageList(pagination, queryJson),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
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
        public ActionResult GetApprovalList(Pagination pagination, string queryJson)
        {
            var data = new
            {
                rows = supplierPurchaseORGBll.GetPageList(pagination, queryJson),
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
            var data = supplierPurchaseORGBll.GetForm(keyValue);
            return Content(data.ToJson());
        }
        #endregion

        #region 数据处理
        

        /// <summary>
        /// 保存功能表单
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="SupplierPurchaseORGEntity">功能实体</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult SaveForm(string keyValue, SupplierPurchaseORGEntity supplierPurchaseORGEntity)
        {
            supplierPurchaseORGBll.SaveForm(keyValue, supplierPurchaseORGEntity);
            return Success("保存成功。");
        }
        /// <summary>
        /// 保存功能表单
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="SupplierPurchaseORGEntity">功能实体</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Approval(int flag,SupplierPurchaseORGEntity supplierPurchaseORGEntity)
        {
            if (flag == 0) 
            {
                supplierPurchaseORGEntity.ApprovalStatus = ConstantUtil.STATU_AUDIT_NO;
            }
            if (flag == 1)
            {
                supplierPurchaseORGEntity.ApprovalStatus = ConstantUtil.STATU_AUDIT_YES;
            }
            supplierPurchaseORGBll.Approval(supplierPurchaseORGEntity);
            return Success("审核成功。");
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
            supplierPurchaseORGBll.DeleteForm(keyValue);
            return Success("删除成功。");
        }

        #endregion

        #region 数据验证

        #endregion
    }
}