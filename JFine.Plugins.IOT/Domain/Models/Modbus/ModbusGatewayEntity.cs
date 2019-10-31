/********************************************************************************
**文 件 名:ModbusGatewayEntity
**命名空间:JFine.Plugins.IOT.Busines.Modbus
**描    述:
**
**
**版 本 号:V1.0.0.0
**创 建 人:此代码由T4模板自动生成
**创建日期:2019-06-24 17:55:28
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
	/// ModbusGatewayEntity
	/// </summary>	
	public class ModbusGatewayEntity:BaseEntity<ModbusGatewayEntity>, ICreate, IModify
	{

		/// <summary>
        /// 默认构造函数
        /// </summary>
        public ModbusGatewayEntity()
		{
            this.Id= System.Guid.NewGuid().ToString();

 		}

	#region 实体成员

	  
	  /// <summary>
	  /// 主键
	  /// </summary>	
	  public string Id { get; set; }

	  
	  /// <summary>
	  /// IP
	  /// </summary>	
	  public string Ip { get; set; }

	  
	  /// <summary>
	  /// 端口号
	  /// </summary>	
	  public int? Port { get; set; }
        
        /// <summary>
	  /// 功能码
	  /// </summary>	
      public int? FunCode { get; set; }

	  
	  /// <summary>
	  /// 超时时间s
	  /// </summary>	
	  public int? Timeout { get; set; }

	  
	  /// <summary>
	  /// 起始寄存器地址
	  /// </summary>	
	  public int? StartAddr { get; set; }

	  
	  /// <summary>
	  /// 寄存器地址长度
	  /// </summary>	
	  public int? AddrLength { get; set; }

	  
	  /// <summary>
	  /// 周期s
	  /// </summary>	
	  public decimal? Period { get; set; }

      /// <summary>
      /// 状态-1:启动失败；0:未启动；1：已启动2：已挂起
      /// </summary>	
      public int? Status { get; set; }

	  
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

