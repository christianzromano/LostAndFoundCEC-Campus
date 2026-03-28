using Microsoft.EntityFrameworkCore;
using LostAndFound.Models;

namespace LostAndFound.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Users> Users { get; set; }
    }
}
