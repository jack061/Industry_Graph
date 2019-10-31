/********************************************************************************
**文 件 名:IOTMouldPParaController
**命名空间:JFine.Plugins.IOT.Busines.IOT
**描    述:
**
**
**版 本 号:V1.0.0.0
**创 建 人:此代码由T4模板自动生成
**创建日期:2019-08-09 14:43:11
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

namespace JFine.Plugins.IOT.Areas.IOT.Controllers
{	
	/// <summary>
	/// IOTMouldPParaController
	/// </summary>	
	public class IOTMouldPParaController:JFControllerBase
	{
		 private IOTMouldPParaBLL iOTMouldPParaBll = new IOTMouldPParaBLL();

        #region View
        //
        // GET: /IOT/
        public override ActionResult Index()
        {
            ViewBag.bindid = Request["bindid"]??"";
            IOTMouldBLL iOTMouldBll = new IOTMouldBLL();
            IOTMouldEntity mould = iOTMouldBll.GetForm(ViewBag.bindid);
            if (mould == null) 
            {
                return Content("获取关联模具信息失败！");
            }
            ViewBag.mouldCode = mould.Code;
            ViewBag.mouldName = mould.Name;
            return View("~/Plugins/JFine.IOT/Views/IOTMouldPPara/Index.cshtml");
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
            return View("~/Plugins/JFine.IOT/Views/IOTMouldPPara/Form.cshtml");
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
            return View("~/Plugins/JFine.IOT/Views/IOTMouldPPara/Details.cshtml");
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
            return View("~/Plugins/JFine.IOT/Views/IOTMouldPPara/Details2.cshtml");
        }

        #endregion

        #region 数据获取
        /// <summary>
        /// 功能列表 
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <returns>返回树形Json</returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeSelectJson(string keyword)
        {
            var data = iOTMouldPParaBll.GetList().ToList();
            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.TreeWhere(t => t.Name.Contains(keyword), "");
            }
            var treeList = new List<TreeSelectModel>();
            foreach (IOTMouldPParaEntity item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.Id;
                treeModel.text = item.Name;
                treeModel.parentId = item.BindId;
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson());
        
		}

		/// <summary>
        /// 列表 
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <returns></returns>
		[HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string queryJson, string bindid)
        {
            var data = new
            {
                rows = iOTMouldPParaBll.GetPageList(pagination, queryJson, bindid),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
        /// <summary>
        /// 获取目录级功能列表 
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <returns>返回树形Json</returns>
        [HttpGet]
        public ActionResult GetTreeGridJson(string keyword)
        {
            var data = iOTMouldPParaBll.GetList().ToList();
            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.TreeWhere(t => t.Name.Contains(keyword), "");
            }
            var treeList = new List<TreeGridModel>();
            foreach (IOTMouldPParaEntity item in data)
            {
                TreeGridModel treeModel = new TreeGridModel();
                bool hasChildren = data.Count(t => t.BindId == item.Id) == 0 ? false : true;
                treeModel.id = item.Id;
                treeModel.isLeaf = hasChildren;
                treeModel.parentId = item.BindId;
                treeModel.expanded = hasChildren;
                treeModel.entityJson = item.ToJson();
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeGridJson());
        }

        /// <summary>
        /// 功能实体 返回对象Json
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = iOTMouldPParaBll.GetForm(keyValue);
            return Content(data.ToJson());
        }
        #endregion

        #region 数据处理
        

        /// <summary>
        /// 保存功能表单
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="IOTMouldPParaEntity">功能实体</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult SaveForm(string keyValue, IOTMouldPParaEntity iOTMouldPParaEntity)
        {
            iOTMouldPParaBll.SaveForm(keyValue, iOTMouldPParaEntity);
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
            iOTMouldPParaBll.DeleteForm(keyValue);
            return Success("删除成功。");
        }

        #endregion

    }
}

