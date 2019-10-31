/********************************************************************************
**文 件 名:apiController
**命名空间:JFine.Plugins.IOT.Areas.IOT.Controllers
**描    述:
**
**版 本 号:V1.0.0.0
**创 建 人:superjoy
**创建日期:2019-07-03 10:49:16
**修 改 人:
**修改日期:
**修改描述:
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
using JFine.Cache;
using JFine.Common.Encrypt;
using JFine.Sequence;
using Microsoft.AspNet.SignalR;
using System.Collections;
using JFine.Plugins.IOT.Domain.Models.IOT;
using JFine.Plugins.IOT.Busines.IOT;
using JFine.Plugins.IOT.IOTHubs;

namespace JFine.Plugins.IOT.Areas.IOT.Controllers
{
    [HandlerPlugin("IOT")]
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
                IOTDeviceBLL iOTDeviceBll = new IOTDeviceBLL();
                var data = iOTDeviceBll.GetForm(code, key);
                if (data == null)
                {
                    return Error("请求不合法。");
                }
                else
                {
                    string token = Md5Helper.MD5(Guid.NewGuid().ToString(), 16);
                    CacheFactory.Cache().WriteCache<string>(code + "_" + key, token, true, 30, false);
                    CacheFactory.Cache().WriteCache<string>(token, token, true, 30, false);
                    return Success("请求成功", token);
                }

            }
            return Success("请求成功", cache);

        }
        /// <summary>
        /// 提交预警信息
        /// </summary>
        /// <param name="token">token</param>
        /// <param name="code">编码</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult PostWarnInfo(string token, IOTDeviceWarnEntity iOTDeviceWarnEntity)
        {
            if (!IsLegal(token))
            {
                return Error("请求无效");
            }
            IOTDeviceBLL iOTDeviceBll = new IOTDeviceBLL();
            var deviceInfo = iOTDeviceBll.GetFormByCode(iOTDeviceWarnEntity.DeviceCode);
            if (deviceInfo == null)
            {
                return Error("获取设备信息失败，请检查设备编码是否正确。");
            }
            //VMWWarnTypeBLL vMWWarnTypeBLL = new VMWWarnTypeBLL();
            //var warnTypeInfo = vMWWarnTypeBLL.GetFormByCode(iOTDeviceWarnEntity.CategoryCode);
            //if (warnTypeInfo == null)
            //{
            //    return Error("预警类型不正确，请检查提交的数据。");
            //}
            IOTDeviceWarnBLL iOTDeviceWarnBLL = new IOTDeviceWarnBLL();
            iOTDeviceWarnEntity.Code = SequenceHelper.getRuleCode(IOTConstant.CODE_RULE_WARN);
            iOTDeviceWarnEntity.BindId = deviceInfo.Id;
            iOTDeviceWarnEntity.DeviceName = deviceInfo.Name;
            iOTDeviceWarnEntity.Position = deviceInfo.Position;
            //if ("是".Equals(warnTypeInfo.IsDeal))
            {
                iOTDeviceWarnEntity.ConfirmStatus = IOTConstant.STATU_CONFIRM_UN;
                iOTDeviceWarnEntity.DealStatus = IOTConstant.STATU_DEAL_UN;
            }
            //else
            //{
            ////    iOTDeviceWarnEntity.ConfirmStatus = IOTConstant.STATU_CONFIRM_NO;
            //    iOTDeviceWarnEntity.DealStatus = IOTConstant.STATU_DEAL_NO;
           // }


            iOTDeviceWarnBLL.SaveForm("", iOTDeviceWarnEntity);
            //广播预警信息
            WarnHub warnHub = new WarnHub();
            warnHub.DeviceWarn(iOTDeviceWarnEntity.ToJson());
            return Success("提交成功。");
        }

        private bool IsLegal(string token)
        {
            bool result = false;
            var cache = CacheFactory.Cache().GetCache<string>(token);
            if (!string.IsNullOrWhiteSpace(cache))
            {
                result = true;
            }
            return result;
        }
    }
}
