using JFine.Busines.Supplier;
using JFine.Code;
using JFine.Common.Json;
using JFine.Domain.Models.Supplier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JFine.Web.Base.MVC.Handler;

namespace JFine.Web.Areas.Portal.Controllers
{
    public class SupplierController : JFControllerBase2
    {
         private SupplierBaseBLL supplierBaseBll = new SupplierBaseBLL();

        #region 视图
        // GET: Portal/Supplier
        public ActionResult Index()
        {
            return View();
        }
        // GET: Portal/Supplier
        /// <summary>
        /// 供应商注册
        /// </summary>
        /// <returns></returns>
        public ActionResult Regist()
        {
            return View();
        }
        #endregion

        #region 数据获取

        #endregion

        #region 数据处理
        /// <summary>
        /// 注册供应商
        /// </summary>
        /// <param name="supplierBaseEntity">供应商基本信息</param>
        /// <param name="supplierLinkmanEntity">联系人信息</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult RegistSupplier(SupplierBaseEntity supplierBaseEntity, SupplierLinkmanEntity supplierLinkmanEntity)
        {

            AjaxResult ajaxResult = supplierBaseBll.RegistSupplier(supplierBaseEntity, supplierLinkmanEntity, Request);
            return Content(ajaxResult.ToJson());
        }
        #endregion
    }
}