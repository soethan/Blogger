using BlogApi.DataAccessLayer.EntityConfigurations;
using BlogApi.DomainClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApi.DataAccessLayer
{
    public class BlogsContext: DbContext
    {
        public BlogsContext()
            : base("name=DefaultConnection")
        {

        }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new BlogConfiguration());
        }
    }
}
