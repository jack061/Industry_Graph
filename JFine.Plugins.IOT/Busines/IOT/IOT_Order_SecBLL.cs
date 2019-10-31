﻿
/********************************************************************************
**文 件 名:IOT_Order_SecBLL
**命名空间:JFine.Plugins.IOT.Busines.IOT
**描    述:
**
**
**版 本 号:V1.0.0.0
**创 建 人:此代码由T4模板自动生成
**创建日期:2019-09-30 14:27:41
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
    /// IOT_Order_SecBLL
    /// </summary>	
    public class IOT_Order_SecBLL
    {
        private IIOT_Order_SecRepository service = new IOT_Order_SecRepository();

        /// <summary>
        /// 缓存key
        /// </summary>
        public string cacheKey = "IOT_Order_SecCache";

        #region 数据获取

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IOT_Order_SecEntity> GetList()
        {
            return service.GetList();
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IOT_Order_SecEntity> GetListBySql(string sqlWhere)
        {
            return service.GetListBySql(sqlWhere);
        }

        /// <summary>
        /// 列表--分页
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<IOT_Order_SecEntity> GetPageListBySql(Pagination pagination, string queryJson)
        {
            var sqlWhere = new StringBuilder();
            var queryParam = queryJson.ToJObject();
            List<DbParameter> parameter = new List<DbParameter>();
            //查询条件
            if (!queryParam["keyword"].IsEmpty())
            {
                string keyword = queryParam["keyword"].ToString();
                sqlWhere.Append(" AND (Code like @keyword or Name like @keyword)");
                parameter.Add(DbParameters.CreateDbParameter("@keyword", "%" + keyword + "%", DbType.AnsiString));
            }

            return service.GetPageListBySql(pagination, sqlWhere.ToString(), parameter);
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IOT_Order_SecEntity> GetList(string queryJson)
        {
            var expression = LinqExtensions.True<IOT_Order_SecEntity>();
            var queryParam = queryJson.ToJObject();
            //查询条件
            if (!queryParam["Name"].IsEmpty())
            {
                string name = queryParam["Name"].ToString();
                expression = expression.And(t => t.Id.Contains(name));
            }
            return service.GetList(expression);
        }

        /// <summary>
        /// 列表--分页
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<IOT_Order_SecEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<IOT_Order_SecEntity>();
            var queryParam = queryJson.ToJObject();
            //查询条件
            if (!queryParam["orderDate_Search"].IsEmpty())
            {
                string orderDate_Search = queryParam["orderDate_Search"].ToString();
                DateTime? orderDate = DateTime.Parse(orderDate_Search);
                expression = expression.And(t => t.OrderDate == orderDate);
            }
            if (!queryParam["orderDate_Begin_Search"].IsEmpty())
            {
                string orderDate_Begin_Search = queryParam["orderDate_Begin_Search"].ToString();
                DateTime? orderDate_Begin = DateTime.Parse(orderDate_Begin_Search);
                expression = expression.And(t => orderDate_Begin >= t.OrderBegin_Time);
            }
            if (!queryParam["orderDate_End_Search"].IsEmpty())
            {
                string orderDate_End_Search = queryParam["orderDate_End_Search"].ToString();
                DateTime? orderDate_End = DateTime.Parse(orderDate_End_Search);
                expression = expression.And(t => orderDate_End <= t.OrderEnd_Time);
            }
            return service.GetPageList(pagination, expression);
        }

        /// <summary>
        /// 实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public IOT_Order_SecEntity GetForm(string keyValue)
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
        public void SaveForm(string keyValue, IOT_Order_SecEntity iOT_Order_SecEntity)
        {
            try
            {
                service.SaveForm(keyValue, iOT_Order_SecEntity);
                CacheFactory.Cache().WriteCache(cacheKey, iOT_Order_SecEntity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void SaveChangeQForm(string keyValue, IOT_Order_SecEntity iOT_Order_SecEntity)
        {
            try
            {
                service.SaveChangeQForm(keyValue, iOT_Order_SecEntity);
                CacheFactory.Cache().WriteCache(cacheKey, iOT_Order_SecEntity);
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
                CacheFactory.Cache().RemoveCache(cacheKey);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void OrderStart(string keyValue)
        {
            try
            {
                service.OrderStart(keyValue);
                CacheFactory.Cache().RemoveCache(cacheKey);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void ChangeProceedOrder(string keyValue)
        {
            try
            {
                service.ChangeProceedOrder(keyValue);
                CacheFactory.Cache().RemoveCache(cacheKey);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateOrderQuantity(string keyValue, string quantityType)
        {
            try
            {
                service.UpdateOrderQuantity(keyValue, quantityType);
                CacheFactory.Cache().RemoveCache(cacheKey);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

    }
}
