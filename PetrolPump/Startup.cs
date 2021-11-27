using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PetrolPump.Startup))]
namespace PetrolPump
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
