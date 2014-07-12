using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using RegFormServer.Models;
using System.Net;
using MongoDB.Bson;
using MongoDB.Driver.GridFS;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Web.Hosting;
using System.Drawing.Imaging;
using System.Net.Http.Headers;

namespace RegFormServer.Controllers
{
    public class UserSettingsController : BaseController
    {
        [HttpGet]
        public HttpResponseMessage AvatarForId(string id)
        {
            var image = Context.Database.GridFS.FindOneById(new ObjectId(id));

            if(image == null)
            {
                return this.Request.CreateResponse(HttpStatusCode.BadRequest,
                        new { error = new ErrorResponse("No such file").ErrorDescriptions });
            }

            Image img = Image.FromStream(image.OpenRead());

            MemoryStream ms = new MemoryStream();
            img.Save(ms, ImageFormat.Png);

            response = new HttpResponseMessage();
            response.Content = new ByteArrayContent(ms.ToArray());
            ms.Close();
            ms.Dispose();

            response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
            response.StatusCode = HttpStatusCode.OK;

            return response;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> UploadAvatar(string email)
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(root);
            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);

                var file = provider.FileData.LastOrDefault();
                FileInfo fileInfo = new FileInfo(file.LocalFileName);
                FileStream fileStream = fileInfo.OpenRead();
                
                User foundUser = FindUser(email);

                if (foundUser == null)
                {
                    return this.Request.CreateResponse(HttpStatusCode.BadRequest,
                        new { error = new ErrorResponse("No such user").ErrorDescriptions });
                }

                var avatarId = ObjectId.GenerateNewId();
                string avatarUrl = "http://" + this.Request.RequestUri.Authority +
                    "/api/usersettings/avatarforid?id=" + avatarId.ToString();
                foundUser.AvatarUrl = avatarUrl;

                Context.Users.Save(foundUser);

                var options = new MongoGridFSCreateOptions
                {
                    Id = avatarId,
                    ContentType = file.Headers.ContentType.ToString()
                };

                Context.Database.GridFS.Upload(fileStream, file.LocalFileName, options);
                
                fileStream.Dispose();

                var t = Task.Run(() =>
                    CleanUpAppDataFolder(provider.FileData)
                );

               

                return this.Request.CreateResponse(HttpStatusCode.OK, foundUser);
            }
            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }

            
        }

        void CleanUpAppDataFolder(IEnumerable<MultipartFileData> filesToDelete)
        {
            foreach (var file in filesToDelete)
            {
                File.Delete(file.LocalFileName);
            }
            
        }
    }
}