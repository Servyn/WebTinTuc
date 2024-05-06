using System.Data.Entity;

namespace WebTinTuc.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("QL_WEBTINTUC")
        {
        }

        public DbSet<Article> Articles { get; set; } // Định nghĩa DbSet cho bảng Article
        public DbSet<User> Users { get; set; } // Định nghĩa DbSet cho bảng Users
    }
}
