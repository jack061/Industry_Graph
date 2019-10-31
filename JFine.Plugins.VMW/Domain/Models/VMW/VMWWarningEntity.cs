/********************************************************************************
**文 件 名:VMWWarningEntity
**命名空间:JFine.Plugins.VMW.Busines.VMW
**描    述:
**
**
**版 本 号:V1.0.0.0
**创 建 人:此代码由T4模板自动生成
**创建日期:2019-02-21 17:18:42
**修 改 人:
**修改日期:
**修改描述:
**
**
**版权所有: ©为之团队
*********************************************************************************/
using System;
using JFine.Domain.Models;
namespace JFine.Plugins.VMW.Domain.Models.VMW
{	
	/// <summary>
	/// VMWWarningEntity
	/// </summary>	
	public class VMWWarningEntity:BaseEntity<VMWWarningEntity>, ICreate, IModify
	{

		/// <summary>
        /// 默认构造函数
        /// </summary>
        public VMWWarningEntity()
		{
            this.Id= System.Guid.NewGuid().ToString();

 		}

	#region 实体成员

	  
	  /// <summary>
	  /// 主键
	  /// </summary>	
	  public string Id { get; set; }

	  
	  /// <summary>
	  /// 摄像头ID
	  /// </summary>	
	  public string BindId { get; set; }

	  
	  /// <summary>
	  /// 摄像头编码
	  /// </summary>	
	  public string CameraCode { get; set; }

      /// <summary>
      /// 摄像头名称
      /// </summary>	
      public string CameraName { get; set; }

	  
	  /// <summary>
	  /// 预警编码
	  /// </summary>	
	  public string Code { get; set; }

	  
	  /// <summary>
	  /// 预警位置
	  /// </summary>	
	  public string Position { get; set; }

	  
	  /// <summary>
	  /// 现场图片
	  /// </summary>	
	  public string ImageURL { get; set; }

      /// <summary>
      /// 现场图片
      /// </summary>	
      public string ImageName { get; set; }

      /// <summary>
      /// 预警视频
      /// </summary>	
      public string VideoURL { get; set; }

      /// <summary>
      /// 预警视频
      /// </summary>	
      public string VideoName { get; set; }

	  
	  /// <summary>
	  /// 预警内容描述
	  /// </summary>	
	  public string DES { get; set; }

	  
	  /// <summary>
	  /// 预警类别编码
	  /// </summary>	
	  public string CategoryCode { get; set; }

	  
	  /// <summary>
	  /// 预警类别名称
	  /// </summary>	
	  public string CategoryName { get; set; }

      /// <summary>
      /// 等级编码
      /// </summary>	
      public string LevelCode { get; set; }


      /// <summary>
      /// 等级名称
      /// </summary>	
      public string LevelName { get; set; }

	  
	  /// <summary>
	  /// 确认状态：未确认|已确认
	  /// </summary>	
	  public string ConfirmStatus { get; set; }

	  
	  /// <summary>
	  /// 确认人
	  /// </summary>	
	  public string ConfirmManCode { get; set; }

	  
	  /// <summary>
	  /// 确认人
	  /// </summary>	
	  public string ConfirmManName { get; set; }

	  
	  /// <summary>
	  /// 确认时间
	  /// </summary>	
	  public DateTime? ConfirmDate { get; set; }

	  
	  /// <summary>
	  /// 处理状态：未处理|已处理
	  /// </summary>	
	  public string DealStatus { get; set; }

	  
	  /// <summary>
	  /// 处理人
	  /// </summary>	
	  public string DealManCode { get; set; }

	  
	  /// <summary>
	  /// 处理人
	  /// </summary>	
	  public string DealManName { get; set; }

	  
	  /// <summary>
	  /// 处理时间
	  /// </summary>	
	  public DateTime? DealDate { get; set; }

	  
	  /// <summary>
	  /// 处理结果
	  /// </summary>	
	  public string Result { get; set; }

	  
	  /// <summary>
	  /// 年
	  /// </summary>	
	  public int? Year { get; set; }

	  
	  /// <summary>
	  /// 月
	  /// </summary>	
	  public int? Month { get; set; }

	  
	  /// <summary>
	  /// 日
	  /// </summary>	
	  public int? DAY { get; set; }

      /// <summary>
      /// 预警时间
      /// </summary>	
      public DateTime? WarningDate { get; set; }

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

