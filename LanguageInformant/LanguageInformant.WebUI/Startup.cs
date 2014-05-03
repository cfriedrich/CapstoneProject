using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LanguageInformant.WebUI.Startup))]
namespace LanguageInformant.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
