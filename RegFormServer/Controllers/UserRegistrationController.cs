using MongoDB.Bson;
using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Linq;
using RegFormServer.App_Start;
using RegFormServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace RegFormServer.Controllers
{
    public class UserRegistrationController : BaseController
    {
        

        public HttpResponseMessage SignUp(FormUser user)
        {
            response = base.ValidateUserRegistration(user);
            if(response != null)
            {
                return response;
            }

            if(this.FindUser(user) != null)
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest,
                    new { error = new ErrorResponse("User already exists").ErrorDescriptions });
            }

            Context.Users.Insert(user);
            
            return this.Request.CreateResponse(HttpStatusCode.OK, user);
        }        
    }
}