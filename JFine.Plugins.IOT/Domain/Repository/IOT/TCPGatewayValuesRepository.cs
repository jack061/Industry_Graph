/********************************************************************************
**文 件 名:TCPGatewayValuesRepository
**命名空间:JFine.Plugins.IOT.Busines.IOT
**描    述:
**
**
**版 本 号:V1.0.0.0
**创 建 人:此代码由T4模板自动生成
**创建日期:2019-10-01 22:38:05
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
using JFine.Data.Repository;
using JFine.Plugins.IOT.Domain.IRepository.IOT;
using JFine.Common.UI;
using JFine.Common.Extend;
using JFine.Common.Json;
using System.Data.Common;
using System.Linq.Expressions;

namespace JFine.Plugins.IOT.Domain.Repository.IOT
{	
	/// <summary>
	/// TCPGatewayValuesRepository
	/// </summary>	
	public class TCPGatewayValuesRepository:RepositoryFactory<TCPGatewayValuesEntity>, ITCPGatewayValuesRepository
	{
		 #region 数据获取
        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TCPGatewayValuesEntity> GetList()
        {
            return this.BaseRepository().IQueryable().OrderByDescending(t => t.CreateDate).ToList();
        
		}

		/// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TCPGatewayValuesEntity> GetListBySql(string sqlWhere)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT * 
                            FROM   Tcp_Gateway_Values
                            WHERE  1=1 ");
            strSql.Append(sqlWhere);
            return this.BaseRepository().FindList(strSql.ToString());
        
		}

		/// <summary>
        /// 列表-分页
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="sqlWhere">查询条件</param>
        /// <returns></returns>
        public IEnumerable<TCPGatewayValuesEntity> GetPageListBySql(Pagination pagination, string sqlWhere, List<DbParameter> parameter)
        {

            var strSql = new StringBuilder();
            strSql.Append(@"SELECT * 
                            FROM   Tcp_Gateway_Values
                            WHERE  1=1 ");
            strSql.Append(sqlWhere);

            return this.BaseRepository().FindList(strSql.ToString(),parameter, pagination);

        }

		/// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TCPGatewayValuesEntity> GetList(Expression<Func<TCPGatewayValuesEntity, bool>> condition)
        {
            return this.BaseRepository().IQueryable(condition).ToList();

        }


		/// <summary>
        /// 列表--分页
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<TCPGatewayValuesEntity> GetPageList(Pagination pagination, Expression<Func<TCPGatewayValuesEntity, bool>> condition)
        {
            return this.BaseRepository().FindList(condition, pagination);
        }

        /// <summary>
        /// 实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public TCPGatewayValuesEntity GetForm(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region 数据处理

        public void SaveList(StringBuilder sql)
        {
            this.BaseRepository().ExecuteBySql(sql.ToString());
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="tCPGatewayValuesEntity">实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, TCPGatewayValuesEntity tCPGatewayValuesEntity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                tCPGatewayValuesEntity.Modify(keyValue);
                this.BaseRepository().Update(tCPGatewayValuesEntity);
            }
            else
            {
                tCPGatewayValuesEntity.Create();
                this.BaseRepository().Insert(tCPGatewayValuesEntity);
            }
        }

		/// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void DeleteForm(string keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }

        #endregion
    }
}


