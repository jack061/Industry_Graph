using JFine.Common.UI;
using JFine.Common.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JFine.Busines.SystemManage;
using JFine.Web.Base.MVC.Handler;

namespace JFine.Web.Areas.SysManage.Controllers
{
    public class RightController : JFControllerBase
    {
        private RightBLL rightBLL = new RightBLL();

        #region 数据获取操作
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string queryJson)
        {
            var data = new
            {
                //rows = rightBLL.GetList(pagination, queryJson),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            //string sss = data.ToJson();
            return Content(data.ToJson());
        }
        #endregion

        #region 数据处理操作
        /// <summary>
        /// 保存授权
        /// </summary>
        /// <param name="keyValue">对象id</param>
        /// <param name="category">对象类型0:用户；1:角色；2：职位；3：岗位；4：工作组</param>
        /// <param name="modules">模块</param>
        /// <param name="moduleOperations">按钮</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult SaveRights(string keyValue, string category, string modules, string moduleOperations)
        {
            if (string.IsNullOrEmpty(keyValue) || string.IsNullOrEmpty(category))
            {
                return Error("角色信息不完整，请重新操作！");
            }
            else
            {
                rightBLL.SaveRights(keyValue, category, modules, moduleOperations);
            }

            return Success("保存成功。");
        }
        #endregion

        #region 验证数据操作

        #endregion
    }
}