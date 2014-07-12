using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using RegFormServer.Models;
using System.Net;
using MongoDB.Bson;
using MongoDB.Driver.GridFS;

namespace RegFormServer.Controllers
{
    public class UserSettings : BaseController
    {
        //[HttpPost]
        //public HttpResponseMessage UploadAvatar(string email, HttpPostedFileBase file)
        //{
        //    User foundUser = FindUser(email);

        //    if (foundUser == null)
        //    {
        //        return this.Request.CreateResponse(HttpStatusCode.BadRequest,
        //            new { error = new ErrorResponse("No such user").ErrorDescriptions });
        //    }

        //    var avatarId = ObjectId.GenerateNewId();
        //    foundUser.AvatarId = avatarId.ToString();

        //    Context.Users.Save(foundUser);

        //    var options = new MongoGridFSCreateOptions
        //    {
        //        Id = avatarId,
        //        ContentType = file.ContentType
        //    };

        //    Context.Database.GridFS.Upload(file.InputStream, file.FileName, options);

        //    return this.Request.CreateResponse(HttpStatusCode.OK, foundUser);
        //}
    }
}