
//------------------------------------------------------------------------------
//     此代码由T4模板自动生成
//	   生成时间 2017-09-26 21:36:41
//    ©为之团队
//------------------------------------------------------------------------------
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
using JFine.WorkFlow.Business;

namespace JFine.Web.Areas.WorkFlow.Controllers
{
    /// <summary>
    /// WF_BaseController
    /// </summary>	
    public class WorkFlowController : JFControllerBase
    {
        private WF_BaseBLL wF_BaseBll = new WF_BaseBLL();

        #region View
        //
        // GET: /WorkFlow/
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
            ViewBag.Id = Request["keyValue"]??"";
            return View();
        }
        /// <summary>
        /// NodeForm表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize]
        public ActionResult NodeForm()
        {
            ViewBag.Id = Request["keyValue"]??"";
            return View();
        }

        /// <summary>
        /// LineForm表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize]
        public ActionResult LineForm()
        {
            ViewBag.Id = Request["keyValue"];
            return View();
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
            var data = wF_BaseBll.GetList().ToList();
            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.TreeWhere(t => t.Name.Contains(keyword), "");
            }
            var treeList = new List<TreeSelectModel>();
            foreach (WF_BaseEntity item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.Id;
                treeModel.text = item.Name;
                treeModel.parentId = "0";
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
        public ActionResult GetGridJson(Pagination pagination, string queryJson)
        {
            var data = new
            {
                rows = wF_BaseBll.GetPageList(pagination, queryJson),
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
            var data = wF_BaseBll.GetForm(keyValue);
            return Content(data.ToJson());
        }
        #endregion

        #region 数据处理

        /// <summary>
        /// 保存功能表单
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="WF_BaseEntity">功能实体</param>
        /// <returns></returns>
        ///
        /*
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult SaveForm(string keyValue, WF_BaseEntity wF_BaseEntity)
        {
            wF_BaseBll.SaveForm(keyValue, wF_BaseEntity);
            return Success("保存成功。");
        }
         * */
        /// <summary>
        /// 保存表单
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体</param>
        /// <param name="nodes">节点列表</param>
        /// <param name="lines">连线列表</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult SaveForm(string keyValue, WF_BaseEntity wF_BaseEntity, List<WF_NodeEntity> nodes, List<WF_LineEntity> lines)
        {
            wF_BaseBll.SaveForm(keyValue, wF_BaseEntity, nodes, lines);
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
            wF_BaseBll.DeleteForm(keyValue);
            return Success("删除成功。");
        }

        #endregion

        #region 数据验证

        #endregion
    }
}