/********************************************************************************
**文 件 名:VMWMapEntity
**命名空间:JFine.Plugins.VMW.Busines.VMW
**描    述:
**
**
**版 本 号:V1.0.0.0
**创 建 人:此代码由T4模板自动生成
**创建日期:2019-02-21 17:17:00
**修 改 人:
**修改日期:
**修改描述:
**
**
**版权所有: ©为之团队
*********************************************************************************/
using System;
using JFine.Domain.Models;
using JFine.Web.Base.MVC.Handler;
namespace JFine.Plugins.VMW.Domain.Models.VMW
{	
	/// <summary>
	/// VMWMapEntity
	/// </summary>	
	public class VMWMapEntity:BaseEntity<VMWMapEntity>, ICreate, IModify
	{

		/// <summary>
        /// 默认构造函数
        /// </summary>
        public VMWMapEntity()
		{
            this.Id= System.Guid.NewGuid().ToString();

 		}

	#region 实体成员

	  
	  /// <summary>
	  /// 主键
	  /// </summary>	
	  public string Id { get; set; }

	  
	  /// <summary>
	  /// 编码
	  /// </summary>	
	  public string Code { get; set; }

	  
	  /// <summary>
	  /// 名称
	  /// </summary>	
	  public string Name { get; set; }

	  
	  /// <summary>
      /// EPSG
	  /// </summary>	
      public string EPSG { get; set; }
        
        /// <summary>
      /// 矿图URL
	  /// </summary>	
      public string ImageURL { get; set; }
        
      /// <summary>
      /// 矿图名称
	  /// </summary>	
      public string ImageName { get; set; }

	  /// <summary>
	  /// 范围左
	  /// </summary>	
      public double? R_Left { get; set; }

	  /// <summary>
	  /// 范围下
	  /// </summary>	
      public double? R_Bottom { get; set; }

	  /// <summary>
	  /// 范围右
	  /// </summary>	
      public double? R_Right { get; set; }

	  /// <summary>
	  /// 范围上
	  /// </summary>	
      public double? R_Top { get; set; }

	  
	  /// <summary>
	  /// 版本
	  /// </summary>	
	  public string Version { get; set; }

	  
	  /// <summary>
	  /// 
	  /// </summary>	
	  public string Remark { get; set; }

	  
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

