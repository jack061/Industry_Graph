/********************************************************************************
**文 件 名:IOTDeviceWarnRepository
**命名空间:JFine.Plugins.IOT.Busines.IOT
**描    述:
**
**
**版 本 号:V1.0.0.0
**创 建 人:此代码由T4模板自动生成
**创建日期:2019-07-01 17:21:29
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
using JFine.Plugins.IOT.Domain.Models.IOT;
using JFine.Data.Repository;
using JFine.Plugins.IOT.Domain.IRepository.IOT;
using JFine.Common.UI;
using JFine.Common.Extend;
using JFine.Common.Json;
using System.Data.Common;
using System.Linq.Expressions;

namespace JFine.Plugins.IOT.Domain.Repository.IOT
{	
	/// <summary>
	/// IOTDeviceWarnRepository
	/// </summary>	
	public class IOTDeviceWarnRepository:RepositoryFactory<IOTDeviceWarnEntity>, IIOTDeviceWarnRepository
	{
		 #region 数据获取
        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IOTDeviceWarnEntity> GetList()
        {
            return this.BaseRepository().IQueryable().OrderByDescending(t => t.CreateDate).ToList();
        
		}

		/// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IOTDeviceWarnEntity> GetListBySql(string sqlWhere)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT * 
                            FROM   IOT_DeviceWarning
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
        public IEnumerable<IOTDeviceWarnEntity> GetPageListBySql(Pagination pagination, string sqlWhere, List<DbParameter> parameter)
        {

            var strSql = new StringBuilder();
            strSql.Append(@"SELECT * 
                            FROM   IOT_DeviceWarning
                            WHERE  1=1 ");
            strSql.Append(sqlWhere);

            return this.BaseRepository().FindList(strSql.ToString(),parameter, pagination);

        }

		/// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IOTDeviceWarnEntity> GetList(Expression<Func<IOTDeviceWarnEntity, bool>> condition)
        {
            return this.BaseRepository().IQueryable(condition).ToList();

        }


		/// <summary>
        /// 列表--分页
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<IOTDeviceWarnEntity> GetPageList(Pagination pagination, Expression<Func<IOTDeviceWarnEntity, bool>> condition)
        {
            return this.BaseRepository().FindList(condition, pagination);
        }

        /// <summary>
        /// 实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public IOTDeviceWarnEntity GetForm(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region 数据处理
        
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="iOTDeviceWarnEntity">实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, IOTDeviceWarnEntity iOTDeviceWarnEntity)
        {
            JFine.Data.Repository.IRepositoryBase db = new RepositoryFactory().BaseRepository().BeginTrans();

            if (!string.IsNullOrEmpty(keyValue))
            {
                iOTDeviceWarnEntity.Modify(keyValue);
                db.Update(iOTDeviceWarnEntity);
            }
            else
            {
                DateTime dt = DateTime.Now;
                iOTDeviceWarnEntity.Create();
                iOTDeviceWarnEntity.Year = dt.Year;
                iOTDeviceWarnEntity.Month = dt.Month;
                iOTDeviceWarnEntity.DAY = dt.Day;
                db.Insert(iOTDeviceWarnEntity);
            }

            //更新摄像头信息
            db.ExecuteBySql("Update IOT_Device set  WarningCount = WarningCount +1, FirstWDT = CASE WHEN FirstWDT IS NULL THEN '"
                + ((DateTime)iOTDeviceWarnEntity.WarningDate).ToString("yyyy-MM-dd HH:mm:ss")
                + "' ELSE FirstWDT END, LastWDT = '" + ((DateTime)iOTDeviceWarnEntity.WarningDate).ToString("yyyy-MM-dd HH:mm:ss")
                + "',Status =2 WHERE Code = '" + iOTDeviceWarnEntity.DeviceCode + "'");
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
        /// <param name="iOTDeviceWarnEntity">实体</param>
        /// <returns></returns>
        public void DealWithWarn(string keyValue, IOTDeviceWarnEntity iOTDeviceWarnEntity)
        {
            var tempWarn = this.BaseRepository().FindEntity(keyValue);

            JFine.Data.Repository.IRepositoryBase db = new RepositoryFactory().BaseRepository().BeginTrans();

            if (tempWarn == null)
            {
                throw new Exception("预警信息获取失败");
            }
            var warnListUnDealList = db.FindList<IOTDeviceWarnEntity>(t => t.DeviceCode.Equals(tempWarn.DeviceCode) && t.Id != keyValue && (t.ConfirmStatus.Equals(IOTConstant.STATU_CONFIRM_UN) || t.DealStatus.Equals(IOTConstant.STATU_DEAL_UN)));

            if (warnListUnDealList.Count() == 0)
            {
                //更新摄像头状态为 正常
                db.ExecuteBySql("Update IOT_Device set  Status =1 WHERE Code = '" + tempWarn.DeviceCode + "'");
            }

            if (IOTConstant.STATU_CONFIRM_IGNORE.Equals(iOTDeviceWarnEntity.ConfirmStatus))
            {
                db.ExecuteBySql("Update IOT_Device set  ErrorCount = ErrorCount + 1 WHERE Code = '" + tempWarn.DeviceCode + "'");
            }

            iOTDeviceWarnEntity.Modify(keyValue);
            db.Update(iOTDeviceWarnEntity);

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


