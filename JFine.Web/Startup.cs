using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JFine.Web.Startup))]
namespace JFine.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
