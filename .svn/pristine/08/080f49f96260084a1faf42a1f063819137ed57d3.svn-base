/********************************************************************************
**文 件 名:VMWCameraController
**命名空间:JFine.Plugins.VMW.Busines.VMW
**描    述:
**
**
**版 本 号:V1.0.0.0
**创 建 人:此代码由T4模板自动生成
**创建日期:2019-02-21 17:15:30
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
using JFine.Common.Encrypt;

namespace JFine.Plugins.VMW.Areas.VMW.Controllers
{
    [HandlerPlugin("VMW")]
	/// <summary>
	/// VMWCameraController
	/// </summary>	
	public class VMWCameraController:JFControllerBase
	{
		 private VMWCameraBLL vMWCameraBll = new VMWCameraBLL();

        #region View
        //
        // GET: /VMW/
        public override ActionResult Index()
        {
            return View("~/Plugins/JFine.VMW/Views/VMWCamera/Index.cshtml");
        }
        
        //
        // GET: /VMW/
        public ActionResult VideoIndex()
        {
            return View("~/Plugins/JFine.VMW/Views/VMWCamera/VideoIndex.cshtml");
        }
        
        //海康摄像头
        // GET: /VMW/
        public ActionResult HKVideo()
        {
            return View("~/Plugins/JFine.VMW/Views/VMWCamera/HKVideo.cshtml");
        }
        
        //海康摄像头
        // GET: /VMW/
        public ActionResult HKVideoIndex()
        {
            return View("~/Plugins/JFine.VMW/Views/VMWCamera/HKVideoIndex.cshtml");
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
            return View("~/Plugins/JFine.VMW/Views/VMWCamera/Form.cshtml");
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
            return View("~/Plugins/JFine.VMW/Views/VMWCamera/Form2.cshtml");
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
            return View("~/Plugins/JFine.VMW/Views/VMWCamera/Details2.cshtml");
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
        public ActionResult GetAllCameraList()
        {
            var data = vMWCameraBll.GetList();
            return Content(data.ToJson());
        }

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
                rows = vMWCameraBll.GetPageList(pagination, queryJson),
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
        public ActionResult GetCameraList(string queryJson)
        {
            var data = vMWCameraBll.GetList(queryJson);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 获取摄像头数量 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetCameraCount()
        {
            var cameralist = vMWCameraBll.GetList("{}");
            var data = new
            {
                sumCount = cameralist.Count(),
                normalCount = cameralist.Count(t=>t.Status == 1),
                warnCount = cameralist.Count(t => t.Status == 2),
                unonlineCount = cameralist.Count(t => t.Status == 0)
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// 获取摄像头预警数量
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetCameraWarnCount()
        {
            var cameralist = vMWCameraBll.GetList("{}");
            var data = new
            {
                nameList = cameralist.Select(x=>x.Name),
                warnCount = cameralist.Select(x => x.WarningCount),
                rightCount = cameralist.Select(x => x.WarningCount-x.ErrorCount),
                rightPer = cameralist.Select(x => ((double)(x.WarningCount - x.ErrorCount) / (double)x.WarningCount) * 100)
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
            var data = vMWCameraBll.GetForm(keyValue);
            return Content(data.ToJson());
        }
        #endregion

        #region 数据处理
        

        /// <summary>
        /// 保存功能表单
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="VMWCameraEntity">功能实体</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult SaveForm(string keyValue, VMWCameraEntity vMWCameraEntity)
        {
            vMWCameraBll.SaveForm(keyValue, vMWCameraEntity);
            return Success("保存成功。");
        }

        /// <summary>
        /// 更新密钥
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult UpdateSecretKey(string keyValue)
        {
            var vMWCameraEntity = vMWCameraBll.GetForm(keyValue);
            if (vMWCameraEntity == null) 
            {
                return Error("获取摄像头信息失败！");
            }

            VMWCameraEntity tempEntity = new VMWCameraEntity();
            tempEntity.Id = keyValue;
            tempEntity.SecretKey = Md5Helper.MD5(new Guid().ToString(), 16);
            vMWCameraBll.SaveForm(keyValue, tempEntity);
            return Success("更新成功。");
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
            vMWCameraBll.DeleteForm(keyValue);
            return Success("删除成功。");
        }

        #endregion

    }
}

