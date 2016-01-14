using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(highChartsTesting.Startup))]
namespace highChartsTesting
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
