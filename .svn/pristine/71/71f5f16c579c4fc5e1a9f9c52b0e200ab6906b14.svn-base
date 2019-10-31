
/********************************************************************************
**文 件 名:ModbusGatewayBLL
**命名空间:JFine.Plugins.IOT.Busines.Modbus
**描    述:
**
**
**版 本 号:V1.0.0.0
**创 建 人:此代码由T4模板自动生成
**创建日期:2019-06-24 17:55:26
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
using JFine.Plugins.IOT.Domain.Models.Modbus;
using JFine.Plugins.IOT.Domain.IRepository.Modbus;
using JFine.Plugins.IOT.Domain.Repository.Modbus;
using JFine.Cache;
using JFine.Common.UI;
using JFine.Common.Extend;
using JFine.Common.Json;
using JFine.Data.Common;
using System.Linq.Expressions;
using System.Data.Common;
using System.Data;

namespace JFine.Plugins.IOT.Busines.Modbus
{	
	/// <summary>
	/// ModbusGatewayBLL
	/// </summary>	
	public class ModbusGatewayBLL
	{
	    private IModbusGatewayRepository service=new ModbusGatewayRepository();

        #region 数据获取

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ModbusGatewayEntity> GetList()
        {
            return service.GetList();
        }

		/// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ModbusGatewayEntity> GetListBySql( string sqlWhere)
        {
			return service.GetListBySql(sqlWhere);
        }

		/// <summary>
        /// 列表--分页
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<ModbusGatewayEntity> GetPageListBySql(Pagination pagination, string queryJson)
        {
            var sqlWhere = new StringBuilder();
            var queryParam = queryJson.ToJObject();
			 List<DbParameter> parameter =  new List<DbParameter>();
            //查询条件
            if (!queryParam["keyword"].IsEmpty())
            {
                string keyword = queryParam["keyword"].ToString();
                sqlWhere.Append(" AND (Code like @keyword or Name like @keyword)");
				parameter.Add(DbParameters.CreateDbParameter("@keyword","%"+ keyword +"%",DbType.AnsiString));
            }
            
            return service.GetPageListBySql(pagination, sqlWhere.ToString(),parameter);
        }

		/// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ModbusGatewayEntity> GetList( string queryJson)
        {
             var expression = LinqExtensions.True<ModbusGatewayEntity>();
            var queryParam = queryJson.ToJObject();
            //查询条件
            if (!queryParam["Ip"].IsEmpty())
            {
                string Ip = queryParam["Ip"].ToString();
                expression = expression.And(t => t.Ip.Contains(Ip));
            }
			return service.GetList(expression);
        }

        /// <summary>
        /// 列表--分页
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<ModbusGatewayEntity> GetPageList(Pagination pagination, string queryJson)
        {
			var expression = LinqExtensions.True<ModbusGatewayEntity>();
            var queryParam = queryJson.ToJObject();
             //查询条件
            if (!queryParam["keyword"].IsEmpty())
            {
                string keyord = queryParam["keyword"].ToString();
                expression = expression.And(t => t.Ip.Contains(keyord));
            }
            return service.GetPageList(pagination, expression);
        }

        /// <summary>
        /// 实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public ModbusGatewayEntity GetForm(string keyValue)
        {
            return service.GetForm(keyValue);
        }
        #endregion

        #region 数据处理

        

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, ModbusGatewayEntity modbusGatewayEntity)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(keyValue))
                {
                    var list = GetListBySql(" AND Ip = '" + modbusGatewayEntity.Ip + "' ");
                    if (list.Count() > 0)
                    {
                        throw new Exception("Ip:" + modbusGatewayEntity.Ip + "已经存在；");
                    }
                }
                else
                {
                    var list = GetListBySql(" AND Ip = '" + modbusGatewayEntity.Ip + "' AND Id <> '" + keyValue + "'");
                    if (list.Count() > 0)
                    {
                        throw new Exception("Ip:" + modbusGatewayEntity.Ip + "已经存在；");
                    }
                }
                service.SaveForm(keyValue, modbusGatewayEntity);
            }
            catch (Exception)
            {
                throw;
            }
        }

		/// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void DeleteForm(string keyValue)
        {
            try
            {
                service.DeleteForm(keyValue);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

    }
}
