/********************************************************************************
**文 件 名:VMWWarningRepository
**命名空间:JFine.Plugins.VMW.Busines.VMW
**描    述:
**
**
**版 本 号:V1.0.0.0
**创 建 人:此代码由T4模板自动生成
**创建日期:2019-02-21 17:18:45
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
using JFine.Data.Common;

namespace JFine.Plugins.VMW.Domain.Repository.VMW
{	
	/// <summary>
	/// VMWWarningRepository
	/// </summary>	
	public class VMWWarningRepository:RepositoryFactory<VMWWarningEntity>, IVMWWarningRepository
	{
		 #region 数据获取
        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<VMWWarningEntity> GetList()
        {
            return this.BaseRepository().IQueryable().OrderByDescending(t => t.CreateDate).ToList();
        
		}

		/// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<VMWWarningEntity> GetListBySql(string sqlWhere)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT * 
                            FROM   VMW_Warning
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
        public IEnumerable<VMWWarningEntity> GetPageListBySql(Pagination pagination, string sqlWhere, List<DbParameter> parameter)
        {

            var strSql = new StringBuilder();
            strSql.Append(@"SELECT * 
                            FROM   VMW_Warning
                            WHERE  1=1 ");
            strSql.Append(sqlWhere);

            return this.BaseRepository().FindList(strSql.ToString(),parameter, pagination);

        }

		/// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<VMWWarningEntity> GetList(Expression<Func<VMWWarningEntity, bool>> condition)
        {
            return this.BaseRepository().IQueryable(condition).ToList();

        }

        /// <summary>
        /// 获取最新的N条数据 最多返回50条
        /// </summary>
        /// <param name="topN">最新条数</param>
        /// <returns></returns>
        public IEnumerable<VMWWarningEntity> GetTopWarnList(int topN)
        {
            var strSql = new StringBuilder();
            switch (DbHelper.DbType)
            {
                case DatabaseType.SqlServer:
                    strSql.Append(@"SELECT top " + topN + @" * 
                            FROM   VMW_Warning
                            order by WarningDate desc ");
                    break;
                case DatabaseType.MySql:
                    strSql.Append(@"SELECT  * 
                            FROM   VMW_Warning  
                            order by WarningDate desc limit " + topN );
                    break;
                default:
                    throw new Exception("数据库类型目前不支持！");
            }
           

            return this.BaseRepository().FindList(strSql.ToString());

        }


		/// <summary>
        /// 列表--分页
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<VMWWarningEntity> GetPageList(Pagination pagination, Expression<Func<VMWWarningEntity, bool>> condition)
        {
            return this.BaseRepository().FindList(condition, pagination);
        }

        /// <summary>
        /// 实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public VMWWarningEntity GetForm(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region 数据处理
        
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="vMWWarningEntity">实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, VMWWarningEntity vMWWarningEntity)
        {
            JFine.Data.Repository.IRepositoryBase db = new RepositoryFactory().BaseRepository().BeginTrans();

            if (!string.IsNullOrEmpty(keyValue))
            {
                vMWWarningEntity.Modify(keyValue);
                db.Update(vMWWarningEntity);
            }
            else
            {
                DateTime dt = DateTime.Now;
                vMWWarningEntity.Create();
                vMWWarningEntity.Year = dt.Year;
                vMWWarningEntity.Month = dt.Month;
                vMWWarningEntity.DAY = dt.Day;
                db.Insert(vMWWarningEntity);
            }

            //更新摄像头信息
            db.ExecuteBySql("Update VMW_Camera set  WarningCount = WarningCount +1, FirstWDT = CASE WHEN FirstWDT IS NULL THEN '" 
                + ((DateTime)vMWWarningEntity.WarningDate).ToString("yyyy-MM-dd HH:mm:ss")
                + "' ELSE FirstWDT END, LastWDT = '" + ((DateTime)vMWWarningEntity.WarningDate).ToString("yyyy-MM-dd HH:mm:ss") 
                + "',Status =2 WHERE Code = '" + vMWWarningEntity.CameraCode + "'");
            try
            {
                db.Commit();
            }
            catch (Exception e)
            {
                db.Rollback();
                throw;
            }
        }
        
        /// <summary>
        /// 预警处理
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="vMWWarningEntity">实体</param>
        /// <returns></returns>
        public void DealWithWarn(string keyValue, VMWWarningEntity vMWWarningEntity)
        {
            var tempWarn = this.BaseRepository().FindEntity(keyValue);

            JFine.Data.Repository.IRepositoryBase db = new RepositoryFactory().BaseRepository().BeginTrans();

            if (tempWarn == null)
            {
                throw new Exception("预警信息获取失败");
            }
            var warnListUnDealList = db.FindList<VMWWarningEntity>(t => t.CameraCode.Equals(tempWarn.CameraCode) && t.Id != keyValue && (t.ConfirmStatus.Equals(VMWConstant.STATU_CONFIRM_UN) || t.DealStatus.Equals(VMWConstant.STATU_DEAL_UN)));

            if (warnListUnDealList.Count() == 0)
            {
                //更新摄像头状态为 正常
                db.ExecuteBySql("Update VMW_Camera set  Status =1 WHERE Code = '" + tempWarn.CameraCode + "'");
            }

            if (VMWConstant.STATU_CONFIRM_IGNORE.Equals(vMWWarningEntity.ConfirmStatus)) 
            {
                db.ExecuteBySql("Update VMW_Camera set  ErrorCount = ErrorCount + 1 WHERE Code = '" + tempWarn.CameraCode + "'");
            }

            vMWWarningEntity.Modify(keyValue);
            db.Update(vMWWarningEntity);
           
            try
            {
                db.Commit();
            }
            catch (Exception e)
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


