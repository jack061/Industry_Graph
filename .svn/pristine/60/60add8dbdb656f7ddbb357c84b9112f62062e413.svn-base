using JFine.Busines.SystemManage;
using JFine.Common.UI;
using JFine.Common.Json;
using JFine.Domain.Models.SystemManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using JFine.Common.Data;
using JFine.Web.Base.MVC.Handler;

namespace JFine.Web.Areas.SysManage.Controllers
{
    [HandlerIsSystem]
    public class ModuleController : JFControllerBase
    {
        private ModuleBLL moduleBLL = new ModuleBLL();

        #region View
        
        /// <summary>
        /// 列表
        /// GET: /SysManage/Module/
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize]
        public override ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 功能表单
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
        /// 功能图标
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Icon()
        {
            return View();
        }

        #endregion

        #region 数据获取操作
        /// <summary>
        /// 功能列表 
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <returns>返回树形Json</returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeSelectJson(string keyword)
        {
            var data = moduleBLL.GetList();
            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.TreeWhere(t => t.Name.Contains(keyword), "");
            }
            var treeList = new List<TreeSelectModel>();
            foreach (ModuleEntity item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                bool hasChildren = data.Count(t => t.BindId == item.Id) == 0 ? false : true;
                treeModel.id = item.Id;
                treeModel.text = item.Name;
                treeModel.hasChildren = hasChildren;
                treeModel.parentId = item.BindId;
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson("0"));
        }

        /// <summary>
        /// 数据字典树形列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeJson()
        {
            var data = moduleBLL.GetList();
            var treeList = new List<TreeViewModel>();
            foreach (ModuleEntity item in data)
            {
                TreeViewModel tree = new TreeViewModel();
                bool hasChildren = data.Count(t => t.BindId == item.Id) == 0 ? false : true;
                tree.id = item.Id;
                tree.text = item.Name;
                tree.value = item.Code;
                tree.parentId = item.BindId;
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = hasChildren;
                treeList.Add(tree);
            }
            return Content(treeList.TreeViewJson());
        }

        /// <summary>
        /// 获取目录级功能列表 
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <returns>返回树形Json</returns>
        [HttpGet]
        public ActionResult GetTreeGridJson(string keyword)
        {
            var data = moduleBLL.GetList();
            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.TreeWhere(t => t.Name.Contains(keyword), "");
            }
            var treeList = new List<TreeGridModel>();
            foreach (ModuleEntity item in data)
            {
                TreeGridModel treeModel = new TreeGridModel();
                bool hasChildren = data.Count(t => t.BindId == item.Id) == 0 ? false : true;
                treeModel.id = item.Id;
                treeModel.isLeaf = hasChildren;
                treeModel.parentId = item.BindId;
                //treeModel.expanded = hasChildren;
                treeModel.expanded = item.IsExpand == null ? false : (bool)item.IsExpand;
                treeModel.entityJson = item.ToJson();
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeGridJson());
        }
        /// <summary>
        /// 功能列表 
        /// </summary>
        /// <param name="parentid">节点Id</param>
        /// <param name="condition">查询条件</param>
        /// <param name="keyword">关键字</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetListJson(string parentid, string condition, string keyword)
        {
            var data = moduleBLL.GetList(parentid);
            if (!string.IsNullOrEmpty(condition) && !string.IsNullOrEmpty(keyword))
            {
                #region 多条件查询
                switch (condition)
                {
                    case "Code":    //编号
                        data = data.FindAll(t => t.Code.Contains(keyword));
                        break;
                    case "Name":      //名称
                        data = data.FindAll(t => t.Name.Contains(keyword));
                        break;
                    case "UrlAddress":   //地址
                        data = data.FindAll(t => t.UrlAddress.Contains(keyword));
                        break;
                    default:
                        break;
                }
                #endregion
            }
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
            var data = moduleBLL.GetEntity(keyValue);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 获取功能授权的按钮
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetOperateButtonJson(string keyValue) 
        {
            DataTable dt = moduleBLL.GetOperateButtonList(keyValue);
            var data = new
            {
                rows = dt,
                total = DataTableHelper.IsExistRows(dt)? dt.Rows.Count:0
            };
            return Content(data.ToJson());
        }


        #endregion

        #region 数据处理操作
        
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
            moduleBLL.DeleteForm(keyValue);
            return Success("删除成功。");
        }
        /// <summary>
        /// 保存功能表单
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="moduleEntity">功能实体</param>
        /// <param name="moduleButtonList">按钮实体列表</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult SaveForm(string keyValue, ModuleEntity moduleEntity)
        {
            moduleBLL.SaveForm(keyValue, moduleEntity);
            return Success("保存成功。");
        }
        
        #endregion

        #region 验证数据操作
        /// <summary>
        /// 功能编号不能重复
        /// </summary>
        /// <param name="EnCode">编号</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ExistEnCode(string EnCode, string keyValue)
        {
            bool IsOk = moduleBLL.ExistEnCode(EnCode, keyValue);
            return Content(IsOk.ToString());
        }
        /// <summary>
        /// 功能名称不能重复
        /// </summary>
        /// <param name="FullName">名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ExistFullName(string FullName, string keyValue)
        {
            bool IsOk = moduleBLL.ExistFullName(FullName, keyValue);
            return Content(IsOk.ToString());
        }
        #endregion
    }
}
