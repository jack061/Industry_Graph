using JFine.Busines.SystemManage;
using JFine.Code.Online;
using JFine.Common.Data;
using JFine.Common.Json;
using JFine.Domain.Models.SystemManage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using JFine.Web.Base.MVC.Handler;

namespace JFine.Web.Controllers
{
    public class ClientsDataController : Controller
    {
        private PermissionBLL permissionBLL = new PermissionBLL();

        /// <summary>
        /// 获取客户相关信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetClientsDataJson()
        {
            var userId = OnlineUser.Operator.GetCurrent().UserId;
            var data = new
            {
                user = GetUserData(userId),//用户
                authorizeMenu = this.ToMenuJson(permissionBLL.GetModuleList(userId).ToList(), "0"),//导航菜单
                authorizeButton = this.GetMenuButtonList(userId)
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// 获取用户数据
        /// </summary>
        /// <returns></returns>
        private object GetUserData(string userId)
        {
            UserBLL userBll = new UserBLL();
            string queryJson = "{\"userId\":\"" + userId + "\"}";
            DataTable dt_user = userBll.GetTable(queryJson);
            if (DataTableHelper.IsExistRows(dt_user))
            {
                return JsonHelper.DataRowToJson(dt_user.Rows[0]);
            }
            else
            {
                return "{}";
            }

        }

        /// <summary>
        /// 获取功能菜单
        /// </summary>
        /// <returns></returns>
        private string ToMenuJson(List<ModuleEntity> data, string parentId)
        {
            StringBuilder sbJson = new StringBuilder();
            sbJson.Append("[");
            List<ModuleEntity> entitys = data.FindAll(t => t.BindId == parentId);
            if (entitys.Count > 0)
            {
                foreach (var item in entitys)
                {
                    string strJson = item.ToJson();
                    strJson = strJson.Insert(strJson.Length - 1, ",\"ChildNodes\":" + ToMenuJson(data, item.Id) + "");
                    sbJson.Append(strJson + ",");
                }
                sbJson = sbJson.Remove(sbJson.Length - 1, 1);
            }
            sbJson.Append("]");
            return sbJson.ToString();
        }
        /// <summary>
        /// 获取授权按钮
        /// </summary>
        /// <returns></returns>
        private object GetMenuButtonList(string userId)
        {
            List<ModuleOperateEntity> data = permissionBLL.GetModuleButtonList(userId).ToList();
            return data;
        }
    }
}