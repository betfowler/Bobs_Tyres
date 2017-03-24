using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Bobs_Tyres.Startup))]
namespace Bobs_Tyres
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
