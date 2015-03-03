using Microsoft.Owin.Cors;
using Owin;

namespace Auctions.Import.ConsoleApp
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR();
        }
    }
}