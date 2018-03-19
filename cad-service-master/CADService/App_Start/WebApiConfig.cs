using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using System.Net.Http.Headers;
//using System.Web.Http.Cors;

namespace CADService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
			var cors = new System.Web.Http.Cors.EnableCorsAttribute("*", "*", "*");
			config.EnableCors(cors);
			// Web API configuration and services
			// Configure Web API to use only bearer token authentication.
			config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            //EnableCorsAttribute cors = new EnableCorsAttribute("http://localhost:4200", "*", "GET,POST");
            //config.EnableCors(cors);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling
                = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            config.Formatters.XmlFormatter.UseXmlSerializer = true;

        }
    }
}
