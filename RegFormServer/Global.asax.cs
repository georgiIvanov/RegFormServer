using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RegFormServer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace RegFormServer
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
           JsonSerializerSettings jSettings = GlobalConfiguration.Configuration
            .Formatters
            .JsonFormatter
            .SerializerSettings;

            jSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            jSettings.Converters.Add(new DateConverter());
            JsonMediaTypeFormatter jsonFormatter = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            jsonFormatter.SerializerSettings = jSettings;

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
