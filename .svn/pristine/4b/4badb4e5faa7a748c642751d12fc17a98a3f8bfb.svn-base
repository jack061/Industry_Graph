using JFine.WorkFlow.Business;
using JFine.Domain.Models.WorkFlow;
using JFine.Web.Base.MVC.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JFine.Web.Areas.WorkFlow.Controllers
{
    public class WorkFlowExecuteController : JFControllerBase
    {
        #region View
        // GET: WorkFlow/WorkFlowExecute
        public override ActionResult Index()
        {
            return View();
        }

        //发起流程
        // GET: WorkFlow/WorkFlowExecute
        public ActionResult Start()
        {
            ViewBag.dataId = Request["dataId"] ?? "";//业务单据Id
            ViewBag.wfId = Request["wfId"] ?? "";//流程Id
            ViewBag.instanceId = Request["instanceId"] ?? "";//流程实例Id
            ViewBag.taskId = Request["taskId"] ?? "";//流程任务Id
            //获取节点
            WF_NodeBLL wfNodeBLL = new WF_NodeBLL();
            var wfNode = wfNodeBLL.GetFirstNode(ViewBag.wfId);
            if(wfNode == null)
            {//节点不存在
                return Content("流程信息不存在");
            }
            if (wfNode.FormURL.IndexOf("?") < 0)
            {
                ViewBag.FormURL = wfNode.FormURL + "?keyValue=" + ViewBag.dataId;
            }
            else 
            {
                ViewBag.FormURL = wfNode.FormURL + "&keyValue=" + ViewBag.dataId;
            }
            
            ViewBag.ApprovalTitle = wfNode.ApprovalTitle;

            return View();
        }


        //流程审批
        public ActionResult Approval()
        {
            ViewBag.dataId = Request["dataId"] ?? "";//业务单据Id
            ViewBag.taskId = Request["taskId"] ?? "";//流程任务Id
            //获取任务
            WF_TaskBLL workFlowTaskBll = new WF_TaskBLL();
            WF_TaskEntity taskEntity = workFlowTaskBll.GetForm(ViewBag.taskId);
            if (taskEntity == null)
            {//任务不存在
                return Content("流程任务已经不存在啦！"); ;
            }
            if (taskEntity.ReadTime == null) 
            {//更新阅读时间
                workFlowTaskBll.UpdateReadingTime(ViewBag.taskId);
            }
            ViewBag.instanceId = taskEntity.BindID;
            //获取节点
            WF_NodeBLL wfNodeBLL = new WF_NodeBLL();
            var wfNode = wfNodeBLL.GetForm(taskEntity.WFSID, taskEntity.WFID);
            if (wfNode == null)
            {//节点不存在
                return Content("获取当前节点信息失败，请重新操作。");
            }
            ViewBag.wfNode = wfNode;
            if (wfNode.FormURL.IndexOf("?") < 0)
            {
                ViewBag.FormURL = wfNode.FormURL + "?keyValue=" + ViewBag.dataId;
            }
            else
            {
                ViewBag.FormURL = wfNode.FormURL + "&keyValue=" + ViewBag.dataId;
            }
            ViewBag.ApprovalTitle = wfNode.ApprovalTitle;

            return View();
        }

        //流程审批查看
        public override ActionResult Details()
        {
            ViewBag.dataId = Request["dataId"] ?? "";//业务单据Id
            ViewBag.BindId = Request["bindId"] ?? "";//流程实例Id
            ViewBag.taskId = Request["taskId"] ?? "";//流程任务Id
            //获取任务
            WF_TaskRecordBLL workFlowTaskRecordBll = new WF_TaskRecordBLL();
            WF_TaskRecordEntity taskEntity = workFlowTaskRecordBll.GetForm(ViewBag.taskId);
            if (taskEntity == null)
            {//任务不存在
                return Content("流程任务已经不存在啦！"); ;
            }
            //获取节点
            WF_NodeBLL wfNodeBLL = new WF_NodeBLL();
            var wfNode = wfNodeBLL.GetForm(taskEntity.WFSID, taskEntity.WFID);
            if (wfNode == null)
            {//节点不存在
                return Content("获取当前节点信息失败，请重新操作。");
            }
            ViewBag.FormURL = wfNode.FormURL + "?keyValue=" + ViewBag.dataId;
            ViewBag.ApprovalTitle = wfNode.ApprovalTitle;

            return View();
        }

        #endregion

        
    }
}