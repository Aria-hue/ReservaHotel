using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ReservaHotel.Startup))]
namespace ReservaHotel
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
