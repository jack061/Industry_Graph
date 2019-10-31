
/********************************************************************************
**文 件 名:IOTProductPlanBLL
**命名空间:JFine.Plugins.IOT.Busines.IOT
**描    述:
**
**
**版 本 号:V1.0.0.0
**创 建 人:此代码由T4模板自动生成
**创建日期:2019-08-06 14:34:14
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
	/// IOTProductPlanBLL
	/// </summary>	
	public class IOTProductPlanBLL
	{
	    private IIOTProductPlanRepository service=new IOTProductPlanRepository();

		/// <summary>
        /// 缓存key
        /// </summary>
        public string cacheKey ="IOTProductPlanCache" ;

        #region 数据获取

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IOTProductPlanEntity> GetList()
        {
            return service.GetList();
        }

		/// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IOTProductPlanEntity> GetListBySql( string sqlWhere)
        {
			return service.GetListBySql(sqlWhere);
        }

		/// <summary>
        /// 列表--分页
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<IOTProductPlanEntity> GetPageListBySql(Pagination pagination, string queryJson)
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
        public IEnumerable<IOTProductPlanEntity> GetList( string queryJson)
        {
             var expression = LinqExtensions.True<IOTProductPlanEntity>();
            var queryParam = queryJson.ToJObject();
            //查询条件
            if (!queryParam["Code"].IsEmpty())
            {
                string Code = queryParam["Code"].ToString();
                expression = expression.And(t => t.Code.Contains(Code));
            }
			return service.GetList(expression);
        }

        /// <summary>
        /// 列表--分页
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<IOTProductPlanEntity> GetPageList(Pagination pagination, string queryJson)
        {
			var expression = LinqExtensions.True<IOTProductPlanEntity>();
            var queryParam = queryJson.ToJObject();
             //查询条件
            if (!queryParam["keyword"].IsEmpty())
            {
                string keyord = queryParam["keyword"].ToString();
                expression = expression.And(t => t.Code.Contains(keyord));
            }
            return service.GetPageList(pagination, expression);
        }

        /// <summary>
        /// 实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public IOTProductPlanEntity GetForm(string keyValue)
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
        public void SaveForm(string keyValue, IOTProductPlanEntity iOTProductPlanEntity)
        {
            try
            {
                service.SaveForm(keyValue, iOTProductPlanEntity);
                CacheFactory.Cache().WriteCache(cacheKey,iOTProductPlanEntity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        /// <summary>
        /// 插入列表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public void SaveForm(IOTProductPlanEntity iOTProductPlanEntity,List<IOTOrderEntity> orderList)
        {
            try
            {
                IOTDeviceBLL iOTDeviceBLL = new IOT.IOTDeviceBLL();
                IOTMouldBLL iOTMouldBLL = new IOT.IOTMouldBLL();
                IOTProductBLL iOTProductBLL = new IOT.IOTProductBLL();

                List<IOTDeviceEntity> deviceList = new List<IOTDeviceEntity>();
                List<IOTMouldEntity> mouldList = new List<IOTMouldEntity>();
                List<IOTProductEntity> productList = new List<IOTProductEntity>();
                deviceList = iOTDeviceBLL.GetList().ToList();
                mouldList = iOTMouldBLL.GetList().ToList();
                productList = iOTProductBLL.GetList().ToList();

                iOTProductPlanEntity.Create();
                foreach (var order in orderList)
                {
                    order.Id = Sequence.SequenceHelper.getSnowflakeID();
                    order.BindId = iOTProductPlanEntity.Id;
                    order.CreateDate = iOTProductPlanEntity.CreateDate;
                    order.CreateUserId = iOTProductPlanEntity.CreateUserId;
                    order.CreateUserName = iOTProductPlanEntity.CreateUserName;
                    order.Status = 0;

                    var deivice = deviceList.SingleOrDefault(t=>t.Code.Equals(order.DeviceCode));
                    if (deivice == null) 
                    {
                        throw new Exception("系统内不存在编码为《"+  order.DeviceCode +"》的设备，请检查");
                    }
                    order.DeviceName = deivice.Name;
                   
                    var mould = mouldList.SingleOrDefault(t => t.Code.Equals(order.MouldCode));
                    if (mould == null)
                    {
                        throw new Exception("系统内不存在编码为《" + order.MouldCode + "》的模具，请检查");
                    }
                    order.MouldName = mould.Name;

                    var product = productList.SingleOrDefault(t => t.Code.Equals(order.ProductCode));
                    if (product == null)
                    {
                        throw new Exception("系统内不存在编码为《" + order.ProductCode + "》的产品，请检查");
                    }
                    order.ProductName = product.Name;
                   
                    if (order.PlanQuantity == null || order.PlanQuantity.Value <= 0) 
                    {
                        throw new Exception("计划数量不能为空或者小于0，请检查");
                    }
                    if (order.PlanStartTime == null || order.PlanStartTime<iOTProductPlanEntity.CreateDate)
                    {
                        throw new Exception("计划开始时间不能为空或者小于当前时间，请检查");
                    }

                    if (order.PlanEndTime == null || order.PlanEndTime < order.PlanStartTime)
                    {
                        throw new Exception("计划结束时间不能为空或者小于计划开始时间，请检查");
                    }

                }
                service.SaveForm(iOTProductPlanEntity, orderList);
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

        #endregion

    }
}
