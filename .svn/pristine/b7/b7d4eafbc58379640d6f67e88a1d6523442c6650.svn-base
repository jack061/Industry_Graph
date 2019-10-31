
/********************************************************************************
**文 件 名:WF_TestBLL
**命名空间:JFine.Busines.Test
**描    述:
**
**
**版 本 号:V1.0.0.0
**创 建 人:此代码由T4模板自动生成
**创建日期:2018-09-11 18:49:12
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
using JFine.Domain.Models.Test;
using JFine.Domain.IRepository.Test;
using JFine.Domain.Repository.Test;
using JFine.Cache;
using JFine.Common.UI;
using JFine.Common.Extend;
using JFine.Common.Json;
using System.Linq.Expressions;
using System.Data.Common;
using JFine.Data.Common;
using System.Data;
using JFine.WorkFlow;

namespace JFine.Busines.Test
{	
	/// <summary>
	/// WF_TestBLL
	/// </summary>	
	public class WF_TestBLL
	{
	    private IWF_TestRepository service=new WF_TestRepository();

        #region 数据获取

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<WF_TestEntity> GetList()
        {
            return service.GetList();
        }

		/// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<WF_TestEntity> GetListBySql( string sqlWhere)
        {
			return service.GetListBySql(sqlWhere);
        }

		/// <summary>
        /// 列表--分页
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<WF_TestEntity> GetPageListBySql(Pagination pagination, string queryJson)
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
        public IEnumerable<WF_TestEntity> GetList( string queryJson)
        {
             var expression = LinqExtensions.True<WF_TestEntity>();
            var queryParam = queryJson.ToJObject();
            //查询条件
            if (!queryParam["Name"].IsEmpty())
            {
                string name = queryParam["Name"].ToString();
                expression = expression.And(t => t.Name.Contains(name));
            }
			return service.GetList(expression);
        }

        /// <summary>
        /// 列表--分页
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<WF_TestEntity> GetPageList(Pagination pagination, string queryJson)
        {
			var expression = LinqExtensions.True<WF_TestEntity>();
            var queryParam = queryJson.ToJObject();
             //查询条件
            if (!queryParam["keyword"].IsEmpty())
            {
                string keyord = queryParam["keyword"].ToString();
                expression = expression.And(t => t.Name.Contains(keyord));
            }
            return service.GetPageList(pagination, expression);
        }

        /// <summary>
        /// 实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public WF_TestEntity GetForm(string keyValue)
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
        public void SaveForm(string keyValue, WF_TestEntity wF_TestEntity)
        {
            WorkFlowEngine wfe = WorkFlowEngine.getWFEngine();
            try
            {

                //1、创建流程实例
                /*
                if (wF_TestEntity.BindId.IsEmpty() && wF_TestEntity.Id.IsEmpty()) 
                {
                    string wfInstanceId = wfe.CreateWFInstance(wF_TestEntity.WFId, "");
                    wF_TestEntity.BindId = wfInstanceId;
                }
                 * */
                //2、保存业务数据
                service.SaveForm(keyValue, wF_TestEntity);

               //3、启动流程实例


            }
            catch (Exception)
            {
                throw;
            }
        }
        
        /// <summary>
        /// 提交数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public void SubmitForm(string keyValue, WF_TestEntity wF_TestEntity)
        {
            try
            {
                service.SubmitForm(keyValue, wF_TestEntity);
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
