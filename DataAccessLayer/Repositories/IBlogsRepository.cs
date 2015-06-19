using System;
using System.Collections.Generic;
using BlogApi.DomainClasses;
using System.Linq;

namespace BlogApi.DataAccessLayer.Repositories
{
    public interface IBlogsRepository
    {
        IQueryable<Blog> GetAllBlogs();
        Blog GetBlog(int id);
        void AddBlog(Blog blog);
        void UpdateBlog(int id, string title);
        void DeleteBlog(int id);

        IQueryable<Post> GetAllPosts(int blogId);
        void AddPost(int blogId, Post post);
        int SaveChanges();
    }
}
