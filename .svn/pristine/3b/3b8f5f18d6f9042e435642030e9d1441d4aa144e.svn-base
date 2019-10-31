/********************************************************************************
**文 件 名:ModbusGatewayParaController
**命名空间:JFine.Plugins.IOT.Busines.Modbus
**描    述:
**
**
**版 本 号:V1.0.0.0
**创 建 人:此代码由T4模板自动生成
**创建日期:2019-06-24 17:57:28
**修 改 人:
**修改日期:
**修改描述:
**
**
**版权所有: ©为之团队
*********************************************************************************/
using JFine.Plugins.IOT.Busines.Modbus;
using JFine.Common.UI;
using JFine.Common.Json;
using JFine.Plugins.IOT.Domain.Models.Modbus;
using JFine.Web.Base.MVC.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

namespace JFine.Plugins.IOT.Areas.Modbus.Controllers
{
    [HandlerPlugin("IOT")]
	/// <summary>
	/// ModbusGatewayParaController
	/// </summary>	
	public class ModbusGatewayParaController:JFControllerBase
	{
		 private ModbusGatewayParaBLL modbusGatewayParaBll = new ModbusGatewayParaBLL();

        #region View
        //
        // GET: /Modbus/
        public override ActionResult Index()
        {
            return View("~/Plugins/JFine.IOT/Views/ModbusGatewayPara/Index.cshtml");
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
            ViewBag.BindId = Request["bindid"] ?? "";
            return View("~/Plugins/JFine.IOT/Views/ModbusGatewayPara/Form.cshtml");
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
                rows = modbusGatewayParaBll.GetPageList(pagination, queryJson),
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
            var data = modbusGatewayParaBll.GetForm(keyValue);
            return Content(data.ToJson());
        }
        #endregion

        #region 数据处理
        

        /// <summary>
        /// 保存功能表单
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="ModbusGatewayParaEntity">功能实体</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult SaveForm(string keyValue, ModbusGatewayParaEntity modbusGatewayParaEntity)
        {
            modbusGatewayParaBll.SaveForm(keyValue, modbusGatewayParaEntity);
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
            modbusGatewayParaBll.DeleteForm(keyValue);
            return Success("删除成功。");
        }

        #endregion

    }
}

