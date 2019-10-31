using JFine.Busines.SystemManage;
using JFine.Cache;
using JFine.Code.Online;
using JFine.Common.Config;
using JFine.Common.Json;
using JFine.WeChat.GZH;
using Senparc.Weixin.MP;
using Senparc.Weixin.Work.AdvancedAPIs;
using Senparc.Weixin.Work.AdvancedAPIs.OAuth2;
using Senparc.Weixin.Work.CommonAPIs;
using Senparc.Weixin.Work.Containers;
using Senparc.Weixin.Work.Entities;
using Senparc.Weixin.Work.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JFine.Web.Controllers
{
    public class WeiChatController : Controller
    {
        // GET: WeiChat
        private readonly string Token = "JFine.WeiChat";

        public ActionResult Index(string signature, string timestamp, string nonce, string echostr)
        {
            var a = Request[""];
            //get method - 仅在微信后台填写URL验证时触发
            if (CheckSignature.Check(signature, timestamp, nonce, Token))
            {
                return Content(echostr); //返回随机字符串则表示验证通过
            }
            else
            {
                return Content("failed:" + signature + "," + CheckSignature.GetSignature(timestamp, nonce, Token) + "。" +
                           "如果你在浏览器中看到这句话，说明此地址可以被作为微信公众账号后台的Url，请注意保持Token一致。");
            }
        }

        [HttpPost]

        public ActionResult Create(string signature, string timestamp, string nonce, string echostr)
        {
            //post method - 当有用户想公众账号发送消息时触发
            if (!CheckSignature.Check(signature, timestamp, nonce, Token))
            {
                return Content("参数错误！");
            }
            //设置每个人上下文消息储存的最大数量，防止内存占用过多，如果该参数小于等于0，则不限制
            var maxRecordCount = 10;

            //自定义MessageHandler，对微信请求的详细判断操作都在这里面。
            var messageHandler = new WeiChatMessageHandler(Request.InputStream, maxRecordCount);
            try
            {

                //测试时可开启此记录，帮助跟踪数据，使用前请确保App_Data文件夹存在，且有读写权限。
                messageHandler.RequestDocument.Save(
                    Server.MapPath("~/App_Data/" + DateTime.Now.Ticks + "_Request_" +
                                   messageHandler.RequestMessage.FromUserName + ".txt"));
                //执行微信处理过程
                messageHandler.Execute();
                //测试时可开启，帮助跟踪数据
                messageHandler.ResponseDocument.Save(
                   Server.MapPath("~/App_Data/" + DateTime.Now.Ticks + "_Response_" +
                                   messageHandler.ResponseMessage.ToUserName + ".txt"));
                return Content(messageHandler.ResponseDocument.ToString());
            }
            catch (Exception ex)
            {
                //将程序运行中发生的错误记录到App_Data文件夹
                using (TextWriter tw = new StreamWriter(Server.MapPath("~/App_Data/Error_" + DateTime.Now.Ticks + ".txt")))
                {
                    tw.WriteLine(ex.Message);
                    tw.WriteLine(ex.InnerException.Message);
                    if (messageHandler.ResponseDocument != null)
                    {
                        tw.WriteLine(messageHandler.ResponseDocument.ToString());
                    }
                    tw.Flush();
                    tw.Close();
                }
                return Content(ex.Message);
            }
        }

        #region 企业号登陆验证
        public ActionResult UserOAuth(string code, string redirecturl = "")
        {
            OnlineUserModel onlineUser = OnlineUser.Operator.GetCurrent();
            if (onlineUser == null)
            {
                string Host = ConfigHelper.GetValue("Host");
                string corpId = ConfigHelper.GetValue("qy_corpId");
                string agentId = ConfigHelper.GetValue("qy_agentId");
                string corpSecret = ConfigHelper.GetValue("qy_corpSecret");
                string url = OAuth2Api.GetCode(corpId, Host + "/WeiChat/UserOAuth", "", "");
                if (string.IsNullOrWhiteSpace(code))
                {
                    return Redirect(url);
                }
                if (CacheFactory.Cache().GetCache<string>(code) != code)
                {
                    CacheFactory.Cache().WriteCache<string>(code, code, true, 5, false);
                    //获取token
                    AccessTokenResult tokenResult = CommonApi.GetToken(corpId, corpSecret);
                    GetUserInfoResult userInfoResult = OAuth2Api.GetUserId(tokenResult.access_token, code);
                    if (userInfoResult != null)
                    {
                        UserBLL userBLL = new UserBLL();
                        userBLL.CheckLoginFromQYWechat(userInfoResult.UserId, Session.SessionID);
                        if (redirecturl == "")
                        {
                            return Redirect("/Credit/Credit/Index");
                        }
                        else
                        {
                            return Redirect(redirecturl);
                        }
                    }
                    else
                    {
                        return Content("登陆失败");
                    }
                }
                else
                {
                    //多次请求时暂且重定向主页
                    return Redirect("/Credit/Credit/Index");
                }

            }
            else
            {
                if (redirecturl == "")
                {
                    return Redirect("/Credit/Credit/Index");
                }
                else
                {
                    return Redirect(redirecturl);
                }
            }



        }
        /// <summary>
        /// JSDK
        /// </summary>
        /// <returns></returns>
        public ActionResult GetWXJSToken()
        {
            string Host = ConfigHelper.GetValue("Host");
            string corpId = ConfigHelper.GetValue("qy_corpId");
            string agentId = ConfigHelper.GetValue("qy_agentId");
            string corpSecret = ConfigHelper.GetValue("qy_corpSecret");
            //AccessTokenResult tokenResult = CommonApi.GetToken(corpId, corpSecret);
            string url = Host + "/Credit/Credit/JFExchangeScan";
            // string url ="http://2218019gx5.51mypc.cn/Credit/Credit/JFExchange";
            //string access_token = tokenResult.access_token;
            string tickt = JsApiTicketContainer.TryGetTicket(corpId, corpSecret);
            string noncestr = JSSDKHelper.GetNoncestr();
            long timestamp = JSSDKHelper.GetTimestamp();
            string signature = JSSDKHelper.GetSignature(tickt, noncestr, timestamp, url);
            JsSdkUiPackage package = new JsSdkUiPackage(corpId, timestamp.ToString(), noncestr, signature);

            return Content(package.ToJson());
        }
        #endregion
    }
}