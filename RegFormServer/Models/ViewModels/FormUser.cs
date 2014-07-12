using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RegFormServer.Models
{
    public class FormUser
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public UserGender Gender { get; set; }
        public DateTime Birthday { get; set; }
        public bool BirthDatePublic { get; set; }
        public string Fullname { get; set; }
        public string AvatarUrl { get; set; }
    }
}