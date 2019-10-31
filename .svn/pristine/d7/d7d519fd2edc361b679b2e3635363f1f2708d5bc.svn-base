
//------------------------------------------------------------------------------
//     此代码由T4模板自动生成
//	   生成时间 2017-08-01 15:57:32
//    ©为之团队
//------------------------------------------------------------------------------
using JFine.Busines.SystemManage;
using JFine.Common.UI;
using JFine.Common.Json;
using JFine.Domain.Models.SystemManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Collections;
using JFine.Util;
using JFine.Code.Online;
using JFine.Common.DotNetImage;
using System.Drawing;
using JFine.Common.Data;
using JFine.Busines.SysManage;
using JFine.Web.Base.MVC.Handler;
using JFine.Common.Encrypt;

namespace JFine.Web.Areas.SysManage.Controllers
{	
	/// <summary>
	/// UserController
	/// </summary>	
    public class UserController : JFControllerBase
	{
		 private UserBLL userBll = new UserBLL();
         //private UserLogOnApp userLogOnApp = new UserLogOnApp();

        #region View
        //用户列表
        // GET: /SysManage/Organize/
        public override ActionResult Index()
        {
            return View();
        }
        
        //分配角色
        // GET: /SysManage/Organize/
        public ActionResult DistributeRole()
        {
            ViewBag.Id = Request["keyValue"];
            return View();
        }

        //分配部门
        // GET: /SysManage/User/
        public ActionResult DistributeDept()
        {
            ViewBag.Id = Request["keyValue"];
            return View();
        }
        
        //分配采购组织
        // GET: /SysManage/User/
        public ActionResult DistributeBidOrg()
        {
            ViewBag.Id = Request["keyValue"];
            return View();
        }
        
        //根据角色查看人员
        // GET: /SysManage/User/
        public ActionResult RoleUserIndex()
        {
            //分类(1:角色；2：职位；3：岗位；4：工作组)
            ViewBag.category = Request["category"];
            return View();
        }

        /// <summary>
        /// 选择人员
        /// </summary>
        /// <returns></returns>
        public ActionResult ChooseUser() 
        {
            return View();
        }
        
        /// <summary>
        /// 选择人员
        /// </summary>
        /// <returns></returns>
        public ActionResult ChooseUser2() 
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
        /// 用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Info()
        {
            return View();
        }

        /// <summary>
        /// 用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Info2()
        {
            var keyValue = Request["keyValue"] ?? "";
            var data = userBll.GetForm(keyValue);
            return View(data);
        }

        /// <summary>
        /// 用户修改信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult UserAlter()
        {
            var keyValue = Request["keyValue"] ?? "";
            return View();
        }
        
        /// <summary>
        /// 在线用户列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult OnlinerIndex()
        {
            OnlineUserModel onliner = OnlineUser.Operator.GetCurrent();
            if(onliner.IsSystem)
            {
                return View();
            }
            return Content("当前用户权限不足");
            
        }

        #endregion

        #region 数据获取
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string queryJson)
        {
            var data = new
            {
                rows = userBll.GetPageList(pagination, queryJson),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
        
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJsonRPD(Pagination pagination, string queryJson)
        {
            var data = new
            {
                rows = userBll.GetListByRPD(pagination, queryJson),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetPageList_DT(Pagination pagination, string queryJson)
        {
            DataTable dt = userBll.GetPageList_DT(pagination, queryJson);
            var data = new
            {
                rows = dt,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// 树形用户数据列表
        /// </summary>
        /// <param name="displayTyle">显示类型：组织、角色</param>
        /// <param name="orgcode">组织编码，多个用逗号(,)隔开</param>
        /// <param name="rolecode">角色编码，多个用逗号(,)隔开</param>
        /// <param name="isLeader">是否只显示领导</param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetChooseTreeJson(string displayTyle = "role", string orgCode = "", string roleCode = "", string isLeader = "")
        {
            var treeList = new List<TreeViewModel>();
            string queryJson = new
            {
                orgCode = orgCode,
                roleCode = roleCode,
                isLeader = isLeader
            }.ToJson();


            if (displayTyle == "role")
            {
                #region 角色
                IEnumerable<UserExpand> userList = userBll.GetList(queryJson);
                userList = userList.Where(t => t.IsAdministrator == false);
                RoleBLL roleBll = new RoleBLL();
                var data = roleBll.GetList("{}");

                TreeViewModel treeroot = new TreeViewModel();
                treeroot.id = "role_root";
                treeroot.text = "角色";
                treeroot.value = "角色";
                treeroot.parentId = "0";
                treeroot.showcheck = true;
                treeroot.checkstate = 0;
                treeroot.isexpand = true;
                treeroot.complete = true;
                treeroot.expand1 = "0";//扩展数据类型0：角色，1：人员
                treeroot.hasChildren = true;
                treeList.Add(treeroot);

                foreach (RoleEntity item in data)
                {
                    TreeViewModel tree = new TreeViewModel();
                    bool hasChildren = userList.Count(t => t.RoleId == item.Id) == 0 ? false : true;
                    tree.id = item.Id;
                    tree.text = item.Name;
                    tree.value = item.Code;
                    tree.parentId = "role_root";
                    tree.showcheck = true;
                    tree.checkstate = 0;
                    tree.isexpand = true;
                    tree.complete = true;
                    tree.expand1 = "0";//扩展数据类型0：角色，1：人员
                    tree.hasChildren = hasChildren;
                    treeList.Add(tree);
                    if (hasChildren)
                    {
                        IEnumerable<UserExpand> userTreeList = userList.Where(t => t.RoleId == item.Id);
                        OrganizeBLL organizeBll = new OrganizeBLL();
                        foreach (UserExpand itemUser in userTreeList)
                        {
                            TreeViewModel userTree = new TreeViewModel();
                            userTree.id = itemUser.Id;
                            userTree.text = itemUser.RealName;
                            userTree.value = itemUser.Account;
                            userTree.parentId = item.Id;
                            userTree.showcheck = true;
                            userTree.checkstate = 0;
                            userTree.isexpand = true;
                            userTree.complete = true;
                            userTree.img = (itemUser.IsLeader.HasValue && (bool)itemUser.IsLeader) ? "fa-user-leader" : "fa-user-ordinary";
                            userTree.expand1 = "1";//扩展数据类型0：角色，1：人员
                            userTree.expand2 = itemUser.OrganizeId;//组织ID
                            var orgEntity = organizeBll.GetEntity(itemUser.OrganizeId);
                            userTree.expand3 = orgEntity.Name;//组织名称
                            userTree.expand4 = itemUser.DepartmentId;//部门id
                            orgEntity = organizeBll.GetEntity(itemUser.DepartmentId);
                            userTree.expand5 = orgEntity.Name;//部门名称
                            userTree.expand6 = itemUser.MobilePhone;//手机
                            userTree.expand7 = itemUser.Email;//邮箱
                            userTree.expand8 = itemUser.IsLeader.ToString();//是否为领导
                            userTree.expand9 = itemUser.IsPartTime.ToString();//是否为兼职
                            userTree.hasChildren = false;
                            treeList.Add(userTree);
                        }

                    }
                }

                #endregion

            }
            else
            {
                #region 组织
                IEnumerable<UserExpand> userList = userBll.GetList(queryJson, "dept");
                userList = userList.Where(t => t.IsAdministrator == false);
                OrganizeBLL organizeBll = new OrganizeBLL();
                var data = organizeBll.GetList();
                if (!string.IsNullOrEmpty(orgCode))
                {
                    string[] orgCodeArr = orgCode.Split(',');
                    data = data.Where(t => orgCodeArr.All(p => !string.IsNullOrEmpty(p) && t.Code.StartsWith(p)));

                }

                foreach (OrganizeEntity item in data)
                {
                    TreeViewModel tree = new TreeViewModel();
                    bool hasChildren = data.Count(t => t.BindId == item.Id) == 0 ? false : true;
                    bool hasUser = userList.Count(t => t.DepartmentId == item.Id) == 0 ? false : true;
                    tree.id = item.Id;
                    tree.text = item.Name;
                    tree.value = item.Code;
                    tree.parentId = item.BindId;
                    tree.showcheck = true;
                    tree.checkstate = 0;
                    tree.isexpand = (item.BindId == "0" ? true : false);
                    tree.complete = true;
                    tree.expand1 = "0";//扩展数据类型0：组织，1：人员
                    tree.hasChildren = (hasChildren || hasUser);
                    treeList.Add(tree);
                    if (hasUser)
                    {
                        IEnumerable<UserExpand> userTreeList = userList.Where(t => t.DepartmentId == item.Id);
                        foreach (UserExpand itemUser in userTreeList)
                        {
                            TreeViewModel userTree = new TreeViewModel();
                            userTree.id = itemUser.Id;
                            userTree.text = itemUser.RealName;
                            userTree.value = itemUser.Account;
                            userTree.parentId = item.Id;
                            userTree.showcheck = true;
                            userTree.checkstate = 0;
                            userTree.isexpand = true;
                            userTree.complete = true;
                            userTree.img = (itemUser.IsPartTime.HasValue && itemUser.IsPartTime == 1) ? "fa-user-parttime" : (itemUser.IsLeader.HasValue && (bool)itemUser.IsLeader) ? "fa-user-leader" : "fa-user-ordinary";
                            userTree.expand1 = "1";
                            userTree.expand2 = itemUser.OrganizeId;//组织ID
                            var orgEntity = organizeBll.GetEntity(itemUser.OrganizeId);
                            userTree.expand3 = orgEntity.Name;//组织名称
                            userTree.expand4 = itemUser.DepartmentId;//部门id
                            userTree.expand5 = item.Name;//部门名称
                            userTree.expand6 = itemUser.MobilePhone;//手机
                            userTree.expand7 = itemUser.Email;//邮箱
                            userTree.expand8 = itemUser.IsLeader.ToString();//是否为领导
                            userTree.expand9 = itemUser.IsPartTime.ToString();//是否兼职
                            userTree.hasChildren = false;
                            treeList.Add(userTree);
                        }

                    }
                }

                #endregion
            }

            return Content(treeList.TreeViewJson());
        }

        /// <summary>
        /// 功能实体 返回对象Json
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = userBll.GetForm(keyValue);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 功能实体 返回对象Json
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetExpandUser(string keyValue)
        {
            var data = userBll.GetExpandForm(keyValue);
            return Content(data.ToJson());
        }

        /// <summary>
        /// 用户所在采购组织
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
         [HttpGet]
        public ActionResult GetPurchaseOrgListJoinUser(string userId)
        {
            DataTable dt = userBll.GetPurchaseOrgListJoinUser(userId);
            var data = new
            {
                rows = dt,
                total = DataTableHelper.IsExistRows(dt) ? dt.Rows.Count : 0
            };
            return Content(data.ToJson());
        }
        
        /// <summary>
        /// 获取在线用户列表
        /// </summary>
        /// <returns></returns>
         [HttpGet]
         public ActionResult GetOnlinerAll(Pagination pagination, string queryJson)
        {
            var data = new
            {
                rows = userBll.GetOnlinerAll(pagination, queryJson),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
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
            userBll.DeleteForm(keyValue);
            return Success("删除成功。");
        }

        /// <summary>
        /// 保存功能表单
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="UserEntity">功能实体</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult SaveForm(string keyValue, UserEntity userEntity)
        {
            userBll.SaveForm(keyValue, userEntity);
            return Success("保存成功。");
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(string keyValue)
        {

            userBll.ResetPassword(keyValue);
            return Success("重置密码成功。");
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="newPW"></param>
        /// <param name="oldPW"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult ModifyPassword(string newPW, string oldPW)
        {
            OnlineUserModel onliner = OnlineUser.Operator.GetCurrent();
            userBll.ModifyPassword(onliner.UserId, newPW, oldPW);
            return Success("修改密码成功。");
        }
        /// <summary>
        /// 禁用
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult DisabledAccount(string keyValue)
        {
            userBll.UpdateState(keyValue,0);
            return Success("账户禁用成功。");
        }
        /// <summary>
        /// 启用
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult EnabledAccount(string keyValue)
        {
            userBll.UpdateState(keyValue, 1);
            return Success("账户启用成功。");
        }
        
        /// <summary>
        /// 解锁
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult UnlockAccount(string keyValue)
        {
            userBll.UnlockAccount(keyValue);
            return Success("账户解锁成功。");
        }

        /// <summary>
        /// 修改头像
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public JsonResult alterHeadImage(string keyValue)
        {
            Hashtable ht_result = new Hashtable();
            //1、数据获取
            //data:image/bmp;base64,
            string dataTemp = Request["data"] ?? "";
            string data = "";
            int index = dataTemp.IndexOf(",");
            if (index > 0)
            {
                data = dataTemp.Substring(index + 1);
            }

            try
            {
                string savePath = Server.MapPath(ConstantUtil.HEADIMGE_URL);//Server.MapPath 获得虚拟服务器相对路径
                string fileName = OnlineUser.Operator.GetCurrent().Account + ".png";
                bool flag = ImageHelper.Base64ToImage(data, savePath, fileName);
                if (flag)
                {

                    using (Image i = new Bitmap(savePath + fileName))
                    {
                        Bitmap b = new Bitmap(i.Width, i.Height);
                        using (Graphics g = Graphics.FromImage(b))
                        {
                            g.FillEllipse(new TextureBrush(i), 0, 0, i.Width, i.Height);
                        }
                        fileName = OnlineUser.Operator.GetCurrent().Account + "_s.png";
                        flag = ImageHelper.saveBitmap(b, savePath, fileName);
                    }
                    if (flag)
                    {
                        ht_result.Add("status", "T");
                        ht_result.Add("msg", "操作成功！");
                    }
                    else
                    {
                        ht_result.Add("status", "F");
                        ht_result.Add("msg", "操作失败！");
                    }

                }
                else
                {
                    ht_result.Add("status", "F");
                    ht_result.Add("msg", "操作失败！");
                }
            }
            catch (Exception ex)
            {
                ht_result.Add("status", "F");
                ht_result.Add("msg", "base64编码的文本转为图片出现异常或者保存异常！请联系系统管理员！");
            }

            return Json(ht_result.ToJson());
        }

        /// <summary>
        /// 保存用户招标组织
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="roleListJson">功能实体</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult SaveUserPurchaseOrg(string keyValue, string orgListJson)
        {
            UserPurchaseOrgBLL userPurchaseOrgBLL = new UserPurchaseOrgBLL();
            userPurchaseOrgBLL.SaveUserPurOrg(keyValue, orgListJson);
            return Success("保存成功。");
        }

        /// <summary>
        /// 保存用户审核供应商采购类别
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="typeListJson">功能实体</param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult SaveUserPurchaseType(string keyValue, string typeListJson)
        {
            UserPurchaseTypeBLL userPurchaseTypeBLL = new UserPurchaseTypeBLL();
            userPurchaseTypeBLL.SaveUserPurType(keyValue, typeListJson);
            return Success("保存成功。");
        }

        /// <summary>
        /// 屏幕锁定
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult LockScreen()
        {
            OnlineUserModel onLiner = OnlineUser.Operator.GetCurrent();
            if (!onLiner.IsLockScreen)
            {
                onLiner.IsLockScreen = true;
                OnlineUser userOperate = new OnlineUser();
                userOperate.AddCurrent(onLiner);
            }
            return Success("屏幕已锁定。");
        }

        /// <summary>
        /// 屏幕解锁
        /// </summary>
        /// <param name="ScreenPW"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [HandlerAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult UnLockScreen(string ScreenPW)
        {
            OnlineUserModel onLiner = OnlineUser.Operator.GetCurrent();
            if (onLiner.IsLockScreen)
            {
                ScreenPW = Md5Helper.MD5(DESEncrypt.Encrypt(ScreenPW.ToLower()).ToLower(), 32).ToLower();
                if (onLiner.Password == ScreenPW)
                {
                    onLiner.IsLockScreen = false;
                    OnlineUser userOperate = new OnlineUser();
                    userOperate.AddCurrent(onLiner);
                    return Success("屏幕已解锁。");
                }
                else
                {
                    return Error("密码不正确。");
                }

            }
            else
            {
                return Success("屏幕已解锁。");

            }

        }

        
        #endregion

        #region 数据验证
       
        #endregion
    }
}