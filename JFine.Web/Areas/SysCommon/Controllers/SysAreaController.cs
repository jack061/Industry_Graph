//------------------------------------------------------------------------------
//     此代码由T4模板自动生成
//	   生成时间 2018-03-14 10:27:23
//    ©为之团队
//------------------------------------------------------------------------------
using JFine.Busines.SysCommon;
using JFine.Common.UI;
using JFine.Common.Json;
using JFine.Domain.Models.SysCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using JFine.Web.Base.MVC.Handler;

namespace JFine.Web.Areas.SysCommon.Controllers
{
    /// <summary>
    /// SysAreaController
    /// </summary>	
    public class SysAreaController : JFControllerBase2
    {
        private SysAreaBLL sysAreaBll = new SysAreaBLL();

        #region View
        //
        // GET: /SysCommon/
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
            ViewBag.Id = Request["keyValue"];
            return View();
        }

        #endregion

        #region 数据获取
        /// <summary>
        /// 功能列表 
        /// </summary>
        /// <param name="queryJson">查询字符串</param>
        /// <returns>下拉选择Json</returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetSelectJson(string queryJson)
        {
            var data = sysAreaBll.GetList(queryJson).ToList();
            return Content(data.ToJson());
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
                rows = sysAreaBll.GetPageList(pagination, queryJson),
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
            var data = sysAreaBll.GetForm(keyValue);
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
            sysAreaBll.DeleteForm(keyValue);
            return Success("删除成功。");
        }

        /// <summary>
        /// 保存功能表单
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="SysAreaEntity">功能实体</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult SaveForm(string keyValue, SysAreaEntity sysAreaEntity)
        {
            sysAreaBll.SaveForm(keyValue, sysAreaEntity);
            return Success("保存成功。");
        }
        #endregion

        #region 数据验证

        #endregion
    }
}