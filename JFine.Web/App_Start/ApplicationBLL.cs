using JFine.Busines.SysConfig;
using JFine.Busines.SystemManage;
using JFine.WorkFlow.Business;
using JFine.Cache;
using JFine.Code.Online;
using JFine.Data.EF;
using JFine.Domain.Models.SysConfig;
using JFine.Domain.Models.SystemManage;
using JFine.Util;
using JFine.Web.Base.MVC.Routes;
/********************************************************************************
**文 件 名:ApplicationBLL
**命名空间:JFine.Web.App_Start.Handler
**描    述:应用级业务处理
**
**版 本 号:V1.0.0.0
**创 建 人:superjoy
**创建日期:2018-05-12 21:46:11
**修 改 人:
**修改日期:
**修改描述:
**版权所有: ©为之团队
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Web.Routing;
using JFine.Busines.SysManage;
using JFine.Domain.Models.SysManage;
using System.Collections;
using JFine.Infrastructure;
using JFine.Domain.Models.SysCommon;
using JFine.Domain.IRepository.SysCommon;
using JFine.Domain.Repository.SysCommon;

namespace JFine.Web.App_Start
{
    public class ApplicationBLL
    {
        private OnlineUser onlineBll = new OnlineUser();
        private SysConfigBLL sysConfigBLL = new SysConfigBLL();
        /// <summary>
        /// 缓存初始化
        /// </summary>
        public void InitCache()
        {
            #region 组织机构
            OrganizeBLL organizeBLL = new OrganizeBLL();
            List<OrganizeEntity> orgList = organizeBLL.GetList().ToList();
            CacheFactory.Cache().WriteCache<List<OrganizeEntity>>(CacheKeysUtil.ORG.ToString(), orgList, false);
            #endregion

            #region 功能权限
            SysActionBLL sysActionBLL = new SysActionBLL();
            List<SysActionEntity> actionList = sysActionBLL.GetList().ToList();
            CacheFactory.Cache().WriteCache<List<SysActionEntity>>(CacheKeysUtil.ACTION.ToString(), actionList, false);

            SysActionRightBLL sysActionRightBLL = new SysActionRightBLL();
            List<SysActionRightEntity> actionRightList = sysActionRightBLL.GetList().ToList();
            CacheFactory.Cache().WriteCache<List<SysActionRightEntity>>(CacheKeysUtil.ACTIONRIGHT.ToString(), actionRightList, false);
            #endregion
            
            #region 系统配置
            sysConfigBLL.GetFromCode(ConstantUtil.CODE_SYS);
            
            #endregion

            #region 审批流
            WF_CacheInit WF_CacheInit = new WF_CacheInit();
            WF_CacheInit.CacheInit();
            #endregion

            #region ef Test
            /*
            SqlServerDbContext sqlcontext = new SqlServerDbContext("Server=.;Initial Catalog=JFine_v2;User ID=sa;Password=sasa1990");
            sqlcontext.Configuration.AutoDetectChangesEnabled = false;
            sqlcontext.UpdateModelCreating();

            SqlServerDbContext sqlcontext2 = new SqlServerDbContext("Server=.;Initial Catalog=JFine_v2;User ID=sa;Password=sasa1990");
            sqlcontext2.Configuration.AutoDetectChangesEnabled = false;
             * */
            #endregion

            #region mqtt配置数据
            Hashtable mqttConfig = new Hashtable();
            mqttConfig.Add("DLLName", "JFine.Plugins.IOT.dll");
            mqttConfig.Add("ExecuteContent", "JFine.Plugins.IOT.Scheduler.InitDataJob");
            mqttConfig.Add("ExecuteParam", "");
            ExecuteTask(mqttConfig);
            #endregion

            #region 对采集的原始数据进行实时处理
            Hashtable dealDataConfig = new Hashtable();
            dealDataConfig.Add("DLLName", "JFine.Plugins.IOT.dll");
            dealDataConfig.Add("ExecuteContent", "JFine.Plugins.IOT.Scheduler.DealDataJob");
            dealDataConfig.Add("ExecuteParam", "");
            ExecuteTask(dealDataConfig);
            #endregion

            #region 对海立达采集的原始数据进行实时处理
            Hashtable HLD_dealDataConfig = new Hashtable();
            HLD_dealDataConfig.Add("DLLName", "JFine.Plugins.IOT.dll");
            HLD_dealDataConfig.Add("ExecuteContent", "JFine.Plugins.IOT.Scheduler.HLD_DealDataJob");
            HLD_dealDataConfig.Add("ExecuteParam", "");
            ExecuteTask(HLD_dealDataConfig);
            #endregion


        }

        #region 在线用户处理

        /// <summary>
        /// 启动检查操作Session用户队列
        /// </summary>
        public void StartCheckOnlineQueue()
        {
            //取消队列过渡
            //OnlineUser.Operator.CheckOnlineQueue();
        }

        /// <summary>
        /// 启动检查在线用户是否超期
        /// </summary>
        public void StartCheckOnLineIsOverdue()
        {
            CheckOnlineList();
        }


        /// <summary>
        /// 检测在线列表，超期用户移出列表
        /// </summary>
        private void CheckOnlineList()
        {
            ThreadPool.QueueUserWorkItem((a) =>
            {
                while (true)
                {
                    var onlinerList = onlineBll.GetOnlinerAll();
                    if (onlinerList != null)
                    {
                        //默认90分钟
                        var onlinerOverDue = onlinerList.Where(t => t.SessionStartTime.AddMinutes(90) < DateTime.Now).ToList();
                        if (onlinerOverDue != null && onlinerOverDue.Count > 0)
                        {
                            //删除超期用户
                            foreach (var onliner in onlinerOverDue)
                            {
                                onlineBll.DelUserFromCacheList(onliner.SessionId);
                            }
                        }
                        else
                        {
                            //如果没有用户超期，休眠5分钟.
                            Thread.Sleep(300000);
                        }
                    }
                    else
                    {
                        //如果没有数据，休眠10分钟.
                        Thread.Sleep(600000);
                    }
                }
            });
        }

        #endregion

        #region 系统日志记录到数据库
         public void StartLog2DB()
        {
            LogBLL.StartLogQueue();
        }
        
        #endregion

        #region 插件路由注册
        public void RegistPluginRoutes() 
        {
            RoutePublisher rp = new RoutePublisher();
            rp.RegisterRoutes(RouteTable.Routes);
        }
        #endregion


        /// <summary>
        /// 重新加载缓存
        /// </summary>
        public void ReloadCache() 
        {

        }

        private void ExecuteTask(Hashtable ht )
        {
            IScheduleLogRepository service = new ScheduleLogRepository();
            ScheduleLogEntity logEntity = new ScheduleLogEntity();
            try {
                string ExecuteContent = ht["ExecuteContent"].ToString();//可执行内容
                string ExecuteParam = ht["ExecuteParam"].ToString();//传入参数 
                string DLLName = ht["DLLName"].ToString();//传入参数

                string TaskId = "0";//系统任务
                string jobName = "数据初始化";
                
                logEntity.BindId = TaskId;
                logEntity.ExecuteDT = DateTime.Now;


                Dictionary<string, string> dic_paras = new Dictionary<string, string>();
                if (ExecuteParam.Length > 0)
                {
                    string[] paras = ExecuteParam.Split(',');
                    dic_paras = paras.ToDictionary(element => element.Substring(0, element.IndexOf('=')), element => element.Substring(element.IndexOf('=') + 1));
                }

                AppDomainTypeFinder finder = new AppDomainTypeFinder();
                Assembly assem = finder.GetAssemblyByName(DLLName);
                if (assem == null) 
                {
                    string assembleFileName = Assembly.GetExecutingAssembly().CodeBase.Replace("JFine.Job.DLL", DLLName).Replace("file:///", "");
                    assem = Assembly.LoadFrom(assembleFileName);
                    if (assem == null)
                    {
                        throw new Exception("获取" + DLLName + "失败");
                    }
                }
                Object o = assem.CreateInstance(ExecuteContent, false,
                    BindingFlags.ExactBinding,
                    null, new Object[] { }, null, null);

                MethodInfo m = assem.GetType(ExecuteContent).GetMethod("RunJob");//调用方法
                Object ret = m.Invoke(o, new Object[] { dic_paras, jobName, TaskId });
                logEntity.Result = 1;
                logEntity.ResultDESC = "正常执行";
            }
            catch (Exception ex)
            {
                logEntity.Result = -1;
                logEntity.ResultDESC = ex.Message + ":" + (ex.InnerException == null ? "" : ex.InnerException.Message);
                logEntity.SourceContent = ex.StackTrace;
                //service.SaveForm("",logEntity);
            }
            service.SaveForm("", logEntity);
        }
    }
}