
//------------------------------------------------------------------------------
//     此代码由T4模板自动生成
//	   生成时间 2017-08-02 10:15:17
//    ©为之团队
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JFine.Busines.SystemManage;
using JFine.Common.UI;
using JFine.Common.Json;
using JFine.Domain.Models.SystemManage;
using System.Data;
using JFine.Common.Data;
using JFine.Web.Base.MVC.Handler;

namespace JFine.Web.Areas.SysManage.Controllers
{
    public class RoleController : JFControllerBase
    {
        private RoleBLL roleBll = new RoleBLL();

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
        public ActionResult ChooseRole()
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
            var data = roleBll.GetList("{}").ToList();
            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.TreeWhere(t => t.Name.Contains(keyword), "");
            }
            var treeList = new List<TreeSelectModel>();
            foreach (RoleEntity item in data)
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
        /// 数据字典树形列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetTreeJson(int category)
        {
            var data = roleBll.GetList("{}",category).ToList();
            var treeList = new List<TreeViewModel>();
            foreach (RoleEntity item in data)
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
            return Content(treeList.TreeViewJson(null));
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
            var data = roleBll.GetList(queryJson);
            return Content(data.ToJson());
        }
        
        /// <summary>
        /// 获取角色列表--不分页
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetAllList(string queryJson)
        {
            DataTable dt = roleBll.GetAllList(queryJson);
            var data = new
            {
                rows = dt,
                total = DataTableHelper.IsExistRows(dt) ? dt.Rows.Count : 0
            };
            return Content(data.ToJson());
        }
        
        /// <summary>
        /// 获取角色列表--分页
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridPageJson(Pagination pagination, string queryJson)
        {
            var data = new
            {
                rows = roleBll.GetPageList(pagination, queryJson),
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
            var data = roleBll.GetForm(keyValue);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 获取关联用户的角色列表
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        public ActionResult GetRoleListJoinUser(string userId)
        {
            DataTable dt = roleBll.GetRoleListJoinUser(userId);
            var data = new
            {
                rows = dt,
                total = DataTableHelper.IsExistRows(dt) ? dt.Rows.Count : 0
            };
            return Content(data.ToJson());
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
            roleBll.DeleteForm(keyValue);
            return Success("删除成功。");
        }

        /// <summary>
        /// 保存功能表单
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="RoleEntity">功能实体</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult SaveForm(string keyValue, RoleEntity roleEntity)
        {

            if (!roleBll.ExistEnCode(roleEntity.Code, keyValue))
            {
                return Error("编码已经存在，请修改");
            }
            if (!roleBll.ExistFullName(roleEntity.Name, keyValue))
            {
                return Error("名称已经存在，请修改");
            }

            roleBll.SaveForm(keyValue, roleEntity);
            return Success("保存成功。");
        }
        
        /// <summary>
        /// 保存用户角色
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="roleListJson">功能实体</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult SaveUserRole(string keyValue, string roleListJson)
        {

            roleBll.SaveUserRole(keyValue, roleListJson);
            return Success("保存成功。");
        }
        #endregion

        #region 数据验证

        #endregion
	}
}