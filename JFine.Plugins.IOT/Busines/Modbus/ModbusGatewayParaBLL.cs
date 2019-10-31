﻿
/********************************************************************************
**文 件 名:ModbusGatewayParaBLL
**命名空间:JFine.Plugins.IOT.Busines.Modbus
**描    述:
**
**
**版 本 号:V1.0.0.0
**创 建 人:此代码由T4模板自动生成
**创建日期:2019-06-24 17:57:28
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
	/// ModbusGatewayParaBLL
	/// </summary>	
	public class ModbusGatewayParaBLL
	{
	    private IModbusGatewayParaRepository service=new ModbusGatewayParaRepository();

        #region 数据获取

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ModbusGatewayParaEntity> GetList()
        {
            return service.GetList();
        }

		/// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ModbusGatewayParaEntity> GetListBySql( string sqlWhere)
        {
			return service.GetListBySql(sqlWhere);
        }

		/// <summary>
        /// 列表--分页
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<ModbusGatewayParaEntity> GetPageListBySql(Pagination pagination, string queryJson)
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
        public IEnumerable<ModbusGatewayParaEntity> GetList( string queryJson)
        {
             var expression = LinqExtensions.True<ModbusGatewayParaEntity>();
            var queryParam = queryJson.ToJObject();
            //查询条件
            if (!queryParam["Name"].IsEmpty())
            {
                string ParameterCode = queryParam["ParameterCode"].ToString();
                expression = expression.And(t => t.ParameterCode.Contains(ParameterCode));
            }
			return service.GetList(expression);
        }

        /// <summary>
        /// 列表--分页
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<ModbusGatewayParaEntity> GetPageList(Pagination pagination, string queryJson)
        {
			var expression = LinqExtensions.True<ModbusGatewayParaEntity>();
            var queryParam = queryJson.ToJObject();
             //查询条件
            if (!queryParam["keyword"].IsEmpty())
            {
                string keyord = queryParam["keyword"].ToString();
                expression = expression.And(t => t.ParameterCode.Contains(keyord));
            }
            string bindid = (queryParam["bindid"]??"").ToString();
            expression = expression.And(t => t.BindId.Equals(bindid));
            return service.GetPageList(pagination, expression);
        }

        /// <summary>
        /// 实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public ModbusGatewayParaEntity GetForm(string keyValue)
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
        public void SaveForm(string keyValue, ModbusGatewayParaEntity modbusGatewayParaEntity)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(keyValue))
                {
                    if (string.IsNullOrWhiteSpace(modbusGatewayParaEntity.BindId))
                    {
                        throw new Exception("关联设备不能为空！");
                    }
                    var list = GetListBySql(" AND BindId = '" + modbusGatewayParaEntity.BindId
                        + "' AND ParameterCode = '" + modbusGatewayParaEntity.ParameterCode + "' AND DeviceCode = '" + modbusGatewayParaEntity.DeviceCode + "'");
                    if (list.Count() > 0)
                    {
                        throw new Exception("设备（" + modbusGatewayParaEntity.DeviceCode + "）下已经存在参数编码（" + modbusGatewayParaEntity.ParameterCode + "）；");
                    }
                }
                else
                {
                    var list = GetListBySql(" AND BindId = '" + modbusGatewayParaEntity.BindId
                        + "' AND ParameterCode = '" + modbusGatewayParaEntity.ParameterCode
                        + "' AND DeviceCode = '" + modbusGatewayParaEntity.DeviceCode
                        + "' AND Id <> '" + keyValue + "'");
                    if (list.Count() > 0)
                    {
                        throw new Exception("设备（" + modbusGatewayParaEntity.DeviceCode + "）下已经存在参数编码（" + modbusGatewayParaEntity.ParameterCode + "）；");
                    }
                }
                service.SaveForm(keyValue, modbusGatewayParaEntity);
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