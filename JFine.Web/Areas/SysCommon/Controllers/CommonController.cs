using JFine.Common.Code;
using JFine.Common.Data;
using JFine.Common.Json;
using JFine.Common.Offices;
using JFine.Data.EL;
using JFine.Sequence;
using JFine.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using JFine.Web.Base.MVC.Handler;
using JFine.Common.DotNetFile;

namespace JFine.Web.Areas.SysCommon.Controllers
{
    public class CommonController : JFControllerBase2
    {
        #region 视图
        //
        // GET: /SysCommon/Common/
        public override ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 功能图标
        /// </summary>
        /// GET: /SysCommon/Common/
        /// <returns></returns>
        [HttpGet]
        public ActionResult Icon()
        {
            return View();
        }
        
        /// <summary>
        /// 上传Excel数据
        /// </summary>
        /// GET: /SysCommon/Common/
        /// <returns></returns>
        [HttpGet]
        [HandlerLogin]
        public ActionResult UpLoadExcelData()
        {
            return View();
        }
        
        /// <summary>
        /// 通过bootstrap file input 上传文件
        /// </summary>
        /// GET: /SysCommon/Common/
        /// <returns></returns>
        [HttpGet]
        [HandlerLogin]
        public ActionResult UpLoadBF()
        {
            return View();
        }

        /// <summary>
        /// 背景图片
        /// </summary>
        /// GET: /SysCommon/Common/
        /// <returns></returns>
        [HttpGet]
        [HandlerLogin]
        public ActionResult Background()
        {
            return View();
        }
        #endregion

        #region 数据获取
        public ActionResult GetServerTime() 
        {//获取服务器时间
            Hashtable ht_result = new Hashtable();

            ht_result.Add("status", "T");
            ht_result.Add("msg", DateTimeHelper.ShortDateTimeS);

            return Json(ht_result); 
        }



        /// <summary>
        /// 获取背景图片
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        [HandlerLogin]
        public ActionResult GetBackgroundList()
        {

            var result = new List<BackgroundImage>();
            var files = Directory.GetFiles(Server.MapPath(ConstantUtil.FILE_BACKGROUND_URL));
            foreach (var file in files)
            {
                BackgroundImage image = new BackgroundImage();
               
                string fileName = file.Substring(file.LastIndexOf("\\") + 1);
                image.Src = ConstantUtil.FILE_BACKGROUND_URL + fileName;
                image.Name = fileName;
                image.Name_ = fileName.Substring(0, fileName.LastIndexOf("."));
                int index = fileName.IndexOf("_");
                if (index > 0)
                {
                    image.OriginName = fileName.Substring(index + 1);
                }
                else 
                {
                    image.OriginName = fileName;
                }
                result.Add(image);
            }
            return Content(result.ToJson());
        }

        // <summary>
        /// 删除背景图片
        /// </summary>
        /// <param name="Ids">Ids</param>
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult DeleteBGImage(string name)
        {
            string path = Server.MapPath(ConstantUtil.FILE_BACKGROUND_URL);
            path = path + name;
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
                return Success("删除成功。");
            }
            else 
            {
                return Error("该文件已经不存在。");
            }
            
        }

        /// <summary>
        /// 文字转声音
        /// </summary>
        public void GetVoice() 
        {
            string text= Request["voice"]?? "";
            Response.ContentType = "application/wav";
            using (MemoryStream ms = new MemoryStream())
            {
                Thread thread = new Thread(() =>
                {
                    SpeechSynthesizer synth = new SpeechSynthesizer();
                    try
                    {
                        synth.Rate = 0;
                        synth.Volume = 80;
                       
                        /*输出到文件
                        string savePath = Server.MapPath(ConstantUtil.FILE_VOICE_URL);//Server.MapPath 获得虚拟服务器物理路径
                        string saveFullPath = savePath + "voice.wav";//文件路径
                        synth.SetOutputToWaveFile(saveFullPath);
                        synth.Speak(text);
                        synth.SetOutputToNull();
                         * */
                        synth.SetOutputToWaveStream(ms);                       
                        synth.Speak(text);
                    }
                    catch (Exception ex)
                    {
                        synth.Dispose();
                        Response.Write(ex.Message);
                    }
                });
                thread.Start();
                thread.Join();
                ms.Position = 0;
                if (ms.Length > 0)
                {
                    ms.WriteTo(Response.OutputStream);
                }
                Response.End();
            }
        }
        #endregion

        #region 上传文件数据

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <returns></returns>
        [HandlerLogin]
        public JsonResult Upload() 
        {
            Hashtable ht_result = new Hashtable();

            try
            {
                HttpPostedFileBase  file = Request.Files["Filedata"];
                if (file != null)
                {
                    string oldFileName = file.FileName;//原文件名
                    int size = file.ContentLength;//附件大小

                    string extenstion = oldFileName.Substring(oldFileName.LastIndexOf(".") + 1);//后缀名
                    //if (extenstion != "doc" && extenstion != "docx")
                    //{
                    //    ht_result.Add("status", "F");
                    //    ht_result.Add("msg", "只可以选择Word文件");
                    //    return JsonHelper.HashtableToJson(ht_result);
                    //}
                    string filename = DateTimeHelper.GetToday("yyyyMMddHHmmssfff") + "_" + oldFileName; //文件重命名
                    string savePath = Server.MapPath(ConstantUtil.FILE_TARGET_URL);//Server.MapPath 获得虚拟服务器相对路径
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
                        ht_result.Add("status", "T");
                        ht_result.Add("msg", "上传成功！");
                        ht_result.Add("filename_o", oldFileName);
                        ht_result.Add("filename", filename);
                        ht_result.Add("path", ConstantUtil.FILE_TARGET_URL + filename);
                        return Json(ht_result);;
                    }

                }
                ht_result.Add("status", "F");
                ht_result.Add("msg", "加载文件失败");
                return Json(ht_result);;
            }
            catch (Exception ex)
            {
                ht_result.Add("status", "F");
                ht_result.Add("msg", "加载文件失败:" + ex.ToString());
                return Json(ht_result);
            }
        }
        
        /// <summary>
        /// 上传文件 Bootstrap File Input 
        /// </summary>
        /// <returns></returns>
        [HandlerLogin]
        public JsonResult UploadBFI() 
        {
            Hashtable ht_result = new Hashtable();

            try
            {
                string modelName = Request["modelName"];//获取文件所属的模块
                string isDate = Request["isDate"]??"";//是否按日期创建文件夹
                string fileNewName = Request["fileNewName"] ?? "";//文件重命名的新名称
                string isCover = Request["isCover"] ?? "";//是否覆盖
                HttpPostedFileBase file = Request.Files["uploadBF"];
                if (file != null)
                {
                    string oldFileName = file.FileName;//原文件名
                    int size = file.ContentLength;//附件大小

                    string extenstion = oldFileName.Substring(oldFileName.LastIndexOf(".") + 1);//后缀名
                    string filename = DateTimeHelper.GetToday("yyyyMMddHHmmssfff") + "_" + oldFileName; //文件重命名

                    //@2019-04-01
                    if (!string.IsNullOrWhiteSpace(fileNewName)) 
                    {
                        filename = fileNewName + "." + extenstion;
                    }

                    string savePath = "";
                    if(string.IsNullOrEmpty(modelName))
                    {
                        modelName = "Default";
                    }
                    savePath = @"\Content\Files\" + modelName + @"\";
                    if(!string.IsNullOrWhiteSpace(isDate))
                    {
                        savePath = savePath + DateTimeHelper.GetToday("yyyyMMdd") + @"\";
                    }
                    string saveFullPath = Server.MapPath(savePath);////Server.MapPath 获得虚拟服务器相对路径
                    string fileFullPath = Server.MapPath(savePath) + filename;////Server.MapPath 获得虚拟服务器相对路径 + 文件名
                    if (!(Directory.Exists(saveFullPath)))
                    {//判断路径是否存在---不存在创建路径
                        Directory.CreateDirectory(saveFullPath);
                    }
                    if ((System.IO.File.Exists(fileFullPath)))
                    {//判断文件是否已经存在，存在删除
                        System.IO.File.Delete(fileFullPath);
                    }

                    file.SaveAs(fileFullPath);
                    bool uploaded = System.IO.File.Exists(fileFullPath);

                    if (uploaded)
                    {
                        ht_result.Add("status", "T");
                        ht_result.Add("msg", "上传成功！");
                        ht_result.Add("filename_o", oldFileName);
                        ht_result.Add("filename", filename);
                        ht_result.Add("path", savePath + filename);
                        return Json(ht_result);
                    }

                }
                ht_result.Add("status", "F");
                ht_result.Add("msg", "加载文件失败");
                return Json(ht_result);;
            }
            catch (Exception ex)
            {
                ht_result.Add("status", "F");
                ht_result.Add("msg", "加载文件失败:" + ex.ToString());
                return Json(ht_result);
            }
        }

        #endregion

        #region 下载文件数据

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="filename">新名称</param>
        /// <param name="id">服务器文件名称</param>
        /// <returns></returns>
        public FileResult DownloadExcelFile(string filename, string id)
        {
            string filePath = Server.MapPath(ConstantUtil.FILE_DOWNLOADTEMP_URL + id);//路径
            return File(filePath, "application/ms-excel", filename + ".xls");
        }
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="url">文件相对路径</param>
        /// <param name="filename">新名称</param>
        /// 
        /// <returns></returns>
        public void DownloadFile(string url, string filename)
        {
            if (url.Length > 0 && filename.Length > 0)
            {
                FileDownHelper.DownLoadold(url, filename);
            }
           
        }

        /// <summary>
        ///下载excel模板
        /// </summary>
        /// <param name="context"></param>
        public void downloadExcelModule()
        {
            string filename = (Request["filename"] ?? "").ToString().Trim();
            string head = (Request["head"] ?? "").ToString().Trim();

            DataTable dt = new DataTable();

            Response.ContentType = "application/vnd.ms-excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Charset = "gb2312";
            Response.HeaderEncoding = System.Text.Encoding.GetEncoding("gb2312");
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + filename + ".xls");
            MemoryStream ms = getMSFromDatatable(dt, head, 1);
            Response.BinaryWrite(ms.ToArray());
            Response.End();
        }

        /// <summary>
        /// 导出excel文件
        /// </summary>
        /// <param name="fileName">文件名称/文件标题</param>
        /// <param name="headerJson">头部数据</param>
        /// <param name="dataJson">数据集</param>
        /// <param name="headType">0:Name;1:Value<Name></param>
        /// <param name="isTitle">是否添加标题0:不添加;1:添加<Name></param>
        public ActionResult downloadExcel(string fileName, string headerJson, string dataJson, int headType = 1, int isTitle = 1)
        {
            DataTable dt = dataJson.ToTable();
            string result = new ExcelHelper().getFileFromDatatable(dt, headerJson, fileName, headType, isTitle);
            var data = new
            {
                state = "success",
                name = result
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// 导出excel文件--流方式（客户端构造form提交）
        /// </summary>
        /// <param name="fileName">文件名称/文件标题</param>
        /// <param name="headerJson">头部数据</param>
        /// <param name="dataJson">数据集</param>
        /// <param name="flag">服务器端是否写文件</param>
        /// <param name="headType">0:Name;1:Value<Name></param>
        public void exportExcel(string fileName, string headerJson, string dataJson, int flag = 0, int headType = 1) 
        {

            DataTable dt = dataJson.ToTable();
            if (flag == 0)
            {
                Response.ContentType = "application/vnd.ms-excel";
                Response.ContentEncoding = System.Text.Encoding.UTF8;
                Response.Charset = "gb2312";
                Response.HeaderEncoding = System.Text.Encoding.GetEncoding("gb2312");
                Response.AppendHeader("Content-Disposition", "attachment;filename=" + fileName + ".xls");
                MemoryStream ms = getMSFromDatatable(dt, headerJson, 1);
                Response.BinaryWrite(ms.ToArray());
                Response.End();
            }
            else 
            {
                string result = new ExcelHelper().getFileFromDatatable(dt, headerJson, fileName, headType);
                string filePath = Server.MapPath(ConstantUtil.FILE_DOWNLOADTEMP_URL + result);//路径
                FileStream fileStream = new FileStream(filePath, FileMode.Open);
                byte[] content = new byte[fileStream.Length];
                fileStream.Read(content, 0, content.Length);
                Response.ContentType = "application/vnd.ms-excel";
                Response.ContentEncoding = System.Text.Encoding.UTF8;
                Response.Charset = "gb2312";
                Response.HeaderEncoding = System.Text.Encoding.GetEncoding("gb2312");
                Response.AppendHeader("Content-Disposition", "attachment;filename=" + fileName + ".xls");
                Response.BinaryWrite(content);
                Response.End();
            }
            
        }

        #endregion

        #region 验证数据

        #endregion

        #region 辅助函数
        /// <summary>
        /// 获取导出excel流
        /// </summary>
        /// <param name="data">数据源</param>
        /// <param name="head">表头数据（json格式）</param>
        /// <param name="ifJudgeKey">是否判断data是否含有Key 0：判断；非0：不判断 </param>
        /// <param name="type">表头格式0：Value；1：Key<Value> </param>
        /// <returns></returns>
        private MemoryStream getMSFromDatatable(DataTable data, string head, int ifJudgeKey = 0, int type = 0)
        {
            Dictionary<string, string> dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(head);
            Dictionary<string, string> dic_temp = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> de in dic)
            {
                if (ifJudgeKey == 0)
                {
                    if (data.Columns.Contains(de.Key))
                    {
                        if (type == 0)
                        {
                            dic_temp.Add(de.Key, de.Key + "<" + de.Value + ">");
                        }
                        else
                        {
                            dic_temp.Add(de.Key, de.Value);
                        }

                    }
                }
                else 
                {
                    if (type == 0)
                    {
                        dic_temp.Add(de.Key, de.Key + "<" + de.Value + ">");
                    }
                    else
                    {
                        dic_temp.Add(de.Key, de.Value);
                    }
                }
                
            }
            NPOIHelper.ListColumnsName = dic_temp;
            MemoryStream ms = NPOIHelper.RetrunStream(data);
            return ms;
        }
        #endregion
    }

    class BackgroundImage 
    {
        public string Src { set; get; }

        public string Name { set; get; }
        public string Name_ { set; get; }

        public string OriginName { set; get; }
    }
}