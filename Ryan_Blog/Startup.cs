using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Ryan_Blog.Startup))]
namespace Ryan_Blog
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
