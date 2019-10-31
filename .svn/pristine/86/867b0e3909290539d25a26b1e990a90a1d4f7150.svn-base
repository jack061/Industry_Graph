
//------------------------------------------------------------------------------
//     此代码由T4模板自动生成
//	   生成时间 2017-12-12 21:52:04
//    ©为之团队
//------------------------------------------------------------------------------
using JFine.WorkFlow.Business;
using JFine.Common.UI;
using JFine.Common.Json;
using JFine.Domain.Models.WorkFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using JFine.Web.Base.MVC.Handler;

namespace JFine.Web.Areas.WorkFlow.Controllers
{
    /// <summary>
    /// WorkFlowInstanceController
    /// </summary>	
    public class WorkFlowInstanceController : JFControllerBase
    {
        private WF_InstanceBLL workFlowInstanceBll = new WF_InstanceBLL();

        #region View
        //
        // GET: /WorkFlow/
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
                rows = workFlowInstanceBll.GetPageList(pagination, queryJson),
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
            var data = workFlowInstanceBll.GetForm(keyValue);
            return Content(data.ToJson());
        }
        #endregion

        #region 数据处理

        /// <summary>
        /// 保存功能表单
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="WorkFlowInstanceEntity">功能实体</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult SaveForm(string keyValue, WF_InstanceEntity workFlowInstanceEntity)
        {
            workFlowInstanceBll.SaveForm(keyValue, workFlowInstanceEntity);
            return Success("保存成功。");
        }


        /// <summary>
        /// 启动流程
        /// </summary>
        /// <param name="WorkFlowInstanceEntity">功能实体</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult StartWf( WF_InstanceEntity workFlowInstanceEntity)
        {
            workFlowInstanceBll.StartWf( workFlowInstanceEntity);
            return Success("流程发起成功。");
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
            workFlowInstanceBll.DeleteForm(keyValue);
            return Success("删除成功。");
        }

        
        #endregion

        #region 数据验证

        #endregion
    }
}