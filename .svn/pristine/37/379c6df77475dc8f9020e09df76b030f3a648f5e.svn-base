/********************************************************************************
**文 件 名:VMWWarningController
**命名空间:JFine.Plugins.VMW.Busines.VMW
**描    述:
**
**
**版 本 号:V1.0.0.0
**创 建 人:此代码由T4模板自动生成
**创建日期:2019-02-21 17:18:41
**修 改 人:
**修改日期:
**修改描述:
**
**
**版权所有: ©为之团队
*********************************************************************************/
using JFine.Plugins.VMW.Busines.VMW;
using JFine.Common.UI;
using JFine.Common.Json;
using JFine.Plugins.VMW.Domain.Models.VMW;
using JFine.Web.Base.MVC.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using JFine.Code.Online;
using JFine.Common.Data;
using Microsoft.AspNet.SignalR;
using JFine.Plugins.VMW.VMWHub;

namespace JFine.Plugins.VMW.Areas.VMW.Controllers
{
    [HandlerPlugin("VMW")]
	/// <summary>
	/// VMWWarningController
	/// </summary>	
	public class VMWWarningController:JFControllerBase
	{
		 private VMWWarningBLL vMWWarningBll = new VMWWarningBLL();

        #region View
        //
        // GET: /VMW/
        public override ActionResult Index()
        {
            return View("~/Plugins/JFine.VMW/Views/VMWWarning/Index.cshtml");
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
            return View("~/Plugins/JFine.VMW/Views/VMWWarning/Form.cshtml");
        }

        /// <summary>
        /// Form表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize]
        public  ActionResult Form2()
        {
            ViewBag.Id = Request["keyValue"];
            return View("~/Plugins/JFine.VMW/Views/VMWWarning/Form2.cshtml");
        }
        
        /// <summary>
        /// Form表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize]
        public ActionResult WarningForm()
        {
            ViewBag.Id = Request["keyValue"];
            return View("~/Plugins/JFine.VMW/Views/VMWWarning/WarningForm.cshtml");
        }

        /// <summary>
        /// Form表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize]
        public  ActionResult Details2()
        {
            ViewBag.Id = Request["keyValue"];
            return View("~/Plugins/JFine.VMW/Views/VMWWarning/Details2.cshtml");
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
                rows = vMWWarningBll.GetPageListBySql(pagination, queryJson),
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
        public ActionResult GetTopWarnList(int topN= 50)
        {
            var data = vMWWarningBll.GetTopWarnList(topN);
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
            var data = vMWWarningBll.GetForm(keyValue);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 获取预警统计数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetWarningStatData()
        {
            var data = vMWWarningBll.GetWarningStatData();
            return Content(data.ToJson());
        }

        #endregion

        #region 数据处理


        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Confirm(string keyValue)
        {
            var warning = vMWWarningBll.GetForm(keyValue);
            if(warning == null)
            {
                return Error("获取预警失败，请重新操作。");
            }
            if (!(VMWConstant.STATU_CONFIRM_UN.Equals(warning.ConfirmStatus))) 
            {
                return Error("该预警已经被确认，无需再次操作。");
            }
            OnlineUserModel onliner = OnlineUser.Operator.GetCurrent();
            VMWWarningEntity warnTemp = new VMWWarningEntity();
            warnTemp.Id = keyValue;
            warnTemp.ConfirmStatus = VMWConstant.STATU_CONFIRM_ED;
            warnTemp.ConfirmManCode = onliner.Account;
            warnTemp.ConfirmManName = onliner.UserName;
            warnTemp.ConfirmDate = DateTime.Now;

            vMWWarningBll.DealWithWarn(keyValue, warnTemp);

            //广播处理信息
            VMWCameraBLL vMWCameraBll = new VMWCameraBLL();
            var cameraInfo = vMWCameraBll.GetFormByCode(warning.CameraCode);
            GlobalHost.ConnectionManager.GetHubContext<WarnHub>().Clients.All.deal(keyValue, 0, cameraInfo.ToJson()); 

            return Success("确认成功。");
        }

        /// <summary>
        /// 忽略
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Ignore(string keyValue)
        {
            var warning = vMWWarningBll.GetForm(keyValue);
            if (warning == null)
            {
                return Error("获取预警失败，请重新操作。");
            }
            if (!(VMWConstant.STATU_CONFIRM_UN.Equals(warning.ConfirmStatus)))
            {
                return Error("该预警已经被确认，无需再次操作。");
            }
            OnlineUserModel onliner = OnlineUser.Operator.GetCurrent();
            VMWWarningEntity warnTemp = new VMWWarningEntity();
            warnTemp.Id = keyValue;
            warnTemp.ConfirmStatus = VMWConstant.STATU_CONFIRM_IGNORE;
            warnTemp.ConfirmManCode = onliner.Account;
            warnTemp.ConfirmManName = onliner.UserName;
            warnTemp.ConfirmDate = DateTime.Now;

            warnTemp.DealStatus = VMWConstant.STATU_DEAL_IGNORE;
            warnTemp.DealManCode = onliner.Account;
            warnTemp.DealManName = onliner.UserName;
            warnTemp.DealDate = DateTime.Now;

            vMWWarningBll.DealWithWarn(keyValue, warnTemp);

            //广播处理信息
            VMWCameraBLL vMWCameraBll = new VMWCameraBLL();
            var cameraInfo = vMWCameraBll.GetFormByCode(warning.CameraCode);
            GlobalHost.ConnectionManager.GetHubContext<WarnHub>().Clients.All.deal(keyValue, 2, cameraInfo.ToJson()); 

            return Success("已忽略。");
        }

        /// <summary>
        /// 处理预警 并闭环
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="VMWWarningEntity">功能实体</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult DealWarning(string keyValue, VMWWarningEntity vMWWarningEntity)
        {
            var warning = vMWWarningBll.GetForm(keyValue);
            if (warning == null)
            {
                return Error("获取预警信息失败，请重新操作。");
            }
            if (!(VMWConstant.STATU_DEAL_UN.Equals(warning.DealStatus)))
            {
                return Error("该预警已经被处理，无需再次操作。");
            }
            OnlineUserModel onliner = OnlineUser.Operator.GetCurrent();
            VMWWarningEntity warnTemp = new VMWWarningEntity();
            warnTemp.Id = keyValue;

            DateTime dt = DateTime.Now;
            if ((VMWConstant.STATU_CONFIRM_UN.Equals(warning.ConfirmStatus)))
            {
                warnTemp.ConfirmStatus = VMWConstant.STATU_CONFIRM_ED;
                warnTemp.ConfirmManCode = onliner.Account;
                warnTemp.ConfirmManName = onliner.UserName;
                warnTemp.ConfirmDate = dt;
            }
            warnTemp.DealStatus = VMWConstant.STATU_DEAL_ED;
            warnTemp.DealManCode = onliner.Account;
            warnTemp.DealManName = onliner.UserName;
            warnTemp.DealDate = dt;
            warnTemp.Result = vMWWarningEntity.Result;
            vMWWarningBll.DealWithWarn(keyValue, warnTemp);

            //广播处理信息
            VMWCameraBLL vMWCameraBll = new VMWCameraBLL();
            var cameraInfo = vMWCameraBll.GetFormByCode(vMWWarningEntity.CameraCode);
            GlobalHost.ConnectionManager.GetHubContext<WarnHub>().Clients.All.deal(keyValue, 1,cameraInfo.ToJson()); 
            return Success("该预警闭环处理成功。");
        }


        /// <summary>
        /// 保存功能表单
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="VMWWarningEntity">功能实体</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult SaveForm(string keyValue, VMWWarningEntity vMWWarningEntity)
        {
            if (string.IsNullOrWhiteSpace(keyValue)) 
            {
                vMWWarningEntity.ConfirmStatus = VMWConstant.STATU_CONFIRM_UN;
                vMWWarningEntity.DealStatus = VMWConstant.STATU_DEAL_UN;
            }
            vMWWarningBll.SaveForm(keyValue, vMWWarningEntity);

            //广播预警信息
            VMWCameraBLL vMWCameraBll = new VMWCameraBLL();
            var cameraInfo = vMWCameraBll.GetFormByCode(vMWWarningEntity.CameraCode);
            GlobalHost.ConnectionManager.GetHubContext<WarnHub>().Clients.All.warn(vMWWarningEntity.ToJson(), cameraInfo.ToJson()); 
            return Success("保存成功。");
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
            vMWWarningBll.DeleteForm(keyValue);
            return Success("删除成功。");
        }

        #endregion

    }
}

