using BlogApi.DomainClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlogApi.DataAccessLayer.Repositories;
using System.Data.Entity.Infrastructure;

namespace BlogApi.DataAccessLayer.Repositories
{
    public class BlogsRepository : IBlogsRepository
    {
        private BlogsContext _context;

        public BlogsRepository(BlogsContext context)
        {
            _context = context;
        }

        public IQueryable<Blog> GetAllBlogs()
        {
            return _context.Blogs.AsQueryable();
        }

        public Blog GetBlog(int id)
        {
            return GetAllBlogs().Where(b => b.Id == id).FirstOrDefault();
        }

        public void AddBlog(Blog blog)
        {
            _context.Blogs.Add(blog);
        }

        public void UpdateBlog(int id, string title)
        {
            var blog = GetBlog(id);
            blog.Title = title;
        }

        public void DeleteBlog(int id)
        {
            var blog = GetBlog(id);
            _context.Blogs.Remove(blog);
        }

        public IQueryable<Post> GetAllPosts()
        {
            return _context.Posts.AsQueryable();
        }

        public void AddPost(int blogId, Post post)
        {
            var blog = GetBlog(blogId);
            blog.Posts.Add(post);
        }

        public int SaveChanges()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                //Overwrite existing Database values with new update values... 
                var entry = ex.Entries.Single();
                entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                return _context.SaveChanges();
            }
        }
    }
}
