﻿/********************************************************************************
**文 件 名:IOT_Order_SecEntity
**命名空间:JFine.Plugins.IOT.Busines.IOT
**描    述:
**
**
**版 本 号:V1.0.0.0
**创 建 人:此代码由T4模板自动生成
**创建日期:2019-09-30 14:27:43
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
    /// IOT_Order_SecEntity
    /// </summary>	
    public class IOT_Order_SecEntity : BaseEntity<IOT_Order_SecEntity>, ICreate, IModify
    {

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public IOT_Order_SecEntity()
        {
            this.Id = System.Guid.NewGuid().ToString();

        }

        #region 实体成员


        /// <summary>
        /// 
        /// </summary>	
        public string Id { get; set; }


        /// <summary>
        /// 
        /// </summary>	
        public string ProductLine { get; set; }


        /// <summary>
        /// 
        /// </summary>	
        public string OrderName { get; set; }


        /// <summary>
        /// 
        /// </summary>	
        public string Status { get; set; }

        public string StartStatus { get; set; }

        public string ProceedStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>	
        public string PCode { get; set; }


        /// <summary>
        /// 
        /// </summary>	
        public string PName { get; set; }


        /// <summary>
        /// 
        /// </summary>	
        public int? PlanQuantity { get; set; }


        /// <summary>
        /// 
        /// </summary>	
        public int? QualifyQuantity { get; set; }


        /// <summary>
        /// 
        /// </summary>	
        public int? UnqualifyQuantity { get; set; }


        /// <summary>
        /// 
        /// </summary>	
        public string OrderType { get; set; }

        public DateTime? OrderDate { get; set; }

        public DateTime? OrderBegin_Time { get; set; }

        public DateTime? OrderEnd_Time { get; set; }
        /// <summary>
        /// 
        /// </summary>	
        public string Remark { get; set; }


        /// <summary>
        /// 
        /// </summary>	
        public DateTime? CreateDate { get; set; }


        /// <summary>
        /// 
        /// </summary>	
        public string CreateUserId { get; set; }


        /// <summary>
        /// 
        /// </summary>	
        public string CreateUserName { get; set; }


        /// <summary>
        /// 
        /// </summary>	
        public DateTime? UpdateDate { get; set; }


        /// <summary>
        /// 
        /// </summary>	
        public string UpdateUserId { get; set; }


        /// <summary>
        /// 
        /// </summary>	
        public string UpdateUserName { get; set; }


        #endregion
    }
}

