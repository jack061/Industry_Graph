using JFine.Busines.SystemManage;
using JFine.Common.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JFine.Web.Base.MVC.Handler;

namespace JFine.Web.Areas.SysManage.Controllers
{
    public class PermissionController : JFControllerBase
    {
        private PermissionBLL permissionBLL = new PermissionBLL();
        //
        // GET: /SysManage/Permission/
        public override ActionResult Index()
        {
            return View();
        }

        #region 数据获取
        /// <summary>
        /// /// <summary>
        /// 获取全部菜单-关联角色
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <param name="category">1:角色；2：职位；3：岗位</param>
        /// <returns></returns>
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetModlesUnionRole(string roleId, int category)
        {
            DataTable dt_module = permissionBLL.GetModlesUnionRole(roleId, category);
            DataTable dt_moduleOperate = permissionBLL.GetModlesOprateUnionRole(roleId, category);
            var data = new
            {
                modules = dt_module,
                operates = dt_moduleOperate
                
            };
            return Content(data.ToJson());
        }
        #endregion

        #region 数据处理
        
        #endregion

        #region 验证数据
        #endregion

    }
}