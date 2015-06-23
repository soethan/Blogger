using BlogWebApi.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BlogWebApi.Controllers
{
    [BlogAuthorizationFilter]
    public class RestrictedApiController : ApiController
    {
    }
}
