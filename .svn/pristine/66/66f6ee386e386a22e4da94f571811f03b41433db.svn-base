/********************************************************************************
**文 件 名:WF_TestRepository
**命名空间:JFine.Busines.Test
**描    述:
**
**
**版 本 号:V1.0.0.0
**创 建 人:此代码由T4模板自动生成
**创建日期:2018-09-11 18:49:15
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
using JFine.Domain.Models.Test;
using JFine.Common.UI;
using System.Linq.Expressions;
using System.Data.Common;


namespace JFine.Domain.IRepository.Test
{	
	/// <summary>
	/// WF_TestRepository
	/// </summary>	
	public interface IWF_TestRepository
	{
		#region 数据获取
        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<WF_TestEntity> GetList();

		/// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<WF_TestEntity> GetListBySql(string sqlWhere);

		/// <summary>
        /// 列表-分页
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="sqlWhere">查询条件</param>
        /// <returns></returns>
        IEnumerable<WF_TestEntity> GetPageListBySql(Pagination pagination, string sqlWhere ,List<DbParameter> parameter);

		/// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<WF_TestEntity> GetList(Expression<Func<WF_TestEntity, bool>> condition);

		/// <summary>
        /// 列表-分页
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<WF_TestEntity> GetPageList(Pagination pagination, Expression<Func<WF_TestEntity, bool>> condition);

        /// <summary>
        /// 实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        WF_TestEntity GetForm(string keyValue);
        #endregion

        #region 数据处理
        

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name=entity">实体</param>
        /// <returns></returns>
        void SaveForm(string keyValue, WF_TestEntity wF_TestEntity);

        /// <summary>
        /// 提交数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name=entity">实体</param>
        /// <returns></returns>
        void SubmitForm_0(string keyValue, WF_TestEntity wF_TestEntity);
        
        /// <summary>
        /// 提交数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name=entity">实体</param>
        /// <returns></returns>
        void SubmitForm(string keyValue, WF_TestEntity wF_TestEntity);

		/// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        void DeleteForm(string keyValue);

        #endregion
    }
}

