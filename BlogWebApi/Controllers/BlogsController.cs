using BlogApi.DataAccessLayer;
using BlogApi.DomainClasses;
using BlogWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BlogWebApi.Controllers
{
    public class BlogsController : ApiController
    {
        public BlogsController()
        {

        }
        public HttpResponseMessage Create(BlogModel model)
        {
            var blog = new Blog { BloggerName = model.BloggerName, Title = model.Title };
            var dbContext = new Context();
            dbContext.Blogs.Add(blog);
            dbContext.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.Created, blog);
        }
    }
}
