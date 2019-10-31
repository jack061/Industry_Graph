
//------------------------------------------------------------------------------
//     此代码由T4模板自动生成
//	   生成时间 2017-11-23 21:44:00
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
    /// WorkFlowTaskController
    /// </summary>	
    public class WorkFlowTaskController : JFControllerBase
    {
        private WF_TaskBLL workFlowTaskBll = new WF_TaskBLL();
        private WF_TaskRecordBLL workFlowTaskRecordBll = new WF_TaskRecordBLL();

        #region View
        //我的任务列表
        // GET: /WorkFlow/
        public override ActionResult Index()
        {
            return View();
        }
        
        //任务监控列表
        // GET: /WorkFlow/
        public ActionResult MonitorIndex()
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
        /// 正在进行中的任务
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string queryJson)
        {
            var data = new
            {
                rows = workFlowTaskBll.GetPageList(pagination, queryJson),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// 已经完成的任务
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJsonED(Pagination pagination, string queryJson)
        {
            var data = new
            {
                rows = workFlowTaskRecordBll.GetPageList(pagination, queryJson),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// 我的代办
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetMyGridJson(Pagination pagination, string queryJson)
        {
            var data = new
            {
                rows = workFlowTaskBll.GetMyPageList(pagination, queryJson),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// 我的已办
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetMyGridJsonED(Pagination pagination, string queryJson)
        {
            var data = new
            {
                rows = workFlowTaskRecordBll.GetMyPageList(pagination, queryJson),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// 获取任务列表（审批列表）(审批日志)
        /// </summary>
        /// <param name="Id">流程实例Id</param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTaskRecord(string Id)
        {
            var list = workFlowTaskRecordBll.GetTaskRecord(Id);
            var data = new
            {
                rows = list
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
            var data = workFlowTaskBll.GetForm(keyValue);
            return Content(data.ToJson());
        }
        #endregion

        #region 数据处理

        /// <summary>
        /// 提交任务
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="WorkFlowTaskEntity">功能实体</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult SubmitForm(WF_TaskEntity workFlowTaskEntity)
        {
            workFlowTaskBll.SubmitTask(workFlowTaskEntity);
            return Success("提交成功。");
        }

        /// <summary>
        /// 删除功能
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HandlerRole("System")]
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            workFlowTaskBll.DeleteForm(keyValue);
            return Success("删除成功。");
        }

        


        /// <summary>
        /// 挂起任务
        /// </summary>
        /// <param name="taskId">主键值</param>
        /// <returns></returns>
        [HandlerRole("System")]
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult SuspendTask(string taskId)
        {
            workFlowTaskBll.SuspendTask(taskId);
            return Success("挂起成功。");
        }

        /// <summary>
        /// 重启任务
        /// </summary>
        /// <param name="taskId">主键值</param>
        /// <returns></returns>
        /// 
        [HandlerRole("System")]
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult RestartTask(string taskId)
        {
            workFlowTaskBll.RestartTask(taskId);
            return Success("重启成功。");
        }
        
        /// <summary>
        /// 重启任务
        /// </summary>
        /// <param name="taskId">主键值</param>
        /// <returns></returns>
        /// 
        [HandlerRole("System")]
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult DeliverTask(string taskId, string receivercode, string receivername)
        {
            workFlowTaskBll.DeliverTask(taskId, receivercode, receivername);
            return Success("重启成功。");
        }

        #endregion

        #region 数据验证

        #endregion
    }
}