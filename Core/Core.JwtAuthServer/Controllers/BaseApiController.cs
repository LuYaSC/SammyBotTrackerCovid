﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace TC.Core.JwtAuthServer.Controllers
{
    [Authorize]
    public class BaseApiController : ApiController
    {
        
    }
}