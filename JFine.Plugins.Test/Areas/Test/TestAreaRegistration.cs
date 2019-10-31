/********************************************************************************
**文 件 名:TaxInvoiceAreaRegistration
**命名空间:JFine.Plugins.TaxInvoice
**描    述:
**
**版 本 号:V1.0.0.0
**创 建 人:superjoy
**创建日期:2018-06-25 13:24:41
**修 改 人:
**修改日期:
**修改描述:
**版权所有: ©为之团队
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace JFine.Plugins.Areas.Test
{
    public class TestAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Test";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Test_default",
                "Test/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] { "JFine.Plugins.Test.Areas.Test.Controllers" }
            );
        }
    }
}