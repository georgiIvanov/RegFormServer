using MongoDB.Driver;
using RegFormServer.Models;
using RegFormServer.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace RegFormServer.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public HttpResponseMessage Get()
        {
            HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.OK);

            FormUser user = new FormUser();
            user.Email = "lala@gmail.com";
            user.Password = "fu we";

            message = this.Request.CreateResponse(HttpStatusCode.OK, user);
            //return new string[] { "value1", "value2" };
            return message;
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
