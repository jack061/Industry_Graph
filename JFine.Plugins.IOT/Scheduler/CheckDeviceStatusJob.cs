using JFine.Data.Repository;
using JFine.Job;
using JFine.Plugins.IOT.IOTHubs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using JFine.Common.Json;
/********************************************************************************
**文 件 名:CheckDeviceStatusJob
**命名空间:JFine.Plugins.IOT.Scheduler
**描    述:定时检测设备状态
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

namespace JFine.Plugins.IOT.Scheduler
{
    public class CheckDeviceStatusJob : ITaskJob
    {

        public bool CloseJob(JFine.Domain.Models.SysCommon.Job_ScheduleEntity jobModel)
        {
            throw new NotImplementedException();
        }

        public bool RunJob(Dictionary<string, string> dic_paras, string jobName, string id)
        {
            /*
             * 定时检测是否有节拍信息,如果有推送到前端；
             * 
             */
            if (dic_paras.ContainsKey("@period"))
            {
                double period = 0;
                try
                {
                    period = double.Parse(dic_paras["@period"]);
                    DateTime dt = DateTime.Now;
                    dt = dt.AddMinutes(-period);
                    StringBuilder sql = new StringBuilder();
                    sql.Append(@"SELECT    DISTINCT DeviceCode 
                                  FROM      IOT_DeviceBeat
                                  WHERE     CreateDate>'" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                    DataTable data = new RepositoryFactory().BaseRepository().FindTable(sql.ToString());

                    ModbusHub modbusHub = new ModbusHub();
                    modbusHub.SendDeviceStatus(data.ToJson());

                }
                catch (Exception)
                {

                    return false;
                }

            }



            return true;
        }

        public bool RunJobBefore(JFine.Domain.Models.SysCommon.Job_ScheduleEntity jobModel)
        {
            throw new NotImplementedException();
        }
    }
}
