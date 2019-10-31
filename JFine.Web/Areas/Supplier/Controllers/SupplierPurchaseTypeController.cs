
//------------------------------------------------------------------------------
//     此代码由T4模板自动生成
//	   生成时间 2018-07-09 18:41:22
//    ©为之团队
//------------------------------------------------------------------------------
using JFine.Busines.Supplier;
using JFine.Common.UI;
using JFine.Common.Json;
using JFine.Domain.Models.Supplier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using JFine.Util;
using JFine.Web.Base.MVC.Handler;

namespace JFine.Web.Areas.Supplier.Controllers
{
    /// <summary>
    /// SupplierPurchaseTypeController
    /// </summary>	
    public class SupplierPurchaseTypeController : JFControllerBase
    {
        private SupplierPurchaseTypeBLL supplierPurchaseTypeBll = new SupplierPurchaseTypeBLL();

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
                rows = supplierPurchaseTypeBll.GetPageList(pagination, queryJson),
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
                rows = supplierPurchaseTypeBll.GetPageList(pagination, queryJson),
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
            var data = supplierPurchaseTypeBll.GetForm(keyValue);
            return Content(data.ToJson());
        }
        #endregion

        #region 数据处理


        /// <summary>
        /// 保存功能表单
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="SupplierPurchaseTypeEntity">功能实体</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult SaveForm(string keyValue, SupplierPurchaseTypeEntity supplierPurchaseTypeEntity)
        {
            supplierPurchaseTypeBll.SaveForm(keyValue, supplierPurchaseTypeEntity);
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
        public ActionResult Approval(int flag, SupplierPurchaseTypeEntity supplierPurchaseTypeEntity)
        {
            if (flag == 0)
            {
                supplierPurchaseTypeEntity.ApprovalStatus = ConstantUtil.STATU_AUDIT_NO;
            }
            if (flag == 1)
            {
                supplierPurchaseTypeEntity.ApprovalStatus = ConstantUtil.STATU_AUDIT_YES;
            }
            supplierPurchaseTypeBll.Approval(supplierPurchaseTypeEntity);
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
            supplierPurchaseTypeBll.DeleteForm(keyValue);
            return Success("删除成功。");
        }

        #endregion

    }
}