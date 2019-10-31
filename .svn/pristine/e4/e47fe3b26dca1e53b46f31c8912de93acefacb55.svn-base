using JFine.Busines.SystemManage;
using JFine.WorkFlow;
/********************************************************************************
**文 件 名:GetCustomUser
**命名空间:JFine.Plugins.Test.Event
**描    述:
**
**版 本 号:V1.0.0.0
**创 建 人:superjoy
**创建日期:2019-03-20 16:38:45
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
    public class GetCustomUser : IGetCustomUser
    {
        public List<Domain.Models.SystemManage.UserExpand> GetCustomUserList()
        {
            UserBLL userBll = new UserBLL();
            StringBuilder sqlwhere = new StringBuilder();
            sqlwhere.Append(" AND Account='100003' ");
            return userBll.GetUserList(sqlwhere).ToList();
        }
    }
}
