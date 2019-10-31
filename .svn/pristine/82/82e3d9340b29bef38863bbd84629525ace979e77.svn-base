/********************************************************************************
**文 件 名:ModbusGatewayController
**命名空间:JFine.Plugins.IOT.Busines.Modbus
**描    述:
**
**
**版 本 号:V1.0.0.0
**创 建 人:此代码由T4模板自动生成
**创建日期:2019-06-24 17:55:27
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
	/// ModbusGatewayController
	/// </summary>	
	public class ModbusGatewayController:JFControllerBase
	{
		 private ModbusGatewayBLL modbusGatewayBll = new ModbusGatewayBLL();

        #region View
        //
        // GET: /Modbus/
        public override ActionResult Index()
        {
            return View("~/Plugins/JFine.IOT/Views/ModbusGateway/Index.cshtml");
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
            return View("~/Plugins/JFine.IOT/Views/ModbusGateway/Form.cshtml");
        }

        /// <summary>
        /// Form表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize]
        public ActionResult Form2()
        {
            ViewBag.Id = Request["keyValue"]??"";
            if (string.IsNullOrWhiteSpace(ViewBag.Id)) 
            {
                ModbusGatewayEntity modbusGatewayEntity = new ModbusGatewayEntity();
                modbusGatewayBll.SaveForm("", modbusGatewayEntity);
                ViewBag.Id = modbusGatewayEntity.Id;
            }
            return View("~/Plugins/JFine.IOT/Views/ModbusGateway/Form2.cshtml");
        }

        /// <summary>
        /// Form表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize]
        public ActionResult Test()
        {
            return View("~/Plugins/JFine.IOT/Views/ModbusGateway/Test.cshtml");
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
            var currentlist = modbusGatewayBll.GetPageList(pagination, queryJson).ToList();
            var modList = ModbusBLL.gatewayList;
            currentlist.ForEach(m =>
                    {
                        var s = modList.FirstOrDefault(n => n.Id == m.Id);
                        if(s != null)
                        {
                            m.Status = s.Status;
                        }
                    }
                    );
            var data = new
            {
                rows = currentlist,
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
            var data = modbusGatewayBll.GetForm(keyValue);
            return Content(data.ToJson());
        }
        #endregion

       


        #region modbus 监控线程管理
        /// <summary>
        /// 一键启动
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        public ActionResult OnekeyStart()
        {
            ModbusBLL modbusBLL = new ModbusBLL();
            modbusBLL.OnekeyStart();
            return Success("一键启动成功。");
        }

        /// <summary>
        /// 一键挂起
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        public ActionResult OnekeySuspend()
        {
            ModbusBLL modbusBLL = new ModbusBLL();
            modbusBLL.OnekeySuspend();
            return Success("一键挂起成功。");
        }

        /// <summary>
        /// 一键恢复
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        public ActionResult OnekeyRecover()
        {
            ModbusBLL modbusBLL = new ModbusBLL();
            modbusBLL.OnekeyRecover();
            return Success("一键恢复成功。");
        }

        /// <summary>
        /// 单个启动
        /// </summary>
        /// <param name="keyValue">网关Id</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        public ActionResult Start(string keyValue)
        {
            ModbusBLL modbusBLL = new ModbusBLL();
            modbusBLL.Start(keyValue);
            return Success("启动成功。");
        }

        /// <summary>
        /// 单个挂起
        /// </summary>
        /// <param name="keyValue">网关Id</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        public ActionResult Suspend(string keyValue)
        {
            ModbusBLL modbusBLL = new ModbusBLL();
            modbusBLL.Suspend(keyValue);
            return Success("挂起成功。");
        }

        /// <summary>
        /// 单个恢复
        /// </summary>
        /// <param name="keyValue">网关Id</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        public ActionResult Recover(string keyValue)
        {
            ModbusBLL modbusBLL = new ModbusBLL();
            modbusBLL.Recover(keyValue);
            return Success("恢复成功。");
        }

        #endregion

        #region modbus Server

        /// <summary>
        /// 开启Modbus Server
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        public ActionResult ModbusServerStart()
        {
            ModbusServerBLL modbusServerBLL = new ModbusServerBLL();
            modbusServerBLL.ModbusServerStart();
            return Success("启动成功。");
        }

        #endregion



        #region 数据处理


        /// <summary>
        /// 保存功能表单
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="ModbusGatewayEntity">功能实体</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult SaveForm(string keyValue, ModbusGatewayEntity modbusGatewayEntity)
        {
            modbusGatewayBll.SaveForm(keyValue, modbusGatewayEntity);
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
            modbusGatewayBll.DeleteForm(keyValue);
            return Success("删除成功。");
        }

        #endregion

    }
}

