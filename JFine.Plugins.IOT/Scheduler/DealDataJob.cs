
using JFine.Job;
/********************************************************************************
**文 件 名:CheckDeviceStatusJob
**命名空间:JFine.Plugins.IOT.Scheduler
**描    述:对采集的原始数据进行处理
**
**版 本 号:V1.0.0.0
**创 建 人:superjoy
**创建日期:2019-07-26 18:18:29
**修 改 人:
**修改日期:
**修改描述:
**版权所有: ©为之团队
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using JFine.Plugins.IOT.Busines.MQTT;

namespace JFine.Plugins.IOT.Scheduler
{
    public class DealDataJob : ITaskJob
    {
       
        public bool CloseJob(JFine.Domain.Models.SysCommon.Job_ScheduleEntity jobModel)
        {
            throw new NotImplementedException();
        }

        public bool RunJob(Dictionary<string, string> dic_paras, string jobName, string id)
        {
            /*
             * 对采集的原始数据进行处理
             * 
             */
            MQTTDealDataBLL mQTTDealDataBLL = new MQTTDealDataBLL();
            mQTTDealDataBLL.StartDeal();
            return true;
        }
        
        public bool RunJobBefore(JFine.Domain.Models.SysCommon.Job_ScheduleEntity jobModel)
        {
            throw new NotImplementedException();
        }
    }
}
