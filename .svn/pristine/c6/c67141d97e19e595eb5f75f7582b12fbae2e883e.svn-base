
//------------------------------------------------------------------------------
//     此代码由T4模板自动生成
//	   生成时间 2017-09-14 15:48:52
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
using JFine.Code;
using JFine.Web.Base.MVC.Handler;

namespace JFine.Web.Areas.SysCommon.Controllers
{
    /// <summary>
    /// Bid_BD_ConfigController
    /// </summary>	
    public class BDConfigController : JFControllerBase
    {
        private Bid_BD_ConfigBLL bid_BD_ConfigBll = new Bid_BD_ConfigBLL();

        #region View
        //
        // GET: /SysCommon/Organize/
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
        /// 列表 
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridPageJson(Pagination pagination, string queryJson)
        {
            var data = new
            {
                rows = bid_BD_ConfigBll.GetPageList(pagination, queryJson),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
        /// <summary>
        /// 获取一个条目下的所有扩展字段信息
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(string bidType, string itemId, string keyword)
        {
            var data = bid_BD_ConfigBll.GetList(bidType,itemId, keyword);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 根据类别条目获取下一个扩展字段
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetNextExpand(string bidtype,string itemId)
        {
            AjaxResult result = bid_BD_ConfigBll.GetNextExpand(bidtype,itemId);
            return Content(result.ToJson());
        }

        /// <summary>
        /// 功能实体 返回对象Json
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = bid_BD_ConfigBll.GetForm(keyValue);
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
            bid_BD_ConfigBll.DeleteForm(keyValue);
            return Success("删除成功。");
        }

        /// <summary>
        /// 保存功能表单
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="Bid_BD_ConfigEntity">功能实体</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult SaveForm(string keyValue, Bid_BD_ConfigEntity bid_BD_ConfigEntity)
        {
            bid_BD_ConfigBll.SaveForm(keyValue, bid_BD_ConfigEntity);
            return Success("保存成功。");
        }
        #endregion

        #region 数据验证

        #endregion
    }
}