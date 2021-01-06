using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TC.Core.JwtAuthServer.Models
{
    public class ValidatePinResponseModel
    {
        public bool IsOk { get; set; }
   
        public string Message { get; set; }
    }
}