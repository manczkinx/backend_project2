using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LMSServer;

namespace LMSRazorPages.Data
{
    public class LMSRazorPagesContext : DbContext
    {
        public LMSRazorPagesContext (DbContextOptions<LMSRazorPagesContext> options)
            : base(options)
        {
        }

        public DbSet<LMSServer.User> User { get; set; } = default!;
    }
}
