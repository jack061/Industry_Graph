/********************************************************************************
**文 件 名:ModbusGatewayMap
**命名空间:JFine.Plugins.IOT.Busines.Modbus
**描    述:
**
**
**版 本 号:V1.0.0.0
**创 建 人:此代码由T4模板自动生成
**创建日期:2019-06-24 17:55:31
**修 改 人:
**修改日期:
**修改描述:
**
**
**版权所有: ©为之团队
*********************************************************************************/

using JFine.Plugins.IOT.Domain.Models.Modbus;
using JFine.Data.Common;
using System.Data.Entity.ModelConfiguration;
namespace JFine.Plugins.IOT.Mapping.Modbus
{	
	/// <summary>
	/// ModbusGatewayMap
	/// </summary>	
	public class ModbusGatewayMap:JFEntityTypeConfiguration<ModbusGatewayEntity>
	{
	   public ModbusGatewayMap()
	   {
	      this.ToTable("Modbus_Gateway");
		  this.HasKey(t=>t.Id);
	   }
    }
}

