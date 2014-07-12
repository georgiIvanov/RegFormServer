using MongoDB.Bson;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using RegFormServer.App_Start;
using RegFormServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RegFormServer.Controllers
{
    public class UserRegistrationController : ApiController
    {
        UsersContext Context = new UsersContext();

        public HttpResponseMessage SignUp(FormUser user)
        {
            HttpResponseMessage response;

            response = ValidateUserRegistration(user);
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

        FormUser FindUser(FormUser user)
        {
            var foundUser = Context.Users.AsQueryable().FirstOrDefault(x => x.Email == user.Email);
            return foundUser;
        }

        HttpResponseMessage ValidateUserRegistration(FormUser user)
        {
            ErrorResponse error = new ErrorResponse();
            // TODO: validate with regex
            if(string.IsNullOrEmpty(user.Email))
            {
                error.ErrorDescriptions.Add("Invalid email");
            }

            if (string.IsNullOrEmpty(user.Password))
            {
                error.ErrorDescriptions.Add("Invalid password");
            }

            if(user.Gender == 0 || user.Gender > UserGender.Private)
            {
                error.ErrorDescriptions.Add("Invalid gender");
            }

            DateTime start = new DateTime(1900, 1, 1, 0, 0, 0);
            DateTime end = DateTime.Now;
            if(user.Birthday < start || user.Birthday > end)
            {
                error.ErrorDescriptions.Add("Invalid birthday");
            }

            if (string.IsNullOrEmpty(user.Fullname))
            {
                error.ErrorDescriptions.Add("Invalid name");
            }

            if(error.ErrorDescriptions.Count > 0)
            {
                HttpResponseMessage errorResponse = this.Request.CreateResponse(HttpStatusCode.BadRequest, new { error = error.ErrorDescriptions });
                return errorResponse;
            }
            else
            {
                return null;
            }
        }
    }
}