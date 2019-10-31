using JFine.Code.Online;
using JFine.Common.UI;
using JFine.Web.Base.MVC.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JFine.Common.Json;
using JFine.Common.Extend;
using JFine.Plugin;
using System.Collections;
using JFine.Common.Code;
using System.IO;
using JFine.Common.DotNetFile;
using JFine.Common.Web;

namespace JFine.Web.Areas.SysManage.Controllers
{
    [HandlerIsSystem]
    /// <summary>
    /// 插件管理
    /// </summary>
    public class PluginController : JFControllerBase
    {
        // GET: SysManage/ServerInfo
        public override ActionResult Index()
        {
            OnlineUserModel onliner = OnlineUser.Operator.GetCurrent();
            if (onliner.IsSystem)
            {
                return View();
            }
            return Content("当前用户权限不足");

        }
        /// <summary>
        /// 安装插件
        /// </summary>
        /// <returns></returns>
        public ActionResult InstallForm()
        {
            OnlineUserModel onliner = OnlineUser.Operator.GetCurrent();
            if (onliner.IsSystem)
            {
                return View();
            }
            return Content("当前用户权限不足");

        }

        /// <summary>
        /// 编辑插件信息
        /// </summary>
        /// <returns></returns>
        public ActionResult EditForm(string pluginName)
        {
            OnlineUserModel onliner = OnlineUser.Operator.GetCurrent();
            if (onliner.IsSystem)
            {
                PluginModel pluginModel = GetPluginFromName(pluginName);
                return View(pluginModel);
            }
            return Content("当前用户权限不足");

        }

        #region 数据获取
        /// <summary>
        /// 列表 
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(string queryJson)
        {
            List<PluginModel> pluginList = new List<PluginModel>();
            OnlineUserModel onliner = OnlineUser.Operator.GetCurrent();
            if (onliner.IsSystem)
            {
                pluginList = PluginManager.ReferencedPlugins.ToList();
                var queryParam = queryJson.ToJObject();
                //查询条件
                if (!queryParam["keyword"].IsEmpty())
                {
                    string keyword = queryParam["keyword"].ToString();
                    pluginList = pluginList.Where(t => t.PluginName.Contains(keyword)).ToList();
                }
            }
            
            var data = new
            {
                rows = pluginList,
                total = pluginList.Count
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// 根据插件名称获取插件信息
        /// </summary>
        /// <param name="pluginName">插件名称</param>
        /// <returns></returns>
        private PluginModel GetPluginFromName(string pluginName) 
        {
            PluginModel pluginModel = new PluginModel();
            List<PluginModel> pluginList = new List<PluginModel>();
            pluginList = PluginManager.ReferencedPlugins.ToList();
            pluginModel = pluginList.FirstOrDefault(t => t.PluginName == pluginName);
            return pluginModel;
        }

        #endregion

        #region 数据处理
        /// <summary>
        /// 上传文件 Bootstrap File Input 
        /// </summary>
        /// <returns></returns>
        public JsonResult UploadPlugin()
        {
            Hashtable ht_result = new Hashtable();

            OnlineUserModel onliner = OnlineUser.Operator.GetCurrent();
            if (onliner.IsSystem)
            {
                try
                {
                    HttpPostedFileBase file = Request.Files["uploadBF"];
                    if (file != null)
                    {
                        string oldFileName = file.FileName;//原文件名
                        int size = file.ContentLength;//附件大小
                        string extenstion = oldFileName.Substring(oldFileName.LastIndexOf(".") + 1);//后缀名
                        string fileNameUnExt = oldFileName.Substring(0,oldFileName.LastIndexOf("."));//排除后缀名
                        if (extenstion.ToLower() != "zip" && extenstion.ToLower() != "rar") 
                        {
                            ht_result.Add("status", "F");
                            ht_result.Add("msg", "请选择压缩包文件。");
                            return Json(ht_result);
                        }
                        string filename = DateTimeHelper.GetToday("yyyyMMddHHmmssfff") + "_" + oldFileName; //文件重命名
                        string savePath = Server.MapPath(@"\Content\Files\Plugin\" + DateTimeHelper.GetToday("yyyyMMdd") + @"\");//Server.MapPath 获得虚拟服务器相对路径
                        string saveFullPath = savePath + filename;//文件路径
                        if (!(Directory.Exists(savePath)))
                        {//判断路径是否存在---不存在创建路径
                            Directory.CreateDirectory(savePath);
                        }
                        if ((System.IO.File.Exists(saveFullPath)))
                        {//判断文件是否已经存在，存在删除
                            System.IO.File.Delete(saveFullPath);
                        }

                        file.SaveAs(saveFullPath);
                        bool uploaded = System.IO.File.Exists(saveFullPath);
                        if (uploaded) 
                        {
                            try
                            {
                                string virtualTargetPath = @"\Plugins\" + fileNameUnExt;//相对路径
                                string targetPath = Server.MapPath(virtualTargetPath);//Server.MapPath 获得虚拟服务器相对路径
                                if (!(Directory.Exists(targetPath)))
                                {//判断路径是否存在---不存在创建路径
                                    Directory.CreateDirectory(targetPath);
                                }
                                GZipHelper.DecompressFile(saveFullPath, targetPath,true);
                                PluginManager.MarkPluginAsNewVersion(virtualTargetPath);
                            }
                            catch (Exception e)
                            {
                                ht_result.Add("status", "F");
                                ht_result.Add("msg", e.Message);
                                return Json(ht_result);
                            }

                            ht_result.Add("status", "T");
                            ht_result.Add("msg", "上传成功！");
                            ht_result.Add("filename_o", oldFileName);
                            ht_result.Add("filename", filename);
                            ht_result.Add("path", @"\Content\Files\Plugin\" + DateTimeHelper.GetToday("yyyyMMdd") + @"\");
                            return Json(ht_result);
                        }
                       
                    }
                    ht_result.Add("status", "F");
                    ht_result.Add("msg", "加载文件失败");
                    return Json(ht_result);
                }
                catch (Exception ex)
                {
                    ht_result.Add("status", "F");
                    ht_result.Add("msg", "加载文件失败:" + ex.ToString());
                    return Json(ht_result);
                }
            }
            else 
            {
                ht_result.Add("status", "F");
                ht_result.Add("msg", "当前用户权限不足");
            }
            return  Json(ht_result);
            
        }

        /// <summary>
        /// 修改插件信息
        /// </summary>
        /// <param name="PluginModel">pluginModel</param>
        /// <returns></returns>
        public ActionResult SaveForm(PluginModel pluginModel) 
        {
            if (pluginModel == null) 
            {
                return Error("所修改插件不存在！");
            }

            PluginModel currentPlugin = GetPluginFromName(pluginModel.PluginName);

            if (currentPlugin == null) 
            {
                return Error("所修改插件不存在！");
            }
            currentPlugin.Group = pluginModel.Group;
            currentPlugin.FriendlyName = pluginModel.FriendlyName;
            currentPlugin.Version = pluginModel.Version;
            currentPlugin.Author = pluginModel.Author;
            currentPlugin.Description = pluginModel.Description;
            try
            {
                PluginManager.SavePluginInfo(currentPlugin);
                return Success("操作成功！");
            }
            catch (Exception ex) 
            {
                return Error("操作失败："+ ex.Message);
            }


        }

        /// <summary>
        /// 安装插件
        /// </summary>
        /// <param name="pluginName"></param>
        /// <returns></returns>
        public ActionResult InstallPlugin(string pluginName) 
        {
            Hashtable ht_result = new Hashtable();

            OnlineUserModel onliner = OnlineUser.Operator.GetCurrent();
            if (onliner.IsSystem)
            {
                try
                {
                    PluginManager.MarkPluginAsInstalled(pluginName);
                }
                catch (Exception e)
                {
                    return Error(e.Message);
                }
                return Success("操作成功！");
            }
            else
            {
                return Error("当前用户权限不足");
            }
        }

        /// <summary>
        /// 启用插件
        /// </summary>
        /// <param name="pluginName"></param>
        /// <returns></returns>
        public ActionResult StartPlugin(string pluginName)
        {
            Hashtable ht_result = new Hashtable();

            OnlineUserModel onliner = OnlineUser.Operator.GetCurrent();
            if (onliner.IsSystem)
            {
                try
                {
                    PluginManager.MarkPluginAsUsable(pluginName);
                }
                catch (Exception e)
                {
                    return Error(e.Message);
                }

                return Success("操作成功！");
            }
            else
            {
                return Error("当前用户权限不足");
            }
        }

        /// <summary>
        /// 停用插件
        /// </summary>
        /// <param name="pluginName"></param>
        /// <returns></returns>
        public ActionResult StopPlugin(string pluginName)
        {
            OnlineUserModel onliner = OnlineUser.Operator.GetCurrent();
            if (onliner.IsSystem)
            {
                try
                {
                    PluginManager.MarkPluginAsUnUsable(pluginName);
                }
                catch (Exception e)
                {
                    return Error(e.Message);
                }
                return Success("操作成功！");
            }
            else
            {
                return Error("当前用户权限不足");
            }
        }
        
        /// <summary>
        /// 重启应用
        /// </summary>
        /// <param name="pluginName"></param>
        /// <returns></returns>
        public ActionResult RestartApplication()
        {
            OnlineUserModel onliner = OnlineUser.Operator.GetCurrent();
            if (onliner.IsSystem)
            {
                try
                {
                    RequestHelper.RestartApplication();
                }
                catch (Exception e)
                {
                    return Error(e.Message);
                }
                return Success("操作成功！");
            }
            else
            {
                return Error("当前用户权限不足");
            }
        } 
        
        /// <summary>
        /// 删除插件
        /// </summary>
        /// <param name="pluginName"></param>
        /// <returns></returns>
        public ActionResult DeletePlugin(string pluginName)
        {
            OnlineUserModel onliner = OnlineUser.Operator.GetCurrent();
            if (onliner.IsSystem)
            {
                try
                {
                    PluginManager.MarkPluginAsUninstalled(pluginName);
                }
                catch (Exception e)
                {
                    return Error(e.Message);
                }
                return Success("操作成功！");
            }
            else
            {
                return Error("当前用户权限不足");
            }
        }

        #endregion
    }
}