using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebUoFASM1.Startup))]

namespace WebUoFASM1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}