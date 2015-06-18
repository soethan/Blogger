using BlogApi.DomainClasses;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace BlogApi.DataAccessLayer.EntityConfigurations
{
    public class BlogConfiguration : EntityTypeConfiguration<Blog>
    {
        public BlogConfiguration()
        {
            Property(b => b.BloggerName).IsRequired().HasMaxLength(100);
        }
    }
}
