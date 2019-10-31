﻿/********************************************************************************
**文 件 名:ModbusGatewayValController
**命名空间:JFine.Plugins.IOT.Busines.Modbus
**描    述:
**
**
**版 本 号:V1.0.0.0
**创 建 人:此代码由T4模板自动生成
**创建日期:2019-06-24 17:58:34
**修 改 人:
**修改日期:
**修改描述:
**
**
**版权所有: ©为之团队
*********************************************************************************/
using JFine.Common.UI;
using JFine.Common.Json;
using JFine.Web.Base.MVC.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using JFine.Plugins.IOT.Busines.IOT;
using JFine.Plugins.IOT.Domain.Models.IOT;

namespace JFine.Plugins.IOT.Areas.IOT.Controllers
{
    [HandlerPlugin("IOT")]
	/// <summary>
	/// GatewayValController
	/// </summary>	
	public class GatewayValController:JFControllerBase
	{
		 private GatewayValBLL gatewayValBll = new GatewayValBLL();

        #region View
        //
        // GET: /Modbus/
        public override ActionResult Index()
        {
            return View("~/Plugins/JFine.IOT/Views/GatewayVal/Index.cshtml");
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
            return View("~/Plugins/JFine.IOT/Views/GatewayVal/Form.cshtml");
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
                rows = gatewayValBll.GetPageList(pagination, queryJson),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
        /// <summary>
        /// 获取最新一条数据
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetNewestData(Pagination pagination, string queryJson)
        {
            var data = gatewayValBll.GetNewestData(pagination, queryJson);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 获取历史数据
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetHistoryData(string queryJson)
        {
            var data = gatewayValBll.GetHistoryData(queryJson);
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
            var data = gatewayValBll.GetForm(keyValue);
            return Content(data.ToJson());
        }
        #endregion

        #region 数据处理


        /// <summary>
        /// 保存功能表单
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="GatewayValEntity">功能实体</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult SaveForm(string keyValue, GatewayValEntity modbusGatewayValEntity)
        {
            gatewayValBll.SaveForm(keyValue, modbusGatewayValEntity);
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
            gatewayValBll.DeleteForm(keyValue);
            return Success("删除成功。");
        }

        #endregion

    }
}

