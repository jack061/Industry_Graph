/********************************************************************************
**文 件 名:WF_TestRepository
**命名空间:JFine.Busines.Test
**描    述:
**
**
**版 本 号:V1.0.0.0
**创 建 人:此代码由T4模板自动生成
**创建日期:2018-09-11 18:49:17
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
using JFine.Data.Repository;
using JFine.Domain.IRepository.Test;
using JFine.Common.UI;
using JFine.Common.Extend;
using JFine.Common.Json;
using System.Linq.Expressions;
using System.Data.Common;
using JFine.WorkFlow;

namespace JFine.Domain.Repository.Test
{	
	/// <summary>
	/// WF_TestRepository
	/// </summary>	
	public class WF_TestRepository:RepositoryFactory<WF_TestEntity>, IWF_TestRepository
	{
		 #region 数据获取
        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<WF_TestEntity> GetList()
        {
            return this.BaseRepository().IQueryable().OrderByDescending(t => t.CreateDate).ToList();
        
		}

		/// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<WF_TestEntity> GetListBySql(string sqlWhere)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT * 
                            FROM   WF_Test
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
        public IEnumerable<WF_TestEntity> GetPageListBySql(Pagination pagination, string sqlWhere, List<DbParameter> parameter)
        {

            var strSql = new StringBuilder();
            strSql.Append(@"SELECT * 
                            FROM   WF_Test
                            WHERE  1=1 ");
            strSql.Append(sqlWhere);

            return this.BaseRepository().FindList(strSql.ToString(),parameter, pagination);

        }

		/// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<WF_TestEntity> GetList(Expression<Func<WF_TestEntity, bool>> condition)
        {
            return this.BaseRepository().IQueryable(condition).ToList();

        }


		/// <summary>
        /// 列表--分页
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<WF_TestEntity> GetPageList(Pagination pagination, Expression<Func<WF_TestEntity, bool>> condition)
        {
            return this.BaseRepository().FindList(condition, pagination);
        }

        /// <summary>
        /// 实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public WF_TestEntity GetForm(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region 数据处理
        
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="wF_TestEntity">实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, WF_TestEntity wF_TestEntity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                wF_TestEntity.Modify(keyValue);
                this.BaseRepository().Update(wF_TestEntity);
            }
            else
            {
                wF_TestEntity.Create();
                this.BaseRepository().Insert(wF_TestEntity);
            }
        }
        /// <summary>
        /// 提交数据--事务内完成
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="wF_TestEntity">实体</param>
        /// <returns></returns>
        public void SubmitForm_0(string keyValue, WF_TestEntity wF_TestEntity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                wF_TestEntity.Modify(keyValue);
            }
            else
            {
                wF_TestEntity.Create();    
            }
            WorkFlowEngine wfe = WorkFlowEngine.getWFEngine();
            string instanceId = "";
            JFine.Data.Repository.IRepositoryBase db = wfe.GetStartWFTrans(wF_TestEntity.WFId, wF_TestEntity.Id, ref instanceId);
            wF_TestEntity.BindId = instanceId;
            if (!string.IsNullOrEmpty(keyValue))
            {
                db.Update(wF_TestEntity);
            }
            else
            {
                db.Insert(wF_TestEntity);
            }

            try
            {
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }

        }

        // <summary>
        /// 提交数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="wF_TestEntity">实体</param>
        /// <returns></returns>
        public void SubmitForm(string keyValue, WF_TestEntity wF_TestEntity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                wF_TestEntity.Modify(keyValue);
            }
            else
            {
                wF_TestEntity.Create();
            }
            JFine.Data.Repository.IRepositoryBase db = new RepositoryFactory().BaseRepository().BeginTrans(); 
            if (!string.IsNullOrEmpty(keyValue))
            {
                db.Update(wF_TestEntity);
            }
            else
            {
                db.Insert(wF_TestEntity);
            }

            try
            {
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
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


