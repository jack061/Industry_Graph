using JFine.Code.Online;
using JFine.Common.Code;
using JFine.Common.Web;
using JFine.Log;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Interception;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web;

namespace JFine.Web.App_Start
{
    public class EFIntercepterLogging : DbCommandInterceptor
    {
        private readonly Stopwatch _stopwatch = new Stopwatch();
        Log.Log log = LogFactory.GetLogger(typeof(EFIntercepterLogging));
        public override void ScalarExecuting(System.Data.Common.DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            base.ScalarExecuting(command, interceptionContext);
            _stopwatch.Restart();
        }
        public override void ScalarExecuted(System.Data.Common.DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            _stopwatch.Stop();
            if (interceptionContext.Exception != null)
            {
                string message = String.Format("Exception:{1} \r\n --> Error executing command: {0}", command.CommandText, interceptionContext.Exception.ToString());
                Trace.TraceError(message);
                WriteLogMessage(message, "Error"); 
            }
            else
            {
                string message = String.Format("\r\n执行时间:{0} 毫秒\r\n-->ScalarExecuted.Command:{1}\r\n", _stopwatch.ElapsedMilliseconds, command.CommandText);
                Trace.TraceInformation(message);
                WriteLogMessage(message, "Info");
            }
            base.ScalarExecuted(command, interceptionContext);
        }
        public override void NonQueryExecuting(System.Data.Common.DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            base.NonQueryExecuting(command, interceptionContext);
            _stopwatch.Restart();
        }
        public override void NonQueryExecuted(System.Data.Common.DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            _stopwatch.Stop();
            if (interceptionContext.Exception != null)
            {
                string message = String.Format("Exception:{1} \r\n --> Error executing command:\r\n {0}", command.CommandText, interceptionContext.Exception.ToString());
                Trace.TraceError(message);
                WriteLogMessage(message, "Error");
            }
            else
            {
                string message = String.Format("\r\n执行时间:{0} 毫秒\r\n-->NonQueryExecuted.Command:\r\n{1}", _stopwatch.ElapsedMilliseconds, command.CommandText);
                Trace.TraceInformation(message);
                WriteLogMessage(message, "Info");
            }
            base.NonQueryExecuted(command, interceptionContext);
        }
        public override void ReaderExecuting(System.Data.Common.DbCommand command, DbCommandInterceptionContext<System.Data.Common.DbDataReader> interceptionContext)
        {
            base.ReaderExecuting(command, interceptionContext);
            _stopwatch.Restart();
        }
        public override void ReaderExecuted(System.Data.Common.DbCommand command, DbCommandInterceptionContext<System.Data.Common.DbDataReader> interceptionContext)
        {
            _stopwatch.Stop();
            if (interceptionContext.Exception != null)
            {
                string message = String.Format("Exception:{1} \r\n --> Error executing command:\r\n {0}", command.CommandText, interceptionContext.Exception.ToString());
                Trace.TraceInformation(message);
                WriteLogMessage(message, "Error");
            }
            else
            {
                string message = String.Format("\r\n执行时间:{0} 毫秒 \r\n -->ReaderExecuted.Command:\r\n{1}", _stopwatch.ElapsedMilliseconds, command.CommandText);
                Trace.TraceInformation(message);
                WriteLogMessage(message,"Info");
            }
            base.ReaderExecuted(command, interceptionContext);
        }

        public void WriteLogMessage(string message,string dataType)
        {
            LogMessage logMessage = new LogMessage();
            ThreadPool.QueueUserWorkItem((a) =>
            {
                logMessage = getLogMessage(message);
                if (dataType == "Info")
                {
                    log.Info(logMessage.ToString());
                }
                if (dataType == "Error")
                {
                    log.Error(logMessage.ToString());
                }
            });
        }

        /// <summary>
        /// 生成日志数据
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public LogMessage getLogMessage(string message)
        {
            LogMessage logMessage = new LogMessage();
            try
            {               
                OnlineUserModel user = OnlineUser.Operator.GetCurrent();
                logMessage.Time = DateTimeHelper.ShortDateTimeS;
                logMessage.Account = (user == null) ? "无登陆信息" : user.Account;
                logMessage.UserName = (user == null) ? "无登陆信息" : user.UserName;
                logMessage.Ip = (user == null) ? RequestHelper.Ip : user.IP;
                logMessage.System = (user == null) ? RequestHelper.GetClientOSName() : user.System;
                logMessage.Browser = (user == null) ? RequestHelper.Browser : user.Browser;
                logMessage.Content = message;
                
            }
            catch (Exception e) 
            {
                logMessage.Time = DateTimeHelper.ShortDateTimeS;
                logMessage.Account = "无登陆信息";
                logMessage.UserName = "无登陆信息";
                logMessage.Ip = "";
                logMessage.System = "";
                logMessage.Browser = "";
                logMessage.Content = message;
            }
            return logMessage;
            
        }
    }
}