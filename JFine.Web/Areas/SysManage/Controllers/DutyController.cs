using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JFine.Busines.SystemManage;
using JFine.Common.Data;
using JFine.Common.UI;
using JFine.Domain.Models.SystemManage;
using JFine.Common.Json;
using JFine.Web.Base.MVC.Handler;

namespace JFine.Web.Areas.SysManage.Controllers
{
    public class DutyController : JFControllerBase
    {
        private DutyBLL dutyBll = new DutyBLL();

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
            var data = dutyBll.GetList("{}").ToList();
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
        /// 获取角色列表--不分页
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(string queryJson)
        {
            var data = dutyBll.GetList(queryJson);
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
            DataTable dt = dutyBll.GetAllList(queryJson);
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
                rows = dutyBll.GetPageList(pagination, queryJson),
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
            var data = dutyBll.GetForm(keyValue);
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
            dutyBll.DeleteForm(keyValue);
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
            if (!dutyBll.ExistEnCode(roleEntity.Code, keyValue)) 
            {
                return Error("编码已经存在，请修改");
            }
            if (!dutyBll.ExistFullName(roleEntity.Name, keyValue))
            {
                return Error("名称已经存在，请修改");
            }
            dutyBll.SaveForm(keyValue, roleEntity);
            return Success("保存成功。");
        }
        #endregion

        #region 数据验证

        #endregion
    }
}