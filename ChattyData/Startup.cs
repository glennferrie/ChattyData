using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ChattyData.Startup))]
namespace ChattyData
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
