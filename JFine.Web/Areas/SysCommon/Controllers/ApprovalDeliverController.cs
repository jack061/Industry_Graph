
//------------------------------------------------------------------------------
//     此代码由T4模板自动生成
//	   生成时间 2018-01-27 09:59:57
//    ©为之团队
//------------------------------------------------------------------------------
using JFine.Busines.SysCommon;
using JFine.Common.UI;
using JFine.Common.Json;
using JFine.Domain.Models.SysCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using JFine.Code.Online;
using JFine.Web.Base.MVC.Handler;

namespace JFine.Web.Areas.SysCommon.Controllers
{
    /// <summary>
    /// ApprovalDeliverRecordController
    /// </summary>	
    public class ApprovalDeliverController : JFControllerBase
    {
        private ApprovalDeliverRecordBLL approvalDeliverRecordBll = new ApprovalDeliverRecordBLL();

        #region View
        //
        // GET: /SysCommon/
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
            ViewBag.Id = Request["keyValue"]??"";
            string BINDID = Request["BINDID"] ?? "";
            ViewBag.BINDID = BINDID;
            ViewBag.User = OnlineUser.Operator.GetCurrent();
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
                rows = approvalDeliverRecordBll.GetPageList(pagination, queryJson),
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
            var data = approvalDeliverRecordBll.GetForm(keyValue);
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
            approvalDeliverRecordBll.DeleteForm(keyValue);
            return Success("删除成功。");
        }

        /// <summary>
        /// 保存功能表单
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="ApprovalDeliverRecordEntity">功能实体</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult SaveForm(string keyValue, ApprovalDeliverRecordEntity approvalDeliverRecordEntity)
        {
            approvalDeliverRecordBll.SaveForm(keyValue, approvalDeliverRecordEntity);
            return Success("保存成功。");
        }
        #endregion

        #region 数据验证

        #endregion
    }
}