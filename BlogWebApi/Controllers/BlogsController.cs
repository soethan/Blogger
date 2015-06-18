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
    public class BlogsController : ApiController
    {
        private readonly IBlogsRepository _repository;

        public BlogsController(IBlogsRepository repository)
        {
            _repository = repository;
        }
        public HttpResponseMessage Create(BlogModel model)
        {
            var blog = new Blog { BloggerName = model.BloggerName, Title = model.Title };
            _repository.AddBlog(blog);
            _repository.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.Created, blog);
        }
    }
}
