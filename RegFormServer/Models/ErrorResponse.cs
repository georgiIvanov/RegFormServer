using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RegFormServer.Models
{
    public class ErrorResponse
    {
        List<string> errors;
        public ErrorResponse()
        {
            this.errors = new List<string>();
        }
        public ErrorResponse(string errorDescription)
            :this()
        {
            
            this.ErrorDescriptions.Add(errorDescription);
        }

        public List<string> ErrorDescriptions
        { 
            get
            {
                return this.errors;
            }
        }
    }
}