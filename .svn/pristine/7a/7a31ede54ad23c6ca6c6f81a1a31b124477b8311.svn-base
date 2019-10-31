using JFine.Busines.SystemManage;
using JFine.Common.UI;
using JFine.Common.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JFine.Domain.Models.SystemManage;
using JFine.Web.Base.MVC.Handler;

namespace JFine.Web.Areas.SysManage.Controllers
{
    public class ModuleOperateController : JFControllerBase
    {
        private ModuleOperateBLL moduleOperateBll = new ModuleOperateBLL();

        #region 视图
        /// <summary>
        /// 功能表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize]
        public override ActionResult Form()
        {
            ViewBag.Id = Request["keyValue"];
            ViewBag.BindId = Request["BindId"];
            return View();
        }
        #endregion

        #region 数据获取操作
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(string queryJson)
        {
            var data = moduleOperateBll.GetList(queryJson);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 获取表单数据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = moduleOperateBll.GetEntity(keyValue);
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
            moduleOperateBll.DeleteForm(keyValue);
            return Success("删除成功。");
        }
        /// <summary>
        /// 保存功能表单
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="moduleEntity">实体</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SaveForm(string keyValue, string bindId,ModuleOperateEntity moduleOperateEntity)
        {
            if (string.IsNullOrEmpty(keyValue))
            {
                if(string.IsNullOrEmpty(bindId))
                {
                    return Error("获取菜单信息失败，无法保存按钮数据！。");
                }else
                {
                    moduleOperateEntity.BindId = bindId;
                }
                
            }
            moduleOperateBll.SaveForm(keyValue, moduleOperateEntity);
            return Success("保存成功。");
        }
        #endregion
    }
}