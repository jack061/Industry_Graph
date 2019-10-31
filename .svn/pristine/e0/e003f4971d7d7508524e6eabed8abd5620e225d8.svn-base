using JFine.Busines.SystemManage;
using JFine.Code;
using JFine.Code.Online;
using JFine.Common.Attributes;
using JFine.Common.Code;
using JFine.Common.Config;
using JFine.Common.Encrypt;
using JFine.Common.Extend;
using JFine.Common.Json;
using JFine.Common.Message;
using JFine.Common.Web;
using JFine.Domain.Models;
using JFine.Domain.Models.SystemManage;
using JFine.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using JFine.Web.Base.MVC.Handler;
using JFine.Busines.SysConfig;
using JFine.Domain.Models.SysConfig;
using JFine.Busines.SysManage;

namespace JFine.Web.Controllers
{
    
    public class LoginController : Controller
    {
        #region 视图
        [HandlerLoginSysConfig]
        public ActionResult Index()
        {
            if (OnlineUser.Operator.IsLogin() == 1)
            {
                //重定向到主页
                return Redirect("/Home/Index?token=" + OnlineUser.Operator.GetCurrent().Token);
            }
            SysConfigBLL sysConfigBll = new SysConfigBLL();
            BaseSysConfigEntity config = sysConfigBll.GetCurrentConfig();
            string loginIndex = "";
            string background = "";
            if (config != null && !string.IsNullOrWhiteSpace(config.LoginIndex) &&  !("0".Equals(config.LoginIndex)))
            {
                loginIndex = config.LoginIndex;
                background = config.LoginIndexBackground;
            }
            ViewBag.background = background.Replace("\\\\", "/").Replace("\\", "/");
            return View("index" + loginIndex);
            
        }

        public ActionResult IndexAdmin()
        {
            if (OnlineUser.Operator.IsLogin() == 1)
            {
                //重定向到主页
                return Redirect("/Home/Index?token=" + OnlineUser.Operator.GetCurrent().Token);
            }
            SysConfigBLL sysConfigBll = new SysConfigBLL();
            BaseSysConfigEntity config = sysConfigBll.GetCurrentConfig();
            string loginIndex = "";
            string background = "";
            if (config != null && !string.IsNullOrWhiteSpace(config.LoginIndex) &&  !("0".Equals(config.LoginIndex)))
            {
                loginIndex = config.LoginIndex;
                background = config.LoginIndexBackground;
            }
            ViewBag.background = background.Replace("\\","/");
            return View();
            
        }
        public ActionResult TransferIndex()
        {
            ViewBag.IsLogin = -1;
            if (OnlineUser.Operator.IsLogin() == 1)
            {
                ViewBag.IsLogin = 1;
            }
            SysConfigBLL sysConfigBll = new SysConfigBLL();
            BaseSysConfigEntity config = sysConfigBll.GetCurrentConfig();
            string loginIndex = "";
            string background = "";
            if (config != null && !string.IsNullOrWhiteSpace(config.LoginIndex) && !("0".Equals(config.LoginIndex)))
            {
                loginIndex = config.LoginIndex;
                background = config.LoginIndexBackground;
            }
            ViewBag.background = background.Replace("\\\\", "/").Replace("\\", "/");
            return View();
            
        }
        #endregion

        #region 数据处理
        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult VerifyCode()
        {
            return File(new VerifyCode().GetVerifyCode(), @"image/Gif");
        }
        /// <summary>
        /// 安全退出
        /// </summary>
        /// <returns></returns>
        [HandlerLogin]
        public ActionResult OutLogin()
        {
            OnlineUserModel onliner = OnlineUser.Operator.GetCurrent();
            LogEntity logEntity = new LogEntity();
            logEntity.Category = "退出";
            logEntity.OperateType = "退出";
            logEntity.CreateUserId = onliner.Account;
            logEntity.CreateUserName = onliner.UserName;
            logEntity.Description = "退出系统";
            logEntity.Module = ConfigHelper.GetValue("SoftName");
            logEntity.WriteLog();
            OnlineUser.Operator.RemoveCurrent();
            Session.Abandon();                                          //清除当前会话
            Session.Clear();                                            //清除当前浏览器所有Session
            CookieHelper.RemoveCookie("jfine_autologin");                  //清除自动登录
            if (onliner.IsSystem)
            {
                return RedirectToAction("IndexAdmin", "Login");
            }
            else 
            {
                return RedirectToAction("Index", "Login");
            }
            
        }
        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="verifycode">验证码</param>
        /// <param name="autologin">下次自动登录</param>
        /// <returns></returns>
        [HttpPost]
        //[HandlerAjaxOnly]
        [HandlerLoginSysConfig]
        public ActionResult CheckLogin(string username, string password, string verifycode, int autologin)
        {
            LogEntity logEntity = new LogEntity();
            logEntity.Category = "登陆";
            logEntity.OperateType = "登陆";
            logEntity.CreateUserId = username;
            logEntity.CreateUserName = username;
            logEntity.Module = ConfigHelper.GetValue("SoftName");
            try
            {
                #region 验证码验证
                if (autologin == 0)
                {
                    verifycode = Md5Helper.MD5(verifycode.ToLower(), 16);
                    if (Session["session_verifycode"].IsEmpty() || verifycode != Session["session_verifycode"].ToString())
                    {
                        throw new Exception("验证码错误，请重新输入");
                    }
                }
                #endregion

                #region 验证中心验证数据
                /*
                HttpClient httpClient = new HttpClient();
                string url = ConfigHelper.GetValue("CAHost");;
                httpClient.BaseAddress = new Uri(url);
                HttpResponseMessage response = httpClient.GetAsync("api/CA/Login?account=" + username + "&password=" + password).Result;
                if (response.IsSuccessStatusCode)
                {
                    AjaxResult jsonResult = response.Content.ReadAsAsync<AjaxResult>().Result;
                    if (jsonResult.state.ToString() == ResultType.success.ToString())
                    {
                        UserEntity userEntity = jsonResult.data.ToString().ToObject<UserEntity>();
                        if (userEntity != null)
                        {
                            PermissionBLL authorizeBLL = new PermissionBLL();
                            OrganizeBLL organizeBll = new OrganizeBLL();
                            OnlineUserModel operators = new OnlineUserModel();
                            operators.UserId = userEntity.Id;
                            operators.Account = userEntity.Account;
                            operators.UserName = userEntity.RealName;
                            operators.Password = userEntity.Password;
                            operators.IsLeader = (userEntity.IsLeader.HasValue ? (bool)userEntity.IsLeader : false);
                            operators.CompanyId = userEntity.OrganizeId;
                            var orgEntity = organizeBll.GetEntity(userEntity.OrganizeId);
                            operators.CompanyName = orgEntity.Name;
                            operators.CompanyCode = orgEntity.Code;
                            operators.DepartmentId = userEntity.DepartmentId;
                            orgEntity = organizeBll.GetEntity(userEntity.DepartmentId);
                            operators.DepartmentName = orgEntity.Name;
                            operators.DepartmentCode = orgEntity.Code;
                            operators.IP = RequestHelper.Ip;
                            operators.IPLocal = RequestHelper.GetLocation(RequestHelper.Ip);
                            operators.System = RequestHelper.GetClientOSName();
                            operators.Browser = RequestHelper.Browser;
                            operators.IsMobile = RequestHelper.IsMobile();
                            operators.RoleId = new RoleBLL().GetObjectStr(userEntity.Id, (int)AuthorizeTypeEnum.Role);
                            operators.LogTime = DateTime.Now;
                            operators.Token = jsonResult.message;
                            //判断是否系统管理员
                            if (userEntity.IsAdministrator == true)
                            {
                                operators.IsSystem = true;
                            }
                            else
                            {
                                operators.IsSystem = false;
                            }
                            OnlineUser.Operator.AddCurrent(operators);
                            //登录限制
                            //LoginLimit(username, operators.IPAddress, operators.IPAddressName);
                            //写入日志
                            logEntity.ExecuteResult = 1;
                            logEntity.Description = "登录成功";
                            logEntity.WriteLog();
                        }
                        return Content(new AjaxResult { state = ResultType.success.ToString(), message = "登录成功。", data = jsonResult.message }.ToJson());
                    }
                    else 
                    {
                        return Content(new AjaxResult { state = ResultType.error.ToString(), message = jsonResult.message }.ToJson());
                    }
                }
                else 
                {
                    return Content(new AjaxResult { state = ResultType.error.ToString(), message = "验证失败！" }.ToJson());
                }
                */
                #endregion

                #region 系统验证

                UserEntity userEntity = new UserBLL().CheckLogin(username, password);
                if (userEntity != null)
                {
                    PermissionBLL authorizeBLL = new PermissionBLL();
                    OrganizeBLL organizeBll = new OrganizeBLL();
                    OnlineUserModel operators = new OnlineUserModel();
                    operators.UserId = userEntity.Id;
                    operators.Account = userEntity.Account;
                    operators.UserName = userEntity.RealName;
                    operators.Email = userEntity.Email;
                    operators.Birthday = userEntity.Birthday;
                    operators.MobilePhone = userEntity.MobilePhone;
                    operators.Telephone = userEntity.Telephone;
                    operators.WeChat = userEntity.WeChat;
                    operators.WeChatID = userEntity.WeChatID;
                    operators.QQ = userEntity.QQ;
                    operators.Password = userEntity.Password;
                    operators.IsLeader = (userEntity.IsLeader.HasValue ? (bool)userEntity.IsLeader : false);
                    operators.IsReceiver = (userEntity.IsReceiver.HasValue ? (bool)userEntity.IsReceiver : false);
                    operators.IsPayer = (userEntity.IsPayer.HasValue ? (bool)userEntity.IsPayer : false);
                    operators.CompanyId = userEntity.OrganizeId;
                    var orgEntity = organizeBll.GetEntity(userEntity.OrganizeId);
                    operators.CompanyName = orgEntity.Name;
                    operators.CompanyCode = orgEntity.Code;
                    operators.DepartmentId = userEntity.DepartmentId;
                    orgEntity = organizeBll.GetEntity(userEntity.DepartmentId);
                    operators.DepartmentName = orgEntity.Name;
                    operators.DepartmentCode = orgEntity.Code;
                    operators.OrgName = organizeBll.GetAllParentOrgNames(orgEntity.BindId, true, "/");
                    string[] orgNameArr = operators.OrgName.Split('/');
                    if (orgNameArr.Length > 0) 
                    {
                        string deptNameAll = "";
                        for (int i = 0; i < orgNameArr.Length;i++ )
                        {
                            deptNameAll = deptNameAll + orgNameArr[i];
                        }
                        operators.DepNameAll = deptNameAll;
                    }
                    List<OrganizeEntity> orgList = organizeBll.GetUserParttimeDeptList(userEntity.Id);
                    if (orgList != null && orgList.Count > 0)
                    {
                        for (int i = 0; i < orgList.Count; i++)
                        {
                            if (i == orgList.Count - 1)
                            {
                                operators.PTDepartmentCode = operators.PTDepartmentCode + orgList[i].Code;
                                operators.PTDepartmentName = operators.PTDepartmentName + orgList[i].Name;
                            }
                            else
                            {
                                operators.PTDepartmentCode = operators.PTDepartmentCode + orgList[i].Code + ",";
                                operators.PTDepartmentName = operators.PTDepartmentName + orgList[i].Name + ",";
                            }
                        }

                    }
                    operators.IP = RequestHelper.Ip;
                    operators.IPLocal = RequestHelper.GetLocation(RequestHelper.Ip);
                    operators.System = RequestHelper.GetClientOSName();
                    operators.Browser = RequestHelper.Browser;
                    operators.IsMobile = RequestHelper.IsMobile();
                    RoleBLL roleBLL = new RoleBLL();
                    operators.RoleId = roleBLL.GetObjectStr(userEntity.Id, (int)AuthorizeTypeEnum.Role);
                    operators.RoleCode = roleBLL.GetUserRoleCode(userEntity.Id, (int)AuthorizeTypeEnum.Role);
                    operators.RoleName = roleBLL.GetUserRoleName(userEntity.Id, (int)AuthorizeTypeEnum.Role);
                    operators.PostId = userEntity.PostId;
                    operators.DutyId = userEntity.DutyId;
                    operators.LogTime = DateTime.Now;
                    //operators.Token = Guid.NewGuid().ToString();
                    operators.Token = Md5Helper.MD5(new Guid().ToString(), 16);
                    operators.SessionId = Session.SessionID;
                    operators.SessionStartTime = DateTime.Now;
                    operators.IsReplace = false;
                    //判断是否系统管理员
                    if (userEntity.IsAdministrator == true)
                    {
                        operators.IsSystem = true;
                    }
                    else
                    {
                        operators.IsSystem = false;
                    }

                    //获取用户功能权限
                    SysActionRightBLL sysActionRightBLL = new SysActionRightBLL();
                    List<OnLineUserRight> actionRight = new List<OnLineUserRight>();
                    string roleIds = userEntity.Id + "," + userEntity.PostId + "," + userEntity.DutyId + "," + operators.RoleId;
                    actionRight = sysActionRightBLL.GetUserAction(roleIds).ToList();
                    operators.ActionRight = actionRight;
 
                    OnlineUser.Operator.UpdateUserReplace(operators.UserId);
                    OnlineUser.Operator.AddCurrent(operators);
                    //写入日志
                    logEntity.ExecuteResult = 1;
                    logEntity.Description = "登录成功";
                    logEntity.WriteLog();
                    return Content(new AjaxResult { state = ResultType.success.ToString(), message = "登录成功。", data = operators.Token }.ToJson());
                }
                else
                {
                    return Content(new AjaxResult { state = ResultType.error.ToString(), message = "登录失败。" }.ToJson());
                }


                #endregion

            }
            catch (Exception ex)
            {
                CookieHelper.RemoveCookie("jfine_autologin");                  //清除自动登录
                logEntity.ExecuteResult = -1;
                logEntity.Description = ex.Message;
                logEntity.WriteLog();
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = ex.Message }.ToJson());
            }
        }
        
        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="verifycode">验证码</param>
        /// <param name="autologin">下次自动登录</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult CheckAdminLogin(string username, string password, string verifycode, int autologin)
        {
            LogEntity logEntity = new LogEntity();
            logEntity.Category = "登陆";
            logEntity.OperateType = "登陆";
            logEntity.CreateUserId = username;
            logEntity.CreateUserName = username;
            logEntity.Module = ConfigHelper.GetValue("SoftName");
            try
            {
                #region 验证码验证
                if (autologin == 0)
                {
                    verifycode = Md5Helper.MD5(verifycode.ToLower(), 16);
                    if (Session["session_verifycode"].IsEmpty() || verifycode != Session["session_verifycode"].ToString())
                    {
                        throw new Exception("验证码错误，请重新输入");
                    }
                }
                #endregion

                #region 验证中心验证数据
                /*
                HttpClient httpClient = new HttpClient();
                string url = ConfigHelper.GetValue("CAHost");;
                httpClient.BaseAddress = new Uri(url);
                HttpResponseMessage response = httpClient.GetAsync("api/CA/Login?account=" + username + "&password=" + password).Result;
                if (response.IsSuccessStatusCode)
                {
                    AjaxResult jsonResult = response.Content.ReadAsAsync<AjaxResult>().Result;
                    if (jsonResult.state.ToString() == ResultType.success.ToString())
                    {
                        UserEntity userEntity = jsonResult.data.ToString().ToObject<UserEntity>();
                        if (userEntity != null)
                        {
                            PermissionBLL authorizeBLL = new PermissionBLL();
                            OrganizeBLL organizeBll = new OrganizeBLL();
                            OnlineUserModel operators = new OnlineUserModel();
                            operators.UserId = userEntity.Id;
                            operators.Account = userEntity.Account;
                            operators.UserName = userEntity.RealName;
                            operators.Password = userEntity.Password;
                            operators.IsLeader = (userEntity.IsLeader.HasValue ? (bool)userEntity.IsLeader : false);
                            operators.CompanyId = userEntity.OrganizeId;
                            var orgEntity = organizeBll.GetEntity(userEntity.OrganizeId);
                            operators.CompanyName = orgEntity.Name;
                            operators.CompanyCode = orgEntity.Code;
                            operators.DepartmentId = userEntity.DepartmentId;
                            orgEntity = organizeBll.GetEntity(userEntity.DepartmentId);
                            operators.DepartmentName = orgEntity.Name;
                            operators.DepartmentCode = orgEntity.Code;
                            operators.IP = RequestHelper.Ip;
                            operators.IPLocal = RequestHelper.GetLocation(RequestHelper.Ip);
                            operators.System = RequestHelper.GetClientOSName();
                            operators.Browser = RequestHelper.Browser;
                            operators.IsMobile = RequestHelper.IsMobile();
                            operators.RoleId = new RoleBLL().GetObjectStr(userEntity.Id, (int)AuthorizeTypeEnum.Role);
                            operators.LogTime = DateTime.Now;
                            operators.Token = jsonResult.message;
                            //判断是否系统管理员
                            if (userEntity.IsAdministrator == true)
                            {
                                operators.IsSystem = true;
                            }
                            else
                            {
                                operators.IsSystem = false;
                            }
                            OnlineUser.Operator.AddCurrent(operators);
                            //登录限制
                            //LoginLimit(username, operators.IPAddress, operators.IPAddressName);
                            //写入日志
                            logEntity.ExecuteResult = 1;
                            logEntity.Description = "登录成功";
                            logEntity.WriteLog();
                        }
                        return Content(new AjaxResult { state = ResultType.success.ToString(), message = "登录成功。", data = jsonResult.message }.ToJson());
                    }
                    else 
                    {
                        return Content(new AjaxResult { state = ResultType.error.ToString(), message = jsonResult.message }.ToJson());
                    }
                }
                else 
                {
                    return Content(new AjaxResult { state = ResultType.error.ToString(), message = "验证失败！" }.ToJson());
                }
                */
                #endregion

                #region 系统验证

                UserEntity userEntity = new UserBLL().CheckLogin(username, password);
                if (userEntity != null)
                {
                    //if (userEntity.IsAdministrator == null || !((bool)userEntity.IsAdministrator))  
                    //{
                    //    throw new Exception("非法登陆！");
                    //}
                    PermissionBLL authorizeBLL = new PermissionBLL();
                    OrganizeBLL organizeBll = new OrganizeBLL();
                    OnlineUserModel operators = new OnlineUserModel();
                    operators.UserId = userEntity.Id;
                    operators.Account = userEntity.Account;
                    operators.UserName = userEntity.RealName;
                    operators.Email = userEntity.Email;
                    operators.Birthday = userEntity.Birthday;
                    operators.MobilePhone = userEntity.MobilePhone;
                    operators.Telephone = userEntity.Telephone;
                    operators.WeChat = userEntity.WeChat;
                    operators.WeChatID = userEntity.WeChatID;
                    operators.QQ = userEntity.QQ;
                    operators.Password = userEntity.Password;
                    operators.IsLeader = (userEntity.IsLeader.HasValue ? (bool)userEntity.IsLeader : false);
                    operators.IsReceiver = (userEntity.IsReceiver.HasValue ? (bool)userEntity.IsReceiver : false);
                    operators.IsPayer = (userEntity.IsPayer.HasValue ? (bool)userEntity.IsPayer : false);
                    operators.CompanyId = userEntity.OrganizeId;
                    var orgEntity = organizeBll.GetEntity(userEntity.OrganizeId);
                    operators.CompanyName = orgEntity.Name;
                    operators.CompanyCode = orgEntity.Code;
                    operators.DepartmentId = userEntity.DepartmentId;
                    orgEntity = organizeBll.GetEntity(userEntity.DepartmentId);
                    operators.DepartmentName = orgEntity.Name;
                    operators.DepartmentCode = orgEntity.Code;
                    operators.OrgName = organizeBll.GetAllParentOrgNames(orgEntity.BindId, true, "/");
                    string[] orgNameArr = operators.OrgName.Split('/');
                    if (orgNameArr.Length > 0) 
                    {
                        string deptNameAll = "";
                        for (int i = 0; i < orgNameArr.Length;i++ )
                        {
                            deptNameAll = deptNameAll + orgNameArr[i];
                        }
                        operators.DepNameAll = deptNameAll;
                    }
                    List<OrganizeEntity> orgList = organizeBll.GetUserParttimeDeptList(userEntity.Id);
                    if (orgList != null && orgList.Count > 0)
                    {
                        for (int i = 0; i < orgList.Count; i++)
                        {
                            if (i == orgList.Count - 1)
                            {
                                operators.PTDepartmentCode = operators.PTDepartmentCode + orgList[i].Code;
                                operators.PTDepartmentName = operators.PTDepartmentName + orgList[i].Name;
                            }
                            else
                            {
                                operators.PTDepartmentCode = operators.PTDepartmentCode + orgList[i].Code + ",";
                                operators.PTDepartmentName = operators.PTDepartmentName + orgList[i].Name + ",";
                            }
                        }

                    }
                    operators.IP = RequestHelper.Ip;
                    operators.IPLocal = RequestHelper.GetLocation(RequestHelper.Ip);
                    operators.System = RequestHelper.GetClientOSName();
                    operators.Browser = RequestHelper.Browser;
                    operators.IsMobile = RequestHelper.IsMobile();
                    RoleBLL roleBLL = new RoleBLL();
                    operators.RoleId = roleBLL.GetObjectStr(userEntity.Id, (int)AuthorizeTypeEnum.Role);
                    operators.RoleCode = roleBLL.GetUserRoleCode(userEntity.Id, (int)AuthorizeTypeEnum.Role);
                    operators.RoleName = roleBLL.GetUserRoleName(userEntity.Id, (int)AuthorizeTypeEnum.Role);
                    operators.LogTime = DateTime.Now;
                    //operators.Token = Guid.NewGuid().ToString();
                    operators.Token = Md5Helper.MD5(new Guid().ToString(), 16);
                    operators.SessionId = Session.SessionID;
                    operators.SessionStartTime = DateTime.Now;
                    operators.IsReplace = false;
                    //判断是否系统管理员
                    if (userEntity.IsAdministrator == true)
                    {
                        operators.IsSystem = true;
                    }
                    else
                    {
                        operators.IsSystem = false;
                    }
                    OnlineUser.Operator.UpdateUserReplace(operators.UserId);
                    OnlineUser.Operator.AddCurrent(operators);
                    //写入日志
                    logEntity.ExecuteResult = 1;
                    logEntity.Description = "登录成功";
                    logEntity.WriteLog();
                    return Content(new AjaxResult { state = ResultType.success.ToString(), message = "登录成功。", data = operators.Token }.ToJson());
                }
                else
                {
                    return Content(new AjaxResult { state = ResultType.error.ToString(), message = "登录失败。" }.ToJson());
                }


                #endregion

            }
            catch (Exception ex)
            {
                CookieHelper.RemoveCookie("jfine_autologin");                  //清除自动登录
                logEntity.ExecuteResult = -1;
                logEntity.Description = ex.Message;
                logEntity.WriteLog();
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = ex.Message }.ToJson());
            }
        }

        /// <summary>
        /// 切换系统登陆
        /// </summary>
        /// <param name="returnURL">子系统URL</param>
        /// <returns></returns>

        [HandlerLogin]
        public ActionResult CheckCrossLogin(string returnURL)
        {
            #region 验证中心验证数据

            HttpClient httpClient = new HttpClient();
            string url = ConfigHelper.GetValue("CAHost");
            httpClient.BaseAddress = new Uri(url);
            HttpResponseMessage response = httpClient.GetAsync("api/CA/GetToken?account=" + OnlineUser.Operator.GetCurrent().Account + "&password=" + OnlineUser.Operator.GetCurrent().Password).Result;
            if (response.IsSuccessStatusCode)
            {
                AjaxResult jsonResult = response.Content.ReadAsAsync<AjaxResult>().Result;
                if (jsonResult.state.ToString() == ResultType.success.ToString())
                {
                    if (returnURL.IndexOf("?") < 0)
                    {
                        returnURL = returnURL + "?token=" + jsonResult.message;
                    }
                    else
                    {
                        returnURL = returnURL + "&token=" + jsonResult.message;
                    }
                    return Redirect(returnURL);

                }
                else
                {
                    return Content(new AjaxResult { state = ResultType.error.ToString(), message = jsonResult.message }.ToJson());
                }
            }
            else
            {
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = "验证失败！" }.ToJson());
            }

            #endregion
        }
        #endregion

        #region 找回密码
        /// <summary>
        /// 获取修改密码的验证码
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="email">邮箱</param>
        /// <returns></returns>
        public ActionResult GetVerCode(string account, string email)
        {
            if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(email))
            {
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = "账号或者邮箱不能为空" }.ToJson());
            }
            string queryJson = "{Account:\"" + account + "\",Email:\"" + email + "\"}";
            UserEntity userEntity = new UserBLL().GetFormByQuery(queryJson);
            if (userEntity != null)
            {
                //生成五位随机字符
                string randomCode = "";
                randomCode = CommonHelper.GetCheckCode(5);
                Session["ValidateBackCode"] = randomCode;
                Session["ValidateBackAccount"] = account;
                //发送邮件验证码
                string content = ConfigHelper.GetValue("VailCode");
                content = content.Replace("CODE", randomCode);
                MailHelper.SendByThread(email, "【河钢新材】", "找回账号密码", content);
                return Content(new AjaxResult { state = ResultType.success.ToString(), message = "验证码已发设定邮箱" }.ToJson());
            }
            else
            {
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = "没有被修改的相关信息" }.ToJson());
            }
        }

        /// <summary>
        /// 验证验证码
        /// </summary>
        /// <param name="account"></param>
        /// <param name="email"></param>
        /// <param name="searchcode"></param>
        /// <returns></returns>
        public ActionResult ValidateCode(string account, string email, string searchcode)
        {
            if (ValidateUtil.IsEmail(email))
            {
                if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(searchcode))
                {
                    return Content(new AjaxResult { state = ResultType.error.ToString(), message = "账号或者验证码不能为空" }.ToJson());
                }
                else
                {
                    string queryJson = "{Account:\"" + account + "\",Email:\"" + email + "\"}";
                    UserEntity userEntity = new UserBLL().GetFormByQuery(queryJson);
                    if (userEntity != null)
                    {
                        string code = Session["ValidateBackCode"].ToString().ToLower();
                        if (code == searchcode.ToLower().Trim())
                        {
                            return Content(new AjaxResult { state = ResultType.success.ToString(), message = account }.ToJson());
                        }
                        else
                        {
                            return Content(new AjaxResult { state = ResultType.error.ToString(), message = "验证码不正确" }.ToJson());
                        }
                    }
                    else
                    {
                        return Content(new AjaxResult { state = ResultType.error.ToString(), message = "没有被修改的相关信息" }.ToJson());
                    }
                }
            }
            else
            {
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = "邮箱格式不正确" }.ToJson());
            }
        }

        //修改密码
        public ActionResult modifyPW()
        {
            //1、数据获取
            string account = Request["account"] == null ? "" : Request["account"];
            string search_code = Request["search_code"] == null ? "" : Request["search_code"];
            string newPW = Request["newPW"] == null ? "" : Request["newPW"];

            string code_s = Session["ValidateBackCode"].ToString().ToLower();
            string account_s = Session["ValidateBackAccount"].ToString().ToLower();
            if (code_s == search_code.ToLower().Trim() && account == account_s.Trim())
            {
                UserBLL userBll = new UserBLL();
                string queryJson = "{Account:\"" + account + "\"}";
                UserEntity userEntity = userBll.GetFormByQuery(queryJson);
                if (userEntity != null)
                {
                    userBll.ModifyPassword(userEntity.Id, newPW);
                    return Content(new AjaxResult { state = ResultType.success.ToString(), message = account }.ToJson());
                }
                else
                {
                    return Content(new AjaxResult { state = ResultType.error.ToString(), message = "没有相关的账号信息！" }.ToJson());
                }


            }
            else
            {
                return Content(new AjaxResult { state = ResultType.error.ToString(), message = "验证码不正确！" }.ToJson());
            }
        }

        #endregion

    }
}