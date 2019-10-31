
//------------------------------------------------------------------------------
//     此代码由T4模板自动生成
//	   生成时间 2018-03-06 11:18:35
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
using JFine.Web.Base.MVC.Handler;

namespace JFine.Web.Areas.Supplier.Controllers
{
    /// <summary>
    /// SupplierBaseController
    /// </summary>	
    public class SupplierBaseController : JFControllerBase
    {
        private SupplierBaseBLL supplierBaseBll = new SupplierBaseBLL();

        #region View
        //
        // GET: /Supplier/
        [HttpGet]
        [HandlerAuthorize]
        public override ActionResult Index()
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
            string operation = Request["operation"] ?? "";
            ViewBag.operation = operation;
            if (operation == "add") 
            {
                SupplierBaseEntity supplierBaseEntity = new SupplierBaseEntity();
                supplierBaseBll.SaveForm("", supplierBaseEntity);
                ViewBag.Id = supplierBaseEntity.Id;
            }
            return View();
        }
        
        /// <summary>
        /// Details表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize]
        public override ActionResult Details()
        {
            var keyValue = Request["keyValue"] ?? "";
            ViewBag.keyValue = keyValue;
            return View();
        }

        [HttpGet]
        [HandlerAuthorize]
        public ActionResult Details2()
        {
            var keyValue = Request["keyValue"] ?? "";
            ViewBag.keyValue = keyValue;
            return View();
        }
        
        [HttpGet]
        [HandlerAuthorize]
        public ActionResult ApprovalIndex()
        {
            return View();
        }
        
        [HttpGet]
        [HandlerAuthorize]
        public ActionResult ApprovalForm()
        {
            var keyValue = Request["keyValue"] ?? "";
            ViewBag.keyValue = keyValue;
            return View();
        }
        
        [HttpGet]
        [HandlerAuthorize]
        public ActionResult ChooseSupplier()
        {
            return View();
        }
        
        /// <summary>
        /// 供应商资源库
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize]
        public ActionResult ValidIndex()
        {
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
        public ActionResult GetGridJson(Pagination pagination, string queryJson,int flag = 0)
        {
            var data = new
            {
                rows = supplierBaseBll.GetPageListBySql(pagination, queryJson, flag),
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
            var data = supplierBaseBll.GetForm(keyValue);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 功能实体 返回对象Json
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetFormExtJson(string keyValue,string supno = "")
        {
            string queryJson = "";
            if (!string.IsNullOrWhiteSpace(supno))
            {
                queryJson = "{Id:\"" + keyValue + "\",SupNo:\"" + supno + "\",Status:\"通过\"}";
            }
            else 
            {
                queryJson = "{Id:\"" + keyValue + "\"}";
            }

            var data = supplierBaseBll.GetExtForm(queryJson);
            return Content(data.ToJson());
            
        }
        #endregion

        #region 数据处理
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
            supplierBaseBll.DeleteForm(keyValue);
            return Success("删除成功。");
        }

        /// <summary>
        /// 保存功能表单
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="SupplierBaseEntity">功能实体</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult SaveForm(string keyValue, SupplierBaseEntity supplierBaseEntity, SupplierLinkmanEntity supplierLinkmanEntity)
        {
            string LinkManId = (Request["LinkManId"] ?? "").Trim();
            supplierLinkmanEntity.Id = LinkManId;
            supplierLinkmanEntity.BindId = keyValue;
            supplierBaseBll.SaveForm(keyValue, supplierBaseEntity, supplierLinkmanEntity);
            return Success("保存成功。");
        }
        /// <summary>
        /// 保存功能表单
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="SupplierBaseEntity">功能实体</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult SaveForm2(string keyValue, SupplierBaseEntity supplierBaseEntity, SupplierLinkmanEntity supplierLinkmanEntity, List<SupplierFilesEntity> filedata, List<SupplierPurchaseORGEntity> purchasedata)
        {
            string LinkManId = (Request["LinkManId"] ?? "").Trim();
            supplierLinkmanEntity.Id = LinkManId;
            supplierLinkmanEntity.BindId = keyValue;
            supplierBaseBll.SaveForm(keyValue, supplierBaseEntity,supplierLinkmanEntity,filedata,purchasedata);
            return Success("保存成功。");
        }


        /// <summary>
        /// 保存功能表单
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="SupplierBaseEntity">功能实体</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult SaveApprovalForm(string keyValue, SupplierBaseEntity supplierBaseEntity)
        {
            supplierBaseBll.SaveApprovalForm(keyValue, supplierBaseEntity);
            return Success("保存成功。");
        }

        #endregion

        #region 数据验证

        #endregion
    }
}