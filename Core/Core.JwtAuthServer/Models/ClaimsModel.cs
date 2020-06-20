using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TC.Core.JwtAuthServer.Models
{
    public class ClaimsModel
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public bool CompanyState { get; set; }
        public bool ControllerScheme { get; set; }
        public string UserType { get; set; }
        public string UserName { get; set; }
        public bool IsSignatureSismac { get; set; }
        public string  UserId { get; set; }
    }
}