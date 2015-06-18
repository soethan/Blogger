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
            try
            {
                var blog = new Blog { BloggerName = model.BloggerName, Title = model.Title };
                _repository.AddBlog(blog);
                _repository.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.Created, blog);
            }
            catch (DbEntityValidationException dbEx)
            {
                var errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add(error.ErrorMessage);
                    }
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Errors = errorList });
            }
        }

        public HttpResponseMessage Put(int id, BlogModel model)
        {
            try
            {
                _repository.UpdateBlog(id, model.Title);
                _repository.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (DbEntityValidationException dbEx)
            {
                var errorList = new List<string>();
                foreach (DbEntityValidationResult entityErr in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in entityErr.ValidationErrors)
                    {
                        errorList.Add(error.ErrorMessage);
                    }
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Errors = errorList });
            }
        }
    }
}
