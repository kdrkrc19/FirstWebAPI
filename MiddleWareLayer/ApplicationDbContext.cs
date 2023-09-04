using Microsoft.EntityFrameworkCore;

namespace Web_UI.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {

        }

        public DbSet<UserModel> user { get; set; }
        public DbSet<DiaryModel> diary { get; set; }

    }
}
