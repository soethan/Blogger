using BlogWebApi.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace BlogWebApi.Filters
{
    /// <summary>
    /// http://www.md5.net/md5-generator/
    /// userName:password:secretKey
    /// </summary>
    public class BlogAuthorizationFilterAttribute: AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var authHeader = actionContext.Request.Headers.Authorization;

            if (authHeader != null)
            {
                if (authHeader.Scheme.Equals("basic", StringComparison.CurrentCultureIgnoreCase) &&
                    !string.IsNullOrEmpty(authHeader.Parameter))
                {
                    var credentials = authHeader.Parameter;
                                        
                    var split = credentials.Split(':');
                    var userName = split[0];
                    var password = split[1];
                    var hash = split[2];

                    var hashValue = HashHelper.Md532Bit(string.Format("{0}{1}{2}", userName, password, "6FB692BBF371"), null, false);

                    if (hash == hashValue)//&& IsValidUser(userName, password)
                    {
                        return;
                    }
                }
            }
            HandleUnauthorized(actionContext);
        }

        private void HandleUnauthorized(HttpActionContext actionContext)
        {
            actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            actionContext.Response.Headers.Add("WWW-Authenticate", "Basic Scheme='blog' location='http://localhost/account/login'");
        }
    }
}