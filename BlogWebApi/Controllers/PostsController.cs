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
    public class PostsController : ApiController
    {
        public PostsController()
        {

        }
        public HttpResponseMessage Create(int blogId, PostModel model)
        {
            
            var post = new Post { Title = model.Title, Content = model.Content };
            var dbContext = new Context();

            var blog = dbContext.Blogs.Find(blogId);
            if (blog == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Blog does not exist.");
            }
            blog.Posts.Add(post);
            dbContext.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.Created, post.Id);
        }
    }
}
