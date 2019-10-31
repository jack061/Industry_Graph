﻿/********************************************************************************
**文 件 名:IOTDeviceEntity
**命名空间:JFine.Plugins.IOT.Busines.IOT
**描    述:
**
**
**版 本 号:V1.0.0.0
**创 建 人:此代码由T4模板自动生成
**创建日期:2019-07-01 14:26:05
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
    /// IOTDeviceEntity
    /// </summary>	
    public class IOTDeviceEntity : BaseEntity<IOTDeviceEntity>, ICreate, IModify
    {

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public IOTDeviceEntity()
        {
            this.Id = System.Guid.NewGuid().ToString();

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
        /// 品牌
        /// </summary>	
        public string Brand { get; set; }


        /// <summary>
        /// 型号
        /// </summary>	
        public string Model { get; set; }


        /// <summary>
        /// 产地
        /// </summary>	
        public string ProductionPlace { get; set; }
        
        /// <summary>
        /// 机械手类型
        /// </summary>	
        public string ManipulatorType { get; set; }


        /// <summary>
        /// 标准开机人数
        /// </summary>	
        public decimal? PeopleNumber { get; set; }


        /// <summary>
        /// 类别
        /// </summary>
        public string Category { get; set; }
        /// <summary>
        /// 安装位置
        /// </summary>	
        public string Position { get; set; }

        /// <summary>
        /// 经度
        /// </summary>	
        public double? Lng { get; set; }


        /// <summary>
        /// 纬度
        /// </summary>	
        public double? Lat { get; set; }


        /// <summary>
        /// 状态(0:故障；1：正常；2：预警中；3：整改中)
        /// </summary>	
        public int? Status { get; set; }

        /// <summary>
        /// 预警次数
        /// </summary>	
        public int? WarningCount { get; set; }

        /// <summary>
        /// 不准确次数
        /// </summary>	
        public int? ErrorCount { get; set; }



        /// <summary>
        /// 第一次预警时间
        /// </summary>	
        public DateTime? FirstWDT { get; set; }


        /// <summary>
        /// 最后一次预警时间
        /// </summary>	
        public DateTime? LastWDT { get; set; }


        /// <summary>
        /// 密钥
        /// </summary>	
        public String SecretKey { get; set; }


        /// <summary>
        /// 备注
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
