﻿/********************************************************************************
**文 件 名:IOTDeviceParaMap
**命名空间:JFine.Plugins.IOT.Busines.IOT
**描    述:
**
**
**版 本 号:V1.0.0.0
**创 建 人:此代码由T4模板自动生成
**创建日期:2019-07-24 18:43:54
**修 改 人:
**修改日期:
**修改描述:
**
**
**版权所有: ©为之团队
*********************************************************************************/

using JFine.Plugins.IOT.Domain.Models.IOT;
using JFine.Data.Common;
using System.Data.Entity.ModelConfiguration;
namespace JFine.Plugins.IOT.Mapping.IOT
{	
	/// <summary>
	/// IOTDeviceParaMap
	/// </summary>	
	public class IOTDeviceParaMap:JFEntityTypeConfiguration<IOTDeviceParaEntity>
	{
	   public IOTDeviceParaMap()
	   {
	      this.ToTable("IOT_DeviceParameters");
		  this.HasKey(t=>t.Id);
	   }
    }
}
