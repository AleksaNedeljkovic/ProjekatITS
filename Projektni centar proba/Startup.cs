using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Projektni_centar_proba.Startup))]
namespace Projektni_centar_proba
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
