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
    public class OrganizeController : JFControllerBase
    {
        private OrganizeBLL organizeBll = new OrganizeBLL();

        #region View

        /// <summary>
        /// Form表单
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
        /// Form表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize]
        public ActionResult ChooseOrg()
        {
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
            var data = organizeBll.GetList().ToList();
            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.TreeWhere(t => t.Name.Contains(keyword), "");
            }
            var treeList = new List<TreeSelectModel>();
            foreach (OrganizeEntity item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                bool hasChildren = data.Count(t => t.BindId == item.Id) == 0 ? false : true;
                treeModel.id = item.Id;
                treeModel.code = item.Code;
                treeModel.text = item.Name;
                treeModel.hasChildren = hasChildren;
                treeModel.parentId = item.BindId;
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson("0"));
        }
        /// <summary>
        /// 获取目录级功能列表 
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <returns>返回树形Json</returns>
        [HttpGet]
        public ActionResult GetTreeGridJson(string keyword)
        {
            var data = organizeBll.GetList().ToList();
            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.TreeWhere(t => t.Name.Contains(keyword), "");
            }
            var treeList = new List<TreeGridModel>();
            foreach (OrganizeEntity item in data)
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
        /// 获取角色列表--不分页
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(string queryJson)
        {
            var list = organizeBll.GetList().ToList();
            var data = new
            {
                rows = list,
                total = list.Count
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// 数据字典树形列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeJson()
        {
            var data = organizeBll.GetList();
            var treeList = new List<TreeViewModel>();
            foreach (OrganizeEntity item in data)
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
        /// 功能实体 返回对象Json
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = organizeBll.GetEntity(keyValue);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 获取关联用户的兼职部门列表
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        public ActionResult GetDeptListJoinUser(string userId)
        {
            DataTable datatable = organizeBll.GetDeptListJoinUser(userId);
            List<OrganizeExtend> list = (List<OrganizeExtend>)DataTableHelper.DataTableToIList<OrganizeExtend>(datatable);
            var treeList = new List<TreeGridModel>();
            foreach (OrganizeEntity item in list)
            {
                TreeGridModel treeModel = new TreeGridModel();
                bool hasChildren = list.Count(t => t.BindId == item.Id) == 0 ? false : true;
                treeModel.id = item.Id;
                treeModel.isLeaf = hasChildren;
                treeModel.parentId = item.BindId;
                treeModel.expanded = hasChildren;
                treeModel.entityJson = item.ToJson();
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeGridJson());
        }
        #endregion

        #region 数据处理
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
            organizeBll.DeleteForm(keyValue);
            return Success("删除成功。");
        }
        /// <summary>
        /// 保存功能表单
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="OrganizeEntity">功能实体</param>
        /// <param name="moduleButtonList">按钮实体列表</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult SaveForm(string keyValue, OrganizeEntity OrganizeEntity)
        {
            organizeBll.SaveForm(keyValue, OrganizeEntity);
            return Success("保存成功。");
        }

        /// <summary>
        /// 添加用户部门（主要用于兼职部门）
        /// </summary>
        /// <param name="keyValue">用户Id</param>
        /// <param name="roleList">部门列表</param>
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult SaveUserDept(string keyValue, string deptListJson)
        {

            organizeBll.SaveUserDept(keyValue, deptListJson);
            return Success("保存成功。");
        }
        #endregion

        #region 数据验证
        /// <summary>
        /// 公司名称不能重复
        /// </summary>
        /// <param name="EnCode">公司名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ExistFullName(string fullName, string keyValue)
        {
            bool IsOk = organizeBll.ExistFullName(fullName, keyValue);
            return Content(IsOk.ToString());
        }
        /// <summary>
        /// 外文名称不能重复
        /// </summary>
        /// <param name="FullName">名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ExistEnCode(string enCode, string keyValue)
        {
            bool IsOk = organizeBll.ExistEnCode(enCode, keyValue);
            return Content(IsOk.ToString());
        }
        /// <summary>
        /// 简称不能重复
        /// </summary>
        /// <param name="FullName">名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ExistShortName(string shortName, string keyValue)
        {
            bool IsOk = organizeBll.ExistShortName(shortName, keyValue);
            return Content(IsOk.ToString());
        }
        #endregion
    }
}