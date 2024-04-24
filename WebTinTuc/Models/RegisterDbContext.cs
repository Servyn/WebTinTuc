using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using WebTinTuc.Models;

namespace WebTinTuc.Models
{
    public class RegisterDbContext : DbContext
    {
        public RegisterDbContext() : base("name=QL_WEBTINTUC")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Article> Articles { get; set; }
    }
}