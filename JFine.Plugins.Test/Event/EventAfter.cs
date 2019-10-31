using JFine.Busines.Test;
using JFine.Data.Repository;
using JFine.Domain.Models.Test;
using JFine.WorkFlow;
/********************************************************************************
**文 件 名:EventAfter
**命名空间:JFine.Plugins.Test.Event
**描    述:
**
**版 本 号:V1.0.0.0
**创 建 人:superjoy
**创建日期:2019-03-19 16:29:48
**修 改 人:
**修改日期:
**修改描述:
**版权所有: ©为之团队
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFine.Plugins.Test.Event
{
    public class EventAfter : IWorkFlowEvent
    {
        public void execute(Domain.Models.WorkFlow.WF_TaskEntity workFlowTaskEntity, Domain.Models.WorkFlow.WF_NodeEntity currentNode, string param)
        {
            //1、参数
            Dictionary<string, string> dic_paras = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(param) && param.Length > 0)
            {
                string[] paras = param.Split(',');
                dic_paras = paras.ToDictionary(element => element.Substring(0, element.IndexOf('=')), element => element.Substring(element.IndexOf('=') + 1));
            }
            //2.获取业务数据
            WF_TestBLL wf_TestBLL = new WF_TestBLL();
            WF_TestEntity wf = wf_TestBLL.GetForm(workFlowTaskEntity.DataID);

            //3、处理审批数据
            if (workFlowTaskEntity.Result == "通过") 
            {
                IRepositoryBase db = new RepositoryFactory().BaseRepository();
                db.ExecuteBySql("update WF_Test set Status = '通过' where id = '" + workFlowTaskEntity.DataID + "'");
            }else
            {
                //throw new Exception("此申请必须通过！");
                //db.ExecuteBySql("update WF_Test set Status = '未通过' where id = '" + workFlowTaskEntity.DataID + "'");
            }
        }
    }
}
