using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace WebTinTuc.Models
{
    public class DataContext : DbContext
    {

        public DataContext() : base("name=QL_WEBTINTUC")
        {
        }

        public DbSet<Contacts> Contacts { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<User> Users { get; set; }

    }
}