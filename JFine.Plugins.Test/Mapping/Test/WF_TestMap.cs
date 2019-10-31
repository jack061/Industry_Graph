/********************************************************************************
**文 件 名:WF_TestMap
**命名空间:JFine.Busines.Test
**描    述:
**
**
**版 本 号:V1.0.0.0
**创 建 人:此代码由T4模板自动生成
**创建日期:2018-09-11 18:49:16
**修 改 人:
**修改日期:
**修改描述:
**
**
**版权所有: ©为之团队
*********************************************************************************/

using JFine.Domain.Models.Test;
using JFine.Data.Common;
using System.Data.Entity.ModelConfiguration;
namespace JFine.Mapping.Test
{	
	/// <summary>
	/// WF_TestMap
	/// </summary>	
	public class WF_TestMap:JFEntityTypeConfiguration<WF_TestEntity>
	{
	   public WF_TestMap()
	   {
	      this.ToTable("WF_Test");
		  this.HasKey(t=>t.Id);
	   }
    }
}

