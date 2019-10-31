
/********************************************************************************
**文 件 名:VMWFaceSampleBLL
**命名空间:JFine.Plugins.VMW.Busines.VMW
**描    述:
**
**
**版 本 号:V1.0.0.0
**创 建 人:此代码由T4模板自动生成
**创建日期:2019-04-01 16:03:49
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

namespace JFine.Plugins.VMW.Busines.VMW
{	
	/// <summary>
	/// VMWFaceSampleBLL
	/// </summary>	
	public class VMWFaceSampleBLL
	{
	    private IVMWFaceSampleRepository service=new VMWFaceSampleRepository();


        #region 数据获取

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<VMWFaceSampleEntity> GetList()
        {
            return service.GetList();
        }

		/// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<VMWFaceSampleEntity> GetListBySql( string sqlWhere)
        {
			return service.GetListBySql(sqlWhere);
        }

		/// <summary>
        /// 列表--分页
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<VMWFaceSampleEntity> GetPageListBySql(Pagination pagination, string queryJson)
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
        public IEnumerable<VMWFaceSampleEntity> GetList( string queryJson)
        {
             var expression = LinqExtensions.True<VMWFaceSampleEntity>();
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
        public IEnumerable<VMWFaceSampleEntity> GetPageList(Pagination pagination, string queryJson)
        {
			var expression = LinqExtensions.True<VMWFaceSampleEntity>();
            var queryParam = queryJson.ToJObject();
             //查询条件
            if (!queryParam["keyword"].IsEmpty())
            {
                string keyord = queryParam["keyword"].ToString();
                expression = expression.And(t => t.Name.Contains(keyord) || t.Code.Contains(keyord));
            }
            if (!queryParam["deptCode"].IsEmpty())
            {
                string deptCode = queryParam["deptCode"].ToString();
                expression = expression.And(t => t.DepCode.StartsWith(deptCode));
            }
            return service.GetPageList(pagination, expression);
        }

        /// <summary>
        /// 实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public VMWFaceSampleEntity GetForm(string keyValue)
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
        public void SaveForm(string keyValue, VMWFaceSampleEntity vMWFaceSampleEntity)
        {
            try
            {
                if (string.IsNullOrEmpty(keyValue)) 
                {
                    if (IsExist(vMWFaceSampleEntity.Code))
                    {
                        throw new Exception("该编码已经存在,请修改！");
                    }
                }
                service.SaveForm(keyValue, vMWFaceSampleEntity);
                
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 更新列表
        /// </summary>
        /// <param name=vMWFaceSampleList">列表</param>
        /// <returns></returns>
        public void UpdateList(List<VMWFaceSampleEntity> vMWFaceSampleList) 
        {
            service.UpdateList(vMWFaceSampleList);
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

        /// <summary>
        /// 判断编码是否存在
        /// </summary>
        /// <param name="code">编码</param>
        public bool IsExist(string code)
        {
            bool result = false;
            var list = GetListBySql(" and Code = '" + code + "'");
            if (list.Count() > 0)
            {
                result = true;
            }

            return result;
 
        }

        #endregion

    }
}
