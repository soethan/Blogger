using BlogApi.DataAccessLayer;
using BlogApi.DataAccessLayer.Repositories;
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
        private readonly IBlogsRepository _repository;

        public PostsController(IBlogsRepository repository)
        {
            _repository = repository;
        }
        public HttpResponseMessage Create(int blogId, PostModel model)
        {
            var post = new Post { Title = model.Title, Content = model.Content };

            var blog = _repository.GetBlog(blogId);
            if (blog == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Blog does not exist.");
            }
            _repository.AddPost(blogId, post);
            _repository.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.Created, post.Id);
        }
    }
}
