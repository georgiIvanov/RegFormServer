using RegFormServer.App_Start;
using RegFormServer.Models;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Net;

namespace RegFormServer.Controllers
{
    public class BaseController : ApiController
    {
        protected UsersContext Context = new UsersContext();
        protected HttpResponseMessage response;

        protected User FindUser(FormUser user)
        {
            var foundUser = Context.Users.AsQueryable().FirstOrDefault(x => x.Email == user.Email);
            return foundUser;
        }

        protected User FindUser(string email)
        {
            var foundUser = Context.Users.AsQueryable().FirstOrDefault(x => x.Email == email);
            return foundUser;
        }

        protected HttpResponseMessage ValidateUserRegistration(FormUser user)
        {
            ErrorResponse error = new ErrorResponse();
            // TODO: validate with regex
            if (string.IsNullOrEmpty(user.Email))
            {
                error.ErrorDescriptions.Add("Invalid email");
            }

            if (string.IsNullOrEmpty(user.Password))
            {
                error.ErrorDescriptions.Add("Invalid password");
            }

            if (user.Gender == 0 || user.Gender > UserGender.Private)
            {
                error.ErrorDescriptions.Add("Invalid gender");
            }

            DateTime start = new DateTime(1900, 1, 1, 0, 0, 0);
            DateTime end = DateTime.Now;
            if (user.Birthday < start || user.Birthday > end)
            {
                error.ErrorDescriptions.Add("Invalid birthday");
            }

            if (string.IsNullOrEmpty(user.Fullname))
            {
                error.ErrorDescriptions.Add("Invalid name");
            }

            if (error.ErrorDescriptions.Count > 0)
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