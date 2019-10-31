/********************************************************************************
**文 件 名:VMWFaceSampleMap
**命名空间:JFine.Plugins.VMW.Busines.VMW
**描    述:
**
**
**版 本 号:V1.0.0.0
**创 建 人:此代码由T4模板自动生成
**创建日期:2019-04-01 16:03:54
**修 改 人:
**修改日期:
**修改描述:
**
**
**版权所有: ©为之团队
*********************************************************************************/

using JFine.Plugins.VMW.Domain.Models.VMW;
using JFine.Data.Common;
using System.Data.Entity.ModelConfiguration;
namespace JFine.Plugins.VMW.Mapping.VMW
{	
	/// <summary>
	/// VMWFaceSampleMap
	/// </summary>	
	public class VMWFaceSampleMap:JFEntityTypeConfiguration<VMWFaceSampleEntity>
	{
	   public VMWFaceSampleMap()
	   {
	      this.ToTable("VMW_FaceSample");
		  this.HasKey(t=>t.Id);
	   }
    }
}

