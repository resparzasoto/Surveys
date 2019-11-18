using System.Web.Http;
using Owin;
using Surveys.Web.App_Start;

namespace Surveys.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            var config = new HttpConfiguration();
            OAuthConfig.ConfigureOAuth(app, config);

            app.UseWebApi(config);

            WebApiConfig.Register(config);
        }
    }
}
