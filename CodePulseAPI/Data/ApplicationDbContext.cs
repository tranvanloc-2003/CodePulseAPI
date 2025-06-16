using CodePulseAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace CodePulseAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Categories> categories { get; set; }
         public DbSet<BlogImage> BlogImages { get; set; }
    }
}
