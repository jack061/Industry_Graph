/********************************************************************************
**文 件 名:ModbusGatewayParaEntity
**命名空间:JFine.Plugins.IOT.Busines.Modbus
**描    述:
**
**
**版 本 号:V1.0.0.0
**创 建 人:此代码由T4模板自动生成
**创建日期:2019-06-24 17:57:27
**修 改 人:
**修改日期:
**修改描述:
**
**
**版权所有: ©为之团队
*********************************************************************************/
using System;
using JFine.Domain.Models;
namespace JFine.Plugins.IOT.Domain.Models.Modbus
{	
	/// <summary>
	/// ModbusGatewayParaEntity
	/// </summary>	
	public class ModbusGatewayParaEntity:BaseEntity<ModbusGatewayParaEntity>, ICreate, IModify
	{

		/// <summary>
        /// 默认构造函数
        /// </summary>
        public ModbusGatewayParaEntity()
		{
            this.Id= System.Guid.NewGuid().ToString();

 		}

	#region 实体成员

	  
	  /// <summary>
	  /// 主键
	  /// </summary>	
	  public string Id { get; set; }

	  
	  /// <summary>
	  /// BindId
	  /// </summary>	
	  public string BindId { get; set; }

	  
	  /// <summary>
	  /// 设备编码
	  /// </summary>	
      public string DeviceCode { get; set; }

	  
	  /// <summary>
	  /// 寄存器地址
	  /// </summary>	
	  public int? AddrId { get; set; }

	  
	  /// <summary>
	  /// 参数编码
	  /// </summary>	
	  public string ParameterCode { get; set; }

	  
	  /// <summary>
	  /// 参数名称
	  /// </summary>	
	  public string ParameterName { get; set; }

      /// <summary>
      /// 下限
      /// </summary>	
      public decimal? MinValue { get; set; }

      /// <summary>
      /// 上限
      /// </summary>	
      public decimal? MaxValue { get; set; }

      /// <summary>
      /// 是否预警
      /// </summary>	
      public bool? warnFlag { get; set; }

       /// <summary>
	  /// 数据位数
	  /// </summary>	
      public int? Databit { get; set; }
	  
       /// <summary>
	  /// 排序
	  /// </summary>	
      public int? Sort { get; set; }

      /// <summary>
      /// 参数类型：0:开关量|1:模拟量
      /// </summary>	
      public int? ParameterType { get; set; }
      
      /// <summary>
      /// 参数在系统内分类编码
      /// </summary>	
      public int? ParameterCategory { get; set; }

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

