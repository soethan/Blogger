using System;
using System.Collections.Generic;
using BlogApi.DomainClasses;

namespace BlogApi.DataAccessLayer.Repositories
{
    public interface IBlogsRepository
    {
        List<Blog> GetAllBlogs();
        List<Post> GetAllPosts();
        Blog GetBlog(int id);
        void AddBlog(Blog blog);
        void UpdateBlog(int id, string title);
        void AddPost(int blogId, Post post);
        int SaveChanges();
    }
}
