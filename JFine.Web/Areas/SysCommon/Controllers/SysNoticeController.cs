
//------------------------------------------------------------------------------
//     此代码由T4模板自动生成
//	   生成时间 2018-05-28 18:37:05
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
using JFine.Code.Online;
using JFine.Sequence;
using JFine.Util;
using JFine.Web.Base.MVC.Handler;

namespace JFine.Web.Areas.SysCommon.Controllers
{
    /// <summary>
    /// SysNoticeController
    /// </summary>	
    public class SysNoticeController : JFControllerBase
    {
        private SysNoticeBLL sysNoticeBll = new SysNoticeBLL();

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
        /// <summary>
        /// Detail
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize]
        public override ActionResult Details()
        {
            var Id = Request["keyValue"];
            //获取公告信息
            SysNoticeEntity notice = sysNoticeBll.GetForm(Id);
            if (notice != null) 
            {
                SysNoticeEntity notice_back = new SysNoticeEntity();
                notice_back.Id = Id;
                notice_back.ReadCount = notice.ReadCount + 1;
                sysNoticeBll.SaveForm(Id, notice_back);
            }
            
            return View(notice);
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
                rows = sysNoticeBll.GetPageList(pagination, queryJson),
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
            var data = sysNoticeBll.GetForm(keyValue);
            return Content(data.ToJson());
        }
        #endregion

        #region 数据处理


        /// <summary>
        /// 保存功能表单
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="SysNoticeEntity">功能实体</param>
        /// <param name="opFlag">操作标识0：保存；1：提交</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult SaveForm(string keyValue, SysNoticeEntity sysNoticeEntity,int opFlag)
        {
            if(opFlag == 1)
            {
                OnlineUser onlineUser = OnlineUser.Operator;
                sysNoticeEntity.NoticeNo = SequenceHelper.getRuleCode(ConstantUtil.CODE_RULE_XTGG);
                sysNoticeEntity.Status = "已发布";
                sysNoticeEntity.ReadCount = 0;
                sysNoticeEntity.PublishTime = DateTime.Now;
                sysNoticeEntity.PublishDEPT = onlineUser.GetCurrent().OrgName;
            }
            sysNoticeBll.SaveForm(keyValue, sysNoticeEntity);
            return Success("操作成功。");
        }

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
            sysNoticeBll.DeleteForm(keyValue);
            return Success("删除成功。");
        }

        #endregion

    }
}