/********************************************************************************
**文 件 名:ModbusGatewayParaMap
**命名空间:JFine.Plugins.IOT.Busines.Modbus
**描    述:
**
**
**版 本 号:V1.0.0.0
**创 建 人:此代码由T4模板自动生成
**创建日期:2019-06-24 17:57:25
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
	/// ModbusGatewayParaMap
	/// </summary>	
	public class ModbusGatewayParaMap:JFEntityTypeConfiguration<ModbusGatewayParaEntity>
	{
	   public ModbusGatewayParaMap()
	   {
	      this.ToTable("Modbus_Gateway_Parameters");
		  this.HasKey(t=>t.Id);
	   }
    }
}

