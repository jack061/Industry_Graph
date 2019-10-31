/********************************************************************************
**文 件 名:IOTStopRecordEntity
**命名空间:JFine.Plugins.IOT.Busines.IOT
**描    述:
**
**
**版 本 号:V1.0.0.0
**创 建 人:此代码由T4模板自动生成
**创建日期:2019-08-07 10:22:07
**修 改 人:
**修改日期:
**修改描述:
**
**
**版权所有: ©为之团队
*********************************************************************************/
using System;
using JFine.Domain.Models;
namespace JFine.Plugins.IOT.Domain.Models.IOT
{	
	/// <summary>
	/// IOTStopRecordEntity
	/// </summary>	
	public class IOTStopRecordEntity:BaseEntity<IOTStopRecordEntity>, ICreate, IModify
	{

		/// <summary>
        /// 默认构造函数
        /// </summary>
        public IOTStopRecordEntity()
		{
            this.Id= System.Guid.NewGuid().ToString();

 		}

	#region 实体成员

	  
	  /// <summary>
	  /// 主键
	  /// </summary>	
	  public string Id { get; set; }

	  
	  /// <summary>
	  /// BindId:设备Id
	  /// </summary>	
	  public string BindId { get; set; }

	  
	  /// <summary>
	  /// 停机编码
	  /// </summary>	
	  public string StopReasonCode { get; set; }

	  
	  /// <summary>
	  /// 停机名称
	  /// </summary>	
	  public string StopReasonName { get; set; }

	  
	  /// <summary>
	  /// 停机开始时间
	  /// </summary>	
	  public DateTime? StopStartTime { get; set; }

	  
	  /// <summary>
	  /// 停机结束时间
	  /// </summary>	
	  public DateTime? StopEndTime { get; set; }

	  
	  /// <summary>
	  /// 创建日期
	  /// </summary>	
	  public DateTime? CreateDate { get; set; }

	  
	  /// <summary>
	  /// 创建用户账户
	  /// </summary>	
	  public string CreateUserId { get; set; }

	  
	  /// <summary>
	  /// 创建用户名称
	  /// </summary>	
	  public string CreateUserName { get; set; }

	  
	  /// <summary>
	  /// 最后修改时间
	  /// </summary>	
	  public DateTime? UpdateDate { get; set; }

	  
	  /// <summary>
	  /// 最后修改用户
	  /// </summary>	
	  public string UpdateUserId { get; set; }

	  
	  /// <summary>
	  /// 最后修改用户名称
	  /// </summary>	
	  public string UpdateUserName { get; set; }

 
  #endregion
    }
}

