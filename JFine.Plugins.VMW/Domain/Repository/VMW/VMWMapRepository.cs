/********************************************************************************
**文 件 名:VMWMapRepository
**命名空间:JFine.Plugins.VMW.Busines.VMW
**描    述:
**
**
**版 本 号:V1.0.0.0
**创 建 人:此代码由T4模板自动生成
**创建日期:2019-02-21 17:17:04
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
	/// VMWMapRepository
	/// </summary>	
	public class VMWMapRepository:RepositoryFactory<VMWMapEntity>, IVMWMapRepository
	{
		 #region 数据获取
        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<VMWMapEntity> GetList()
        {
            return this.BaseRepository().IQueryable().OrderByDescending(t => t.CreateDate).ToList();
        
		}

		/// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<VMWMapEntity> GetListBySql(string sqlWhere)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT * 
                            FROM   VMW_Map
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
        public IEnumerable<VMWMapEntity> GetPageListBySql(Pagination pagination, string sqlWhere, List<DbParameter> parameter)
        {

            var strSql = new StringBuilder();
            strSql.Append(@"SELECT * 
                            FROM   VMW_Map
                            WHERE  1=1 ");
            strSql.Append(sqlWhere);

            return this.BaseRepository().FindList(strSql.ToString(),parameter, pagination);

        }

		/// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<VMWMapEntity> GetList(Expression<Func<VMWMapEntity, bool>> condition)
        {
            return this.BaseRepository().IQueryable(condition).ToList();

        }


		/// <summary>
        /// 列表--分页
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<VMWMapEntity> GetPageList(Pagination pagination, Expression<Func<VMWMapEntity, bool>> condition)
        {
            return this.BaseRepository().FindList(condition, pagination);
        }

        /// <summary>
        /// 实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public VMWMapEntity GetForm(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        
        /// <summary>
        /// 实体
        /// </summary>
        /// <param name="code">mapCode</param>
        /// <returns></returns>
        public VMWMapEntity GetMapFromCode(string code)
        {
            return this.BaseRepository().FindEntity(t=>t.Code.Equals(code));
        }
        #endregion

        #region 数据处理
        
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="vMWMapEntity">实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, VMWMapEntity vMWMapEntity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                var tempEntity = this.BaseRepository().FindEntity(keyValue);
                if (tempEntity == null)
                {
                    throw new Exception("该地图已不存在。");
                }
                if (vMWMapEntity.Code != null && !(vMWMapEntity.Code.Equals(tempEntity.Code))) 
                {
                    tempEntity = this.BaseRepository().FindEntity(t => t.Code.Equals(vMWMapEntity.Code));
                    if (tempEntity != null)
                    {
                        throw new Exception("该地图编码已经存在，请修改。");
                    }
                }
                vMWMapEntity.Modify(keyValue);
                this.BaseRepository().Update(vMWMapEntity);
            }
            else
            {
                vMWMapEntity.Create();
                var tempEntity = this.BaseRepository().FindEntity(t => t.Code.Equals(vMWMapEntity.Code));
                if (tempEntity != null) 
                {
                    throw new Exception("该地图编码已经存在，请修改。");
                }
                this.BaseRepository().Insert(vMWMapEntity);
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


