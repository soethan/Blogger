using BlogApi.DataAccessLayer;
using BlogApi.DataAccessLayer.Repositories;
using BlogApi.DomainClasses;
using BlogWebApi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity.Infrastructure;

namespace BlogWebApi.Controllers
{
    public class BlogsController : ApiController
    {
        const int PAGE_SIZE = 10;
        private readonly IBlogsRepository _repository;

        public BlogsController(IBlogsRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Blog> Get(int page = 0)
        {
            return _repository.GetAllBlogs()
                    .OrderBy(b => b.Title)
                    .Skip(PAGE_SIZE * page)
                    .Take(PAGE_SIZE).ToList();
        }

        public HttpResponseMessage Create(BlogModel model)
        {
            var blog = new Blog { BloggerName = model.BloggerName, Title = model.Title };
            _repository.AddBlog(blog);
            _repository.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.Created, blog);
        }

        public HttpResponseMessage Put(int id, BlogModel model)
        {
            if (_repository.GetBlog(id) == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Blog does not exist.");
            }
            _repository.UpdateBlog(id, model.Title);
            _repository.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage Delete(int id)
        {
            if (_repository.GetBlog(id) == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Blog does not exist.");
            }
            _repository.DeleteBlog(id);
            _repository.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
