//------------------------------------------------------------------------------
//     此代码由T4模板自动生成
//	   生成时间 2017-09-22 11:49:04
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
using JFine.Code;
using JFine.Web.Base.MVC.Handler;

namespace JFine.Web.Areas.SysCommon.Controllers
{
    [HandlerIsSystem]
    /// <summary>
    /// Job_ScheduleController
    /// </summary>	
    public class JobScheduleController : JFControllerBase
    {
        private Job_ScheduleBLL job_ScheduleBll = new Job_ScheduleBLL();
        private ScheduleLogBLL scheduleLogBLL = new ScheduleLogBLL();

        #region View
        //
        // GET: /SysCommon/Organize/
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

        /// <summary>
        /// 日志列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize]
        public ActionResult LogIndex()
        {
            ViewBag.BindId = Request["bindid"];
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
                rows = job_ScheduleBll.GetPageList(pagination, queryJson),
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
            var data = job_ScheduleBll.GetForm(keyValue);
            return Content(data.ToJson());
        }


        /// <summary>
        /// 日志列表 
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetLogGridJson(Pagination pagination, string queryJson)
        {
            var data = new
            {
                rows = scheduleLogBLL.GetPageList(pagination, queryJson),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
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

            job_ScheduleBll.DeleteForm(keyValue);
            return Success("删除成功。");
        }

        /// <summary>
        /// 保存功能表单
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="Job_ScheduleEntity">功能实体</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult SaveForm(string keyValue, Job_ScheduleEntity job_ScheduleEntity)
        {
            job_ScheduleBll.SaveForm(keyValue, job_ScheduleEntity);
            return Success("保存成功。");
        }

        /// <summary>
        /// 重启任务
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult ResumeTask(string id)
        {
            AjaxResult ajaxResult = new AjaxResult();
            bool result = job_ScheduleBll.ResumeTask(id);
            if (result)
            {
                ajaxResult.state = ResultType.success.ToString();
                ajaxResult.message = "任务启动成功！";
            }
            else {
                ajaxResult.state = ResultType.error.ToString();
                ajaxResult.message = "任务启动失败！";
            }
            return Content(ajaxResult.ToJson());
        }

        /// <summary>
        /// 暂停任务
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult PauseTask(string id)
        {
            AjaxResult ajaxResult = new AjaxResult();
            bool result = job_ScheduleBll.PauseTask(id);
            if (result)
            {
                ajaxResult.state = ResultType.success.ToString();
                ajaxResult.message = "任务暂停成功！";
            }
            else
            {
                ajaxResult.state = ResultType.error.ToString();
                ajaxResult.message = "任务暂停失败！";
            }
            return Content(ajaxResult.ToJson());
        }

        #endregion

        #region 数据验证

        #endregion
    }
}