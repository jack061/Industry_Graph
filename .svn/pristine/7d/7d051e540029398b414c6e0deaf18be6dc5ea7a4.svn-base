/********************************************************************************
**文 件 名:VMWHomeController
**命名空间:JFine.Plugins.Busines.VMW
**描    述:
**
**
**版 本 号:V1.0.0.0
**创 建 人:此代码由T4模板自动生成
**创建日期:2019-02-19 09:50:01
**修 改 人:
**修改日期:
**修改描述:
**
**
**版权所有: ©为之团队
*********************************************************************************/
using JFine.Common.UI;
using JFine.Common.Json;
using JFine.Web.Base.MVC.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using JFine.Plugins.VMW.Busines.VMW;
using JFine.Plugins.VMW.Domain.Models.VMW;
using JFine.Cache;
using JFine.Common.Encrypt;
using JFine.Sequence;
using Microsoft.AspNet.SignalR;
using JFine.Plugins.VMW.VMWHub;
using System.Collections;

namespace JFine.Plugins.VMW.Areas.VMW.Controllers
{
    [HandlerPlugin("VMW")]
	/// <summary>
    /// apiController
	/// </summary>	
    public class apiController : JFControllerBase2
	{

        /// <summary>
        /// 获取令牌
        /// </summary>
        /// <param name="code"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetToken(string code, string key)
        {
            var cache = CacheFactory.Cache().GetCache<string>(code + "_" + key);
            if (string.IsNullOrWhiteSpace(cache))//如果没有该缓存  
            {
                VMWCameraBLL vMWCameraBll = new VMWCameraBLL();
                var data = vMWCameraBll.GetForm(code, key);
                if (data == null)
                {
                    return Error("请求不合法。");
                }
                else 
                {
                    string token = Md5Helper.MD5(Guid.NewGuid().ToString(), 16);
                    CacheFactory.Cache().WriteCache<string>(code + "_" + key, token, true, 30, false);
                    CacheFactory.Cache().WriteCache<string>(token, token, true, 30, false);
                    return Success("请求成功",token);
                }
               
            }
            return Success("请求成功", cache);
            
        }

        /// <summary>
        /// 返回摄像头信息 返回对象Json
        /// </summary>
        /// <param name="token">token</param>
        /// <param name="code">编码</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetCameraInfo(string token,string code)
        {
            if (!IsLegal(token))
            {
                return Error("请求无效");
            }
            VMWCameraBLL vMWCameraBll = new VMWCameraBLL();
            var data = vMWCameraBll.GetFormByCode(code);
            return Success("请求成功",data.ToJson());
        }

        /// <summary>
        /// 返回预警配置信息 返回对象Json
        /// </summary>
        /// <param name="token">token</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetConfigInfo_old(string token)
        {
            if (!IsLegal(token))
            {
                return Error("请求无效");
            }
            VMWConfigBLL vMWConfigBll = new VMWConfigBLL();
            var data = vMWConfigBll.GetFormByCode("VMWCF");
            if (data != null)
            {
                return Success("请求成功", data.ConfigContent.ToJson());
            }
            else 
            {
                return Error("获取预警配置信息失败。");
            }
            
        }
        
        /// <summary>
        /// 返回预警配置信息 返回对象Json
        /// </summary>
        /// <param name="wtCode">预警类型编码</param>
        /// <param name="token">密钥</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetConfigInfo(string wtCode,string token)
        {
            if (!IsLegal(token))
            {
                return Error("请求无效");
            }
            VMWConfigNewBLL vMWConfigNewBLL = new VMWConfigNewBLL();
            var configList = vMWConfigNewBLL.GetListBySql(" and bindid in (select id from VMW_WarnType where Code = '" + wtCode + "') ");
            if (configList != null)
            {
                Hashtable ht = new Hashtable();
                foreach (var config in configList) 
                {
                    ht.Add(config.Code, config.Value);
                }
                return Success("请求成功", ht.ToJson());
            }
            else 
            {
                return Error("获取预警配置信息失败。");
            }
            
        }

        /// <summary>
        /// 提交预警信息
        /// </summary>
        /// <param name="token">token</param>
        /// <param name="code">编码</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult PostWarnInfo(string token, VMWWarningEntity warningEntity)
        {
            if (!IsLegal(token))
            {
                return Error("请求无效");
            }
            VMWCameraBLL vMWCameraBll = new VMWCameraBLL();
            var cameraInfo = vMWCameraBll.GetFormByCode(warningEntity.CameraCode);
            if (cameraInfo == null)
            {
                return Error("获取摄像头信息失败，请检查摄像头编码是否正确。");
            }
            VMWWarnTypeBLL vMWWarnTypeBLL = new VMWWarnTypeBLL();
            var warnTypeInfo = vMWWarnTypeBLL.GetFormByCode(warningEntity.CategoryCode);
            if (warnTypeInfo == null)
            {
                return Error("预警类型不正确，请检查提交的数据。");
            }
            VMWWarningBLL vMWWarningBll = new VMWWarningBLL();
            warningEntity.Code = SequenceHelper.getRuleCode(VMWConstant.CODE_RULE_WARN);
            warningEntity.BindId = cameraInfo.Id;
            warningEntity.CameraName = cameraInfo.Name;
            warningEntity.Position = cameraInfo.Position;
            if ("是".Equals(warnTypeInfo.IsDeal))
            {
                warningEntity.ConfirmStatus = VMWConstant.STATU_CONFIRM_UN;
                warningEntity.DealStatus = VMWConstant.STATU_DEAL_UN;
            }
            else 
            {
                warningEntity.ConfirmStatus = VMWConstant.STATU_CONFIRM_NO;
                warningEntity.DealStatus = VMWConstant.STATU_DEAL_NO;
            }
            

            vMWWarningBll.SaveForm("", warningEntity);
            //广播预警信息
            cameraInfo = vMWCameraBll.GetFormByCode(warningEntity.CameraCode);
            GlobalHost.ConnectionManager.GetHubContext<WarnHub>().Clients.All.warn(warningEntity.ToJson(), cameraInfo.ToJson());
            return Success("提交成功。");
        }


        private bool IsLegal(string token) 
        {
            bool result= false;
            var cache = CacheFactory.Cache().GetCache<string>(token);
            if (!string.IsNullOrWhiteSpace(cache)) 
            {
                result = true;
            }
            return result;
        }

    }
}

