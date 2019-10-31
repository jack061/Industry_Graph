/********************************************************************************
**文 件 名:ModbusGatewayRepository
**命名空间:JFine.Plugins.IOT.Busines.Modbus
**描    述:
**
**
**版 本 号:V1.0.0.0
**创 建 人:此代码由T4模板自动生成
**创建日期:2019-06-24 17:55:32
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
using JFine.Plugins.IOT.Domain.Models.Modbus;
using JFine.Data.Repository;
using JFine.Plugins.IOT.Domain.IRepository.Modbus;
using JFine.Common.UI;
using JFine.Common.Extend;
using JFine.Common.Json;
using System.Data.Common;
using System.Linq.Expressions;

namespace JFine.Plugins.IOT.Domain.Repository.Modbus
{	
	/// <summary>
	/// ModbusGatewayRepository
	/// </summary>	
	public class ModbusGatewayRepository:RepositoryFactory<ModbusGatewayEntity>, IModbusGatewayRepository
	{
		 #region 数据获取
        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ModbusGatewayEntity> GetList()
        {
            return this.BaseRepository().IQueryable().OrderByDescending(t => t.CreateDate).ToList();
        
		}

		/// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ModbusGatewayEntity> GetListBySql(string sqlWhere)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT * 
                            FROM   Modbus_Gateway
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
        public IEnumerable<ModbusGatewayEntity> GetPageListBySql(Pagination pagination, string sqlWhere, List<DbParameter> parameter)
        {

            var strSql = new StringBuilder();
            strSql.Append(@"SELECT * 
                            FROM   Modbus_Gateway
                            WHERE  1=1 ");
            strSql.Append(sqlWhere);

            return this.BaseRepository().FindList(strSql.ToString(),parameter, pagination);

        }

		/// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ModbusGatewayEntity> GetList(Expression<Func<ModbusGatewayEntity, bool>> condition)
        {
            return this.BaseRepository().IQueryable(condition).ToList();

        }


		/// <summary>
        /// 列表--分页
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<ModbusGatewayEntity> GetPageList(Pagination pagination, Expression<Func<ModbusGatewayEntity, bool>> condition)
        {
            return this.BaseRepository().FindList(condition, pagination);
        }

        /// <summary>
        /// 实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public ModbusGatewayEntity GetForm(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region 数据处理
        
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="modbusGatewayEntity">实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, ModbusGatewayEntity modbusGatewayEntity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                modbusGatewayEntity.Modify(keyValue);
                this.BaseRepository().Update(modbusGatewayEntity);
            }
            else
            {
                modbusGatewayEntity.Create();
                this.BaseRepository().Insert(modbusGatewayEntity);
            }
        }

		/// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void DeleteForm(string keyValue)
        {
            IRepositoryBase db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                db.Delete<ModbusGatewayParaEntity>(t => t.BindId.Equals(keyValue));
                db.Delete<ModbusGatewayEntity>(keyValue);
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }

        #endregion
    }
}


