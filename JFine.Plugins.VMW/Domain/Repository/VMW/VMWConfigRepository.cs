/********************************************************************************
**文 件 名:VMWConfigRepository
**命名空间:JFine.Plugins.VMW.Busines.VMW
**描    述:
**
**
**版 本 号:V1.0.0.0
**创 建 人:此代码由T4模板自动生成
**创建日期:2019-02-26 09:18:13
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
	/// VMWConfigRepository
	/// </summary>	
	public class VMWConfigRepository:RepositoryFactory<VMWConfigEntity>, IVMWConfigRepository
	{
		 #region 数据获取
        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<VMWConfigEntity> GetList()
        {
            return this.BaseRepository().IQueryable().OrderByDescending(t => t.CreateDate).ToList();
        
		}

		/// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<VMWConfigEntity> GetListBySql(string sqlWhere)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT * 
                            FROM   VMW_Config
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
        public IEnumerable<VMWConfigEntity> GetPageListBySql(Pagination pagination, string sqlWhere, List<DbParameter> parameter)
        {

            var strSql = new StringBuilder();
            strSql.Append(@"SELECT * 
                            FROM   VMW_Config
                            WHERE  1=1 ");
            strSql.Append(sqlWhere);

            return this.BaseRepository().FindList(strSql.ToString(),parameter, pagination);

        }

		/// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<VMWConfigEntity> GetList(Expression<Func<VMWConfigEntity, bool>> condition)
        {
            return this.BaseRepository().IQueryable(condition).ToList();

        }


		/// <summary>
        /// 列表--分页
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<VMWConfigEntity> GetPageList(Pagination pagination, Expression<Func<VMWConfigEntity, bool>> condition)
        {
            return this.BaseRepository().FindList(condition, pagination);
        }

        /// <summary>
        /// 实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public VMWConfigEntity GetForm(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        
        /// <summary>
        /// 实体
        /// </summary>
        /// <param name="code">code</param>
        /// <returns></returns>
        public VMWConfigEntity GetFormByCode(string code)
        {
            return this.BaseRepository().FindEntity(t => t.Code.Equals(code));
        }

        #endregion

        #region 数据处理
        
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="vMWConfigEntity">实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, VMWConfigEntity vMWConfigEntity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                var temp = this.BaseRepository().FindEntity(t => t.Code.Equals(vMWConfigEntity.Code) && t.Id != keyValue);
                if (temp != null)
                {
                    throw new Exception("编码《" + vMWConfigEntity.Code + "》已经存在。");
                }
                vMWConfigEntity.Modify(keyValue);
                this.BaseRepository().Update(vMWConfigEntity);
            }
            else
            {
                var temp = this.BaseRepository().FindEntity(t => t.Code.Equals(vMWConfigEntity.Code));
                if (temp != null) 
                {
                    throw new Exception("编码《" + vMWConfigEntity.Code + "》已经存在。");
                }
                vMWConfigEntity.Create();
                this.BaseRepository().Insert(vMWConfigEntity);
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


