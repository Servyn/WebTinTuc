using System.Data.Entity;
using WebTinTuc.Models;

namespace WebTinTuc.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("QL_WEBTINTUC")
        {
        }

        public DbSet<Article> Articles { get; set; } // Định nghĩa DbSet cho bảng Article

        // Định nghĩa các DbSet khác nếu cần thiết
    }
}
