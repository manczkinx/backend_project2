using LMSApi.Model;
using Microsoft.EntityFrameworkCore;

namespace LMSApi.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Member> Members { get; set; }
    }
}
