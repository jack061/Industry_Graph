/********************************************************************************
**文 件 名:ModbusGatewayValRepository
**命名空间:JFine.Plugins.IOT.Busines.Modbus
**描    述:
**
**
**版 本 号:V1.0.0.0
**创 建 人:此代码由T4模板自动生成
**创建日期:2019-06-24 17:58:31
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
using System.Data;

namespace JFine.Plugins.IOT.Domain.Repository.IOT
{
    /// <summary>
    /// ModbusGatewayValRepository
    /// </summary>	
    public class GatewayValRepository : RepositoryFactory<GatewayValEntity>, IGatewayValRepository
    {
        #region 数据获取
        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<GatewayValEntity> GetList()
        {
            return this.BaseRepository().IQueryable().OrderByDescending(t => t.CreateDate).ToList();

        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<GatewayValEntity> GetListBySql(string sqlWhere)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT * 
                            FROM   Modbus_Gateway_Values
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
        public IEnumerable<GatewayValEntity> GetPageListBySql(Pagination pagination, string sqlWhere, List<DbParameter> parameter)
        {

            var strSql = new StringBuilder();
            strSql.Append(@"SELECT * 
                            FROM   Modbus_Gateway_Values
                            WHERE  1=1 ");
            strSql.Append(sqlWhere);

            return this.BaseRepository().FindList(strSql.ToString(), parameter, pagination);

        }
        public DataTable GetNewestData(Pagination pagination, string sqlWhere, List<DbParameter> parameter)
        {
            var strSql = new StringBuilder();
            strSql.Append(@" select t.DeviceId,t.ParameterCode,t.ParameterValue from(select row_number()over(partition by DeviceId,ParameterCode order by CreateDate desc) as num, * from dbo.Gateway_Values )t where t.num=1 ");
            strSql.Append(sqlWhere);
            return new RepositoryFactory().BaseRepository().FindTable(strSql.ToString());
        }
        public DataTable GetHistoryData(string sqlWhere)
        {
            var strSql = new StringBuilder();
            strSql.Append(@" select top 1000 DeviceId,ParameterCode,ParameterValue,  cast(datediff(SS,'1970-1-1 00:00:00',CreateDate) as bigint)*1000 as timeSpan from dbo.Gateway_Values where 1=1  ");
            strSql.Append(sqlWhere);
            return new RepositoryFactory().BaseRepository().FindTable(strSql.ToString());
        }
        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<GatewayValEntity> GetList(Expression<Func<GatewayValEntity, bool>> condition)
        {
            return this.BaseRepository().IQueryable(condition).ToList();

        }


        /// <summary>
        /// 列表--分页
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<GatewayValEntity> GetPageList(Pagination pagination, Expression<Func<GatewayValEntity, bool>> condition)
        {
            return this.BaseRepository().FindList(condition, pagination);
        }
        public IEnumerable<GatewayValEntity> GetDeviceStartTime(StringBuilder sqlWhere)
        {
            //return this.BaseRepository().FindList(condition, pagination);
            StringBuilder sqldata = new StringBuilder(@" select * from (select * from dbo.Gateway_Values where ParameterValue=1 and ParameterName is not null) t1 where not exists(select 1 from dbo.Gateway_Values where DeviceCode=t1.DeviceCode and Year=t1.Year and Month=t1.Month and day=t1.DAY and CreateDate<t1.CreateDate and t1.ParameterValue=ParameterValue and t1.ParameterName=ParameterName) and t1.ParameterName='开模'");
            return this.BaseRepository().FindList(sqldata.ToString());
        }
        /// <summary>
        /// 实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public GatewayValEntity GetForm(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region 数据处理

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="modbusGatewayValEntity">实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, GatewayValEntity modbusGatewayValEntity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                modbusGatewayValEntity.Modify(keyValue);
                this.BaseRepository().Update(modbusGatewayValEntity);
            }
            else
            {
                modbusGatewayValEntity.Create();
                this.BaseRepository().Insert(modbusGatewayValEntity);
            }
        }

        public void SaveList(List<GatewayValEntity> valList)
        {
            this.BaseRepository().Insert(valList);
        }

        public void SaveList(StringBuilder sql)
        {
            this.BaseRepository().ExecuteBySql(sql.ToString());
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void DeleteForm(string keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }


        public DataTable LoadMultiColumn(string bindId)
        {
            var strSql = new StringBuilder();
            strSql.Append(@" select t2.Process,(
stuff((select  '|'+AccessoryName+'' from TN_FJ t1 where t1.Process=t2.Process and t1.BindId=t2.BindId for xml path ('')),1,1,''  ))as attachName from TN_FJ t2 where t2.BindId=" + bindId + " group by Process,t2.BindId ");
            DataTable dt = new RepositoryFactory().BaseRepository().FindTable(strSql.ToString());
            DataTable dt_new = new DataTable();
            dt_new.Columns.Add("Process");
            int count = dt.Rows[0]["Count"].ToInt();
            for (int i = 1; i <= count; i++)
            {
                dt_new.Columns.Add("附件" + i);
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = dt_new.NewRow();
                row["Process"] = dt.Rows[i]["Process"];
                var attachName = dt.Rows[i][1].ToString();
                var attachArray = attachName.Split('|');
                for (int j = 0; j < attachArray.Length; j++)
                {
                    row["附件" + j + 1] = attachArray[j];
                }
                dt_new.Rows.Add(row);
            }
            return dt_new;

        }
        #endregion
    }
}


