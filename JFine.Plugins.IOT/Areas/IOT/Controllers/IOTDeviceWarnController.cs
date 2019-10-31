/********************************************************************************
**文 件 名:IOTDeviceWarnController
**命名空间:JFine.Plugins.IOT.Busines.IOT
**描    述:
**
**
**版 本 号:V1.0.0.0
**创 建 人:此代码由T4模板自动生成
**创建日期:2019-07-01 17:21:25
**修 改 人:
**修改日期:
**修改描述:
**
**
**版权所有: ©为之团队
*********************************************************************************/
using JFine.Plugins.IOT.Busines.IOT;
using JFine.Common.UI;
using JFine.Common.Json;
using JFine.Plugins.IOT.Domain.Models.IOT;
using JFine.Web.Base.MVC.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using JFine.Code.Online;
using JFine.Plugins.IOT.IOTHubs;

namespace JFine.Plugins.IOT.Areas.IOT.Controllers
{
    [HandlerPlugin("IOT")]
	/// <summary>
	/// IOTDeviceWarnController
	/// </summary>	
	public class IOTDeviceWarnController:JFControllerBase
	{
		 private IOTDeviceWarnBLL iOTDeviceWarnBll = new IOTDeviceWarnBLL();

        #region View
        //
        // GET: /IOT/
        public override ActionResult Index()
        {
            return View("~/Plugins/JFine.IOT/Views/IOTDeviceWarn/Index.cshtml");
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
            return View("~/Plugins/JFine.IOT/Views/IOTDeviceWarn/Form.cshtml");
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
            return View("~/Plugins/JFine.IOT/Views/IOTDeviceWarn/WarningForm.cshtml");
        }

        /// <summary>
        /// Details表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize]
        public override ActionResult Details()
        {
            ViewBag.Id = Request["keyValue"];
            return View("~/Plugins/JFine.IOT/Views/IOTDeviceWarn/Details.cshtml");
        }

        /// <summary>
        /// Details2表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize]
        public ActionResult Details2()
        {
            ViewBag.Id = Request["keyValue"];
            return View("~/Plugins/JFine.IOT/Views/IOTDeviceWarn/Details2.cshtml");
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
                rows = iOTDeviceWarnBll.GetPageList(pagination, queryJson),
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
            var data = iOTDeviceWarnBll.GetForm(keyValue);
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
            var warning = iOTDeviceWarnBll.GetForm(keyValue);
            if (warning == null)
            {
                return Error("获取预警失败，请重新操作。");
            }
            if (!(IOTConstant.STATU_CONFIRM_UN.Equals(warning.ConfirmStatus)))
            {
                return Error("该预警已经被确认，无需再次操作。");
            }
            OnlineUserModel onliner = OnlineUser.Operator.GetCurrent();
            IOTDeviceWarnEntity warnTemp = new IOTDeviceWarnEntity();
            warnTemp.Id = keyValue;
            warnTemp.ConfirmStatus = IOTConstant.STATU_CONFIRM_ED;
            warnTemp.ConfirmManCode = onliner.Account;
            warnTemp.ConfirmManName = onliner.UserName;
            warnTemp.ConfirmDate = DateTime.Now;

            iOTDeviceWarnBll.DealWithWarn(keyValue, warnTemp);

            WarnHub warnHub = new WarnHub();
            //广播处理信息
            warnHub.DealWarn(warning.DeviceCode, 0);

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
            var warning = iOTDeviceWarnBll.GetForm(keyValue);
            if (warning == null)
            {
                return Error("获取预警失败，请重新操作。");
            }
            if (!(IOTConstant.STATU_CONFIRM_UN.Equals(warning.ConfirmStatus)))
            {
                return Error("该预警已经被确认，无需再次操作。");
            }
            OnlineUserModel onliner = OnlineUser.Operator.GetCurrent();
            IOTDeviceWarnEntity warnTemp = new IOTDeviceWarnEntity();
            warnTemp.Id = keyValue;
            warnTemp.ConfirmStatus = IOTConstant.STATU_CONFIRM_IGNORE;
            warnTemp.ConfirmManCode = onliner.Account;
            warnTemp.ConfirmManName = onliner.UserName;
            warnTemp.ConfirmDate = DateTime.Now;

            warnTemp.DealStatus = IOTConstant.STATU_DEAL_IGNORE;
            warnTemp.DealManCode = onliner.Account;
            warnTemp.DealManName = onliner.UserName;
            warnTemp.DealDate = DateTime.Now;

            iOTDeviceWarnBll.DealWithWarn(keyValue, warnTemp);

            //广播处理信息
            WarnHub warnHub = new WarnHub();
            warnHub.DealWarn(warning.DeviceCode, 2);

            return Success("已忽略。");
        }

        /// <summary>
        /// 处理预警 并闭环
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="IOTDeviceWarnEntity">功能实体</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult DealWarning(string keyValue, IOTDeviceWarnEntity iOTDeviceWarnEntity)
        {
            var warning = iOTDeviceWarnBll.GetForm(keyValue);
            if (warning == null)
            {
                return Error("获取预警信息失败，请重新操作。");
            }
            if (!(IOTConstant.STATU_DEAL_UN.Equals(warning.DealStatus)))
            {
                return Error("该预警已经被处理，无需再次操作。");
            }
            OnlineUserModel onliner = OnlineUser.Operator.GetCurrent();
            IOTDeviceWarnEntity warnTemp = new IOTDeviceWarnEntity();
            warnTemp.Id = keyValue;

            DateTime dt = DateTime.Now;
            if ((IOTConstant.STATU_CONFIRM_UN.Equals(warning.ConfirmStatus)))
            {
                warnTemp.ConfirmStatus = IOTConstant.STATU_CONFIRM_ED;
                warnTemp.ConfirmManCode = onliner.Account;
                warnTemp.ConfirmManName = onliner.UserName;
                warnTemp.ConfirmDate = dt;
            }
            warnTemp.DealStatus = IOTConstant.STATU_DEAL_ED;
            warnTemp.DealManCode = onliner.Account;
            warnTemp.DealManName = onliner.UserName;
            warnTemp.DealDate = dt;
            warnTemp.Result = iOTDeviceWarnEntity.Result;
            iOTDeviceWarnBll.DealWithWarn(keyValue, warnTemp);

            //广播处理信息
            WarnHub warnHub = new WarnHub();
            warnHub.DealWarn(warning.DeviceCode,1);

            return Success("该预警闭环处理成功。");
        }


        /// <summary>
        /// 保存功能表单
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="iOTDeviceWarnEntity">功能实体</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult SaveForm(string keyValue, IOTDeviceWarnEntity iOTDeviceWarnEntity)
        {
            if (string.IsNullOrWhiteSpace(keyValue))
            {
                iOTDeviceWarnEntity.ConfirmStatus = IOTConstant.STATU_CONFIRM_UN;
                iOTDeviceWarnEntity.DealStatus = IOTConstant.STATU_DEAL_UN;
            }
            iOTDeviceWarnEntity.WarningDate = DateTime.Now;
            iOTDeviceWarnBll.SaveForm(keyValue, iOTDeviceWarnEntity);

            //广播处理信息
            WarnHub warnHub = new WarnHub();
            warnHub.DeviceWarn(iOTDeviceWarnEntity.ToJson());

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
            iOTDeviceWarnBll.DeleteForm(keyValue);
            return Success("删除成功。");
        }

        #endregion

    }
}

