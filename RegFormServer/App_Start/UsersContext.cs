using MongoDB.Driver;
using RegFormServer.Models;
using RegFormServer.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RegFormServer.App_Start
{
    public class UsersContext
    {
        public MongoDatabase Database;

        public UsersContext()
        {
            var client = new MongoClient(Settings.Default.RegFormConnectionString);
            var server = client.GetServer();
            this.Database = server.GetDatabase(Settings.Default.RegFormDatabase);
        }

        public MongoCollection<FormUser> Users
        {
            get
            {
                return Database.GetCollection<FormUser>("users");
            }
        }
    }
}