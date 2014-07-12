using MongoDB.Driver;
using Newtonsoft.Json;
using RegFormServer.App_Start;
using RegFormServer.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace RegFormServer.Controllers
{
    public class HomeController : Controller
    {
        UsersContext Context = new UsersContext();

        public HomeController()
        {
            

        }
        public ActionResult Index()
        {
            Context.Database.GetStats();

            StringBuilder stringResult = new StringBuilder();
            stringResult.Append(JsonConvert.SerializeObject(Context.Database.Server.BuildInfo, Formatting.Indented));

            return View();
        }
    }
}
