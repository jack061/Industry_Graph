/********************************************************************************
**文 件 名:VMWFaceSampleRepository
**命名空间:JFine.Plugins.VMW.Busines.VMW
**描    述:
**
**
**版 本 号:V1.0.0.0
**创 建 人:此代码由T4模板自动生成
**创建日期:2019-04-01 16:03:55
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
using JFine.Plugins.VMW.Domain.Models.VMW;
using JFine.Data.Repository;
using JFine.Plugins.VMW.Domain.IRepository.VMW;
using JFine.Common.UI;
using JFine.Common.Extend;
using JFine.Common.Json;
using System.Data.Common;
using System.Linq.Expressions;

namespace JFine.Plugins.VMW.Domain.Repository.VMW
{	
	/// <summary>
	/// VMWFaceSampleRepository
	/// </summary>	
	public class VMWFaceSampleRepository:RepositoryFactory<VMWFaceSampleEntity>, IVMWFaceSampleRepository
	{
		 #region 数据获取
        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<VMWFaceSampleEntity> GetList()
        {
            return this.BaseRepository().IQueryable().OrderByDescending(t => t.CreateDate).ToList();
        
		}

		/// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<VMWFaceSampleEntity> GetListBySql(string sqlWhere)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT * 
                            FROM   VMW_FaceSample
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
        public IEnumerable<VMWFaceSampleEntity> GetPageListBySql(Pagination pagination, string sqlWhere, List<DbParameter> parameter)
        {

            var strSql = new StringBuilder();
            strSql.Append(@"SELECT * 
                            FROM   VMW_FaceSample
                            WHERE  1=1 ");
            strSql.Append(sqlWhere);

            return this.BaseRepository().FindList(strSql.ToString(),parameter, pagination);

        }

		/// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<VMWFaceSampleEntity> GetList(Expression<Func<VMWFaceSampleEntity, bool>> condition)
        {
            return this.BaseRepository().IQueryable(condition).ToList();

        }


		/// <summary>
        /// 列表--分页
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<VMWFaceSampleEntity> GetPageList(Pagination pagination, Expression<Func<VMWFaceSampleEntity, bool>> condition)
        {
            return this.BaseRepository().FindList(condition, pagination);
        }

        /// <summary>
        /// 实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public VMWFaceSampleEntity GetForm(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region 数据处理
        
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="vMWFaceSampleEntity">实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, VMWFaceSampleEntity vMWFaceSampleEntity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                vMWFaceSampleEntity.Modify(keyValue);
                this.BaseRepository().Update(vMWFaceSampleEntity);
            }
            else
            {
                vMWFaceSampleEntity.Create();
                this.BaseRepository().Insert(vMWFaceSampleEntity);
            }
        }

        /// <summary>
        /// 更新列表
        /// </summary>
        /// <param name=vMWFaceSampleList">列表</param>
        /// <returns></returns>
        public void UpdateList(List<VMWFaceSampleEntity> vMWFaceSampleList) 
        {
            JFine.Data.Repository.IRepositoryBase db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                foreach (var entity in vMWFaceSampleList)
                {
                    db.Update(entity);
                }
                
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


