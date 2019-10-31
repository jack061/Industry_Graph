
/********************************************************************************
**文 件 名:VMWWarningBLL
**命名空间:JFine.Plugins.VMW.Busines.VMW
**描    述:
**
**
**版 本 号:V1.0.0.0
**创 建 人:此代码由T4模板自动生成
**创建日期:2019-02-21 17:18:40
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
using JFine.Plugins.VMW.Domain.Models.VMW;
using JFine.Plugins.VMW.Domain.IRepository.VMW;
using JFine.Plugins.VMW.Domain.Repository.VMW;
using JFine.Cache;
using JFine.Common.UI;
using JFine.Common.Extend;
using JFine.Common.Json;
using JFine.Data.Common;
using System.Linq.Expressions;
using System.Data.Common;
using System.Data;
using JFine.Data.Repository;

namespace JFine.Plugins.VMW.Busines.VMW
{	
	/// <summary>
	/// VMWWarningBLL
	/// </summary>	
	public class VMWWarningBLL
	{
	    private IVMWWarningRepository service=new VMWWarningRepository();

		/// <summary>
        /// 缓存key
        /// </summary>
        public string cacheKey ="VMWWarningCache" ;

        #region 数据获取

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<VMWWarningEntity> GetList()
        {
            return service.GetList();
        }

		/// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<VMWWarningEntity> GetListBySql( string sqlWhere)
        {
			return service.GetListBySql(sqlWhere);
        }

		/// <summary>
        /// 列表--分页
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<VMWWarningEntity> GetPageListBySql(Pagination pagination, string queryJson)
        {
            var sqlWhere = new StringBuilder();
            var queryParam = queryJson.ToJObject();
			 List<DbParameter> parameter =  new List<DbParameter>();
             //查询条件
             #region 查询条件
             if (!queryParam["keyword"].IsEmpty())
            {
                string keyword = queryParam["keyword"].ToString();
                sqlWhere.Append(" AND (CameraCode like @keyword or CameraName like @keyword)");
				parameter.Add(DbParameters.CreateDbParameter("@keyword","%"+ keyword +"%",DbType.AnsiString));
            }

             if (!queryParam["CameraCode"].IsEmpty())
             {
                 string CameraCode = queryParam["CameraCode"].ToString();
                 sqlWhere.Append(" AND CameraCode = @CameraCode ");
                 parameter.Add(DbParameters.CreateDbParameter("@CameraCode", CameraCode, DbType.AnsiString));
             }

            if (!queryParam["Code"].IsEmpty())
            {
                string Code = queryParam["Code"].ToString();
                sqlWhere.Append(" AND Code like @Code ");
                parameter.Add(DbParameters.CreateDbParameter("@Code", "%" + Code + "%", DbType.AnsiString));
            }

            if (!queryParam["Position"].IsEmpty())
            {
                string Position = queryParam["Position"].ToString();
                sqlWhere.Append(" AND Position like @Position ");
                parameter.Add(DbParameters.CreateDbParameter("@Position", "%" + Position + "%", DbType.AnsiString));
            }
            if (!queryParam["CategoryCode"].IsEmpty())
            {
                string CategoryCode = queryParam["CategoryCode"].ToString();
                sqlWhere.Append(" AND CategoryCode = @CategoryCode ");
                parameter.Add(DbParameters.CreateDbParameter("@CategoryCode", CategoryCode, DbType.AnsiString));
            }
            if (!queryParam["LevelCode"].IsEmpty())
            {
                string LevelCode = queryParam["LevelCode"].ToString();
                sqlWhere.Append(" AND LevelCode = @LevelCode ");
                parameter.Add(DbParameters.CreateDbParameter("@LevelCode", LevelCode, DbType.AnsiString));
            }
            if (!queryParam["ConfirmStatus"].IsEmpty())
            {
                string ConfirmStatus = queryParam["ConfirmStatus"].ToString();
                sqlWhere.Append(" AND ConfirmStatus = @ConfirmStatus ");
                parameter.Add(DbParameters.CreateDbParameter("@ConfirmStatus", ConfirmStatus, DbType.AnsiString));
            }
            if (!queryParam["DealStatus"].IsEmpty())
            {
                string DealStatus = queryParam["DealStatus"].ToString();
                sqlWhere.Append(" AND DealStatus = @DealStatus ");
                parameter.Add(DbParameters.CreateDbParameter("@DealStatus", DealStatus, DbType.AnsiString));
            }

            if (!queryParam["startDate"].IsEmpty())
            {
                string startDate = queryParam["startDate"].ToString();
                sqlWhere.Append(" AND WarningDate >= @startDate ");
                parameter.Add(DbParameters.CreateDbParameter("@startDate", startDate, DbType.AnsiString));
            }

            if (!queryParam["endDate"].IsEmpty())
            {
                string endDate = queryParam["endDate"].ToString();
                sqlWhere.Append(" AND WarningDate <= @endDate ");
                parameter.Add(DbParameters.CreateDbParameter("@endDate", endDate + " 23:59:59", DbType.AnsiString));
            }
             #endregion
            return service.GetPageListBySql(pagination, sqlWhere.ToString(),parameter);
        }

		/// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<VMWWarningEntity> GetList( string queryJson)
        {
             var expression = LinqExtensions.True<VMWWarningEntity>();
            var queryParam = queryJson.ToJObject();
            //查询条件
            if (!queryParam["CategoryCode"].IsEmpty())
            {
                string name = queryParam["CategoryCode"].ToString();
                expression = expression.And(t => t.CategoryCode.Contains(name));
            }
			return service.GetList(expression);
        }

        /// <summary>
        /// 列表--分页
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<VMWWarningEntity> GetPageList(Pagination pagination, string queryJson)
        {
			var expression = LinqExtensions.True<VMWWarningEntity>();
            var queryParam = queryJson.ToJObject();
             //查询条件
            if (!queryParam["keyword"].IsEmpty())
            {
                string keyord = queryParam["keyword"].ToString();
                expression = expression.And(t => t.CameraCode.Contains(keyord));
            }
            return service.GetPageList(pagination, expression);
        }

        /// <summary>
        /// 获取最新的N条数据 最多返回50条
        /// </summary>
        /// <param name="topN">最新条数</param>
        /// <returns></returns>
        public IEnumerable<VMWWarningEntity> GetTopWarnList(int topN =50)
        {
            if (topN<0 || topN>50)
            {
                topN = 50;
            }
            return service.GetTopWarnList(topN);
        }

        /// <summary>
        /// 实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public VMWWarningEntity GetForm(string keyValue)
        {
            return service.GetForm(keyValue);
        }

        /// <summary>
        /// 获取预警统计数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetWarningStatData()
        {
            DataTable dt = new DataTable();
            List<DbParameter> parameter = new List<DbParameter>();
            dt = new RepositoryFactory().BaseRepository().FindTableByProc("getWarningStatData", parameter);
            return dt;
        }
        #endregion

        #region 数据处理

        

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, VMWWarningEntity vMWWarningEntity)
        {
            try
            {
                service.SaveForm(keyValue, vMWWarningEntity);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 预警处理
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public void DealWithWarn(string keyValue, VMWWarningEntity vMWWarningEntity)
        {
            try
            {
                service.DealWithWarn(keyValue, vMWWarningEntity);
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
