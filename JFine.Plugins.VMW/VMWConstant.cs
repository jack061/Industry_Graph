/********************************************************************************
**文 件 名:VMWConstant
**命名空间:JFine.Plugins.VMW
**描    述:
**
**版 本 号:V1.0.0.0
**创 建 人:superjoy
**创建日期:2019-02-26 10:04:24
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

namespace JFine.Plugins.VMW
{
    public class VMWConstant
    {

        #region 编码
        public const string CODE_RULE_WARN = "1003";//预警
        #endregion

        #region 状态
        /// <summary>
        /// 未确认
        /// </summary>
        public const string STATU_CONFIRM_UN = "未确认";//未确认
        /// <summary>
        /// 已忽略
        /// </summary>
        public const string STATU_CONFIRM_IGNORE = "已忽略";//已忽略
        /// <summary>
        /// 已确认
        /// </summary>
        public const string STATU_CONFIRM_ED = "已确认";//已确认

        /// <summary>
        /// 无需确认
        /// </summary>
        public const string STATU_CONFIRM_NO = "无需确认";//无需确认

        /// <summary>
        /// 未处理
        /// </summary>
        public const string STATU_DEAL_UN = "未处理";//未处理
        /// <summary>
        /// 已忽略
        /// </summary>
        public const string STATU_DEAL_IGNORE = "已忽略";//已忽略
        /// <summary>
        /// 已处理
        /// </summary>
        public const string STATU_DEAL_ED = "已处理";//已处理

        /// <summary>
        /// 无需处理
        /// </summary>
        public const string STATU_DEAL_NO = "无需处理";//无需处理

        #endregion 
    }
}
