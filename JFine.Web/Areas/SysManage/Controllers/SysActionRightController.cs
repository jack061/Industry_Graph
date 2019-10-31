/********************************************************************************
**文 件 名:SysActionRightController
**命名空间:JFine.Busines.SysManage
**描    述:
**
**
**版 本 号:V1.0.0.0
**创 建 人:此代码由T4模板自动生成
**创建日期:2019-06-17 18:11:23
**修 改 人:
**修改日期:
**修改描述:
**
**
**版权所有: ©为之团队
*********************************************************************************/
using JFine.Busines.SysManage;
using JFine.Common.UI;
using JFine.Common.Json;
using JFine.Domain.Models.SysManage;
using JFine.Web.Base.MVC.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

namespace JFine.Web.Areas.SysManage.Controllers
{
    [HandlerVistLog]
    [HandlerIsSystem]
	/// <summary>
	/// SysActionRightController
	/// </summary>	
	public class SysActionRightController:JFControllerBase
	{
		 private SysActionRightBLL sysActionRightBll = new SysActionRightBLL();

        #region View
        //
        // GET: /SysManage/
        public override ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Form表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public override ActionResult Form()
        {
            ViewBag.Id = Request["keyValue"];
            return View();
        }
        
        /// <summary>
        /// 用户权限
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult RoleActionRight()
        {
            return View();
        }

        /// <summary>
        /// 权限用户
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ActionRightRole()
        {
            return View();
        }

        /// <summary>
        /// 用户分配action
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DistributeAction()
        {
            return View();
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
                rows = sysActionRightBll.GetPageList(pagination, queryJson),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
        
        /// <summary>
        /// 角色对应的功能权限列表 
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <returns></returns>
		[HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetRoleActionRightList(Pagination pagination,string roleId, string queryJson)
        {
            var data = new
            {
                rows = sysActionRightBll.GetRoleActionRightList(pagination,roleId, queryJson),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
        
        /// <summary>
        /// 功能权限对应的角色列表 
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <returns></returns>
		[HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetActionRightRole(Pagination pagination, string bindid, string queryJson)
        {
            var data = new
            {
                rows = sysActionRightBll.GetActionRightRoleList(pagination, bindid, queryJson),
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
        public ActionResult ActionUnionRole(Pagination pagination, string roleId, string queryJson)
        {
            var data = new
            {
                rows = sysActionRightBll.ActionUnionRole(pagination, roleId, queryJson),
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
            var data = sysActionRightBll.GetForm(keyValue);
            return Content(data.ToJson());
        }
        #endregion

        #region 数据处理
        

        /// <summary>
        /// 保存功能表单
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="SysActionRightEntity">功能实体</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult SaveForm(string keyValue, SysActionRightEntity sysActionRightEntity)
        {
            sysActionRightBll.SaveForm(keyValue, sysActionRightEntity);
            return Success("保存成功。");
        }
        
        /// <summary>
        /// 保存功能表单
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="SysActionRightEntity">功能实体</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult OperateRight(bool status, SysActionRightEntity sysActionRightEntity)
        {
            var list = sysActionRightBll.GetListBySql(" and BindId = '" + sysActionRightEntity.BindId + "' and RoleId = '" + sysActionRightEntity.RoleId + "' and Category = '" + sysActionRightEntity.Category + "'").ToList();
            if (status)
            {
                if (list.Count() == 0)
                {
                    sysActionRightBll.SaveForm("", sysActionRightEntity);
                }
                
            }
            else 
            {
                if (list.Count() > 0) 
                {
                    sysActionRightBll.DeleteForm(list[0].Id);
                }
            }
            
            return Success("操作成功。");
        }

		/// <summary>
        /// 删除功能
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            sysActionRightBll.DeleteForm(keyValue);
            return Success("删除成功。");
        }

        #endregion

    }
}

