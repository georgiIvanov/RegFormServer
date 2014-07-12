using MongoDB.Bson;
using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Linq;
using RegFormServer.App_Start;
using RegFormServer.Models;
using RegFormServer.Models.ViewModels;
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
            response = this.ValidateUserRegistration(user);
            if(response != null)
            {
                return response;
            }

            if(this.FindUser(user) != null)
            {
                return this.ErrorWithDescription("User already exists");
            }

            Context.Users.Insert(user);
            
            return this.Request.CreateResponse(HttpStatusCode.OK, user);
        }

        [HttpPost]
        public HttpResponseMessage SignIn(UserLogin user)
        {
            if (string.IsNullOrEmpty(user.Email) ||
                string.IsNullOrEmpty(user.Password))
            {
                return this.ErrorWithDescription("Invalid Credentials");
            }

            User foundUser = this.FindUser(user.Email);

            if(foundUser == null)
            {
                return this.ErrorWithDescription("Invalid credentials");
            }

            if(foundUser.Password != user.Password)
            {
                return this.ErrorWithDescription("Invalid credentials");
            }

            return this.Request.CreateResponse(HttpStatusCode.OK, foundUser);
        }
    }
}