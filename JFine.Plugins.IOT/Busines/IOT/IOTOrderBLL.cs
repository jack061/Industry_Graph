
/********************************************************************************
**文 件 名:IOTOrderBLL
**命名空间:JFine.Plugins.IOT.Busines.IOT
**描    述:
**
**
**版 本 号:V1.0.0.0
**创 建 人:此代码由T4模板自动生成
**创建日期:2019-07-26 09:34:07
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
using JFine.Plugins.IOT.Domain.Models.IOT;
using JFine.Plugins.IOT.Domain.IRepository.IOT;
using JFine.Plugins.IOT.Domain.Repository.IOT;
using JFine.Cache;
using JFine.Common.UI;
using JFine.Common.Extend;
using JFine.Common.Json;
using JFine.Data.Common;
using System.Linq.Expressions;
using System.Data.Common;
using System.Data;

namespace JFine.Plugins.IOT.Busines.IOT
{	
	/// <summary>
	/// IOTOrderBLL
	/// </summary>	
	public class IOTOrderBLL
	{
	    private IIOTOrderRepository service=new IOTOrderRepository();


        #region 数据获取

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IOTOrderEntity> GetList()
        {
            return service.GetList();
        }

		/// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IOTOrderEntity> GetListBySql( string sqlWhere)
        {
			return service.GetListBySql(sqlWhere);
        }

		/// <summary>
        /// 列表--分页
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<IOTOrderEntity> GetPageListBySql(Pagination pagination, string queryJson)
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
        public IEnumerable<IOTOrderEntity> GetList( string queryJson)
        {
             var expression = LinqExtensions.True<IOTOrderEntity>();
            var queryParam = queryJson.ToJObject();
            //查询条件
            if (!queryParam["Name"].IsEmpty())
            {
                string name = queryParam["Name"].ToString();
                expression = expression.And(t => t.DeviceName.Contains(name));
            }
			return service.GetList(expression);
        }

        /// <summary>
        /// 列表--分页
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<IOTOrderEntity> GetPageList(Pagination pagination, string queryJson)
        {
			var expression = LinqExtensions.True<IOTOrderEntity>();
            var queryParam = queryJson.ToJObject();
             //查询条件
            if (!queryParam["keyword"].IsEmpty())
            {
                string keyord = queryParam["keyword"].ToString();
                expression = expression.And(t => t.OrderNum.Contains(keyord));
            }
            if (!queryParam["Status"].IsEmpty())
            {
                int Status = int.Parse(queryParam["Status"].ToString());
                expression = expression.And(t => t.Status == Status);
            }
            if (!queryParam["OrderNum"].IsEmpty())
            {
                string OrderNum = queryParam["OrderNum"].ToString();
                expression = expression.And(t => t.OrderNum.Contains(OrderNum));
            }
            if (!queryParam["DeviceCode"].IsEmpty())
            {
                string DeviceCode = queryParam["DeviceCode"].ToString();
                expression = expression.And(t => t.DeviceCode.Contains(DeviceCode));
            }
            if (!queryParam["MouldCode"].IsEmpty())
            {
                string MouldCode = queryParam["MouldCode"].ToString();
                expression = expression.And(t => t.MouldCode.Contains(MouldCode));
            }
            return service.GetPageList(pagination, expression);
        }
        
        /// <summary>
        /// 列表--分页
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<IOTOrderEntity> GetPageList(Pagination pagination, string queryJson, int flag)
        {
			var expression = LinqExtensions.True<IOTOrderEntity>();
            var queryParam = queryJson.ToJObject();
             //查询条件
            if (!queryParam["keyword"].IsEmpty())
            {
                string keyord = queryParam["keyword"].ToString();
                expression = expression.And(t => t.OrderNum.Contains(keyord));
            }
            if (!queryParam["Status"].IsEmpty())
            {
                int Status = int.Parse(queryParam["Status"].ToString());
                expression = expression.And(t => t.Status == Status);
            }
            if (!queryParam["OrderNum"].IsEmpty())
            {
                string OrderNum = queryParam["OrderNum"].ToString();
                expression = expression.And(t => t.OrderNum.Contains(OrderNum));
            }
            if (!queryParam["DeviceCode"].IsEmpty())
            {
                string DeviceCode = queryParam["DeviceCode"].ToString();
                expression = expression.And(t => t.DeviceCode.Contains(DeviceCode));
            }
            if (!queryParam["MouldCode"].IsEmpty())
            {
                string MouldCode = queryParam["MouldCode"].ToString();
                expression = expression.And(t => t.MouldCode.Contains(MouldCode));
            }
            expression = expression.And(t => t.Status == flag);
            return service.GetPageList(pagination, expression);
        }

        /// <summary>
        /// 实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public IOTOrderEntity GetForm(string keyValue)
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
        public void SaveForm(string keyValue, IOTOrderEntity iOTOrderEntity)
        {
            try
            {
                service.SaveForm(keyValue, iOTOrderEntity);
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
                var entity =  service.GetForm(keyValue);
                if (entity == null) 
                {
                    throw new Exception("当前单据已不存在！");
                }
                if (entity.Status.Value > 0) 
                {
                    throw new Exception("当前单据不能进行删除操作！");
                }
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
