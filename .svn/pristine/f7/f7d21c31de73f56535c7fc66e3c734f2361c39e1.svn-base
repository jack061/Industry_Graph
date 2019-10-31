/********************************************************************************
**文 件 名:ITCPGatewayValuesRepository
**命名空间:JFine.Plugins.IOT.Busines.IOT
**描    述:
**
**
**版 本 号:V1.0.0.0
**创 建 人:此代码由T4模板自动生成
**创建日期:2019-10-01 22:38:03
**修 改 人:
**修改日期:
**修改描述:
**
**
**版权所有: ©为之团队
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JFine.Data;
using JFine.Plugins.IOT.Domain.Models.IOT;
using JFine.Common.UI;
using System.Data.Common;
using System.Linq.Expressions;


namespace JFine.Plugins.IOT.Domain.IRepository.IOT
{	
	/// <summary>
	/// ITCPGatewayValuesRepository
	/// </summary>	
	public interface ITCPGatewayValuesRepository
	{
		#region 数据获取
        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<TCPGatewayValuesEntity> GetList();

		/// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<TCPGatewayValuesEntity> GetListBySql(string sqlWhere);

		/// <summary>
        /// 列表-分页
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="sqlWhere">查询条件</param>
        /// <returns></returns>
        IEnumerable<TCPGatewayValuesEntity> GetPageListBySql(Pagination pagination, string sqlWhere ,List<DbParameter> parameter);

		/// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<TCPGatewayValuesEntity> GetList(Expression<Func<TCPGatewayValuesEntity, bool>> condition);

		/// <summary>
        /// 列表-分页
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<TCPGatewayValuesEntity> GetPageList(Pagination pagination, Expression<Func<TCPGatewayValuesEntity, bool>> condition);

        /// <summary>
        /// 实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        TCPGatewayValuesEntity GetForm(string keyValue);
        #endregion

        #region 数据处理

        void SaveList(StringBuilder sql);

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name=entity">实体</param>
        /// <returns></returns>
        void SaveForm(string keyValue, TCPGatewayValuesEntity tCPGatewayValuesEntity);

		/// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        void DeleteForm(string keyValue);

        #endregion
    }
}

