using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BlogApi.DomainClasses
{
    public class Blog
    {
        public Blog()
        {
            Posts = new List<Post>();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string BloggerName { get; set; }
        public List<Post> Posts { get; set; }
        public byte[] RowVersion { get; set; }
        public DateTime CreatedDate {
            get
            {
                return DateTimeOffset.UtcNow.UtcDateTime;
            }
            private set { }
        }

        //public string BlogCode
        //{
        //    get
        //    {
        //        return Title.Substring(0, 1) + ":" + BloggerName.Substring(0, 1);
        //    }
        //    private set
        //    { 
            
        //    }
        //}
        
    }
}
