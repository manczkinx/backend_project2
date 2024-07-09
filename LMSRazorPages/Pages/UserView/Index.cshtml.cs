using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LMSRazorPages.Data;
using LMSServer;

namespace LMSRazorPages.Pages.UserView
{
    public class IndexModel : PageModel
    {
        private readonly LMSRazorPages.Data.LMSRazorPagesContext _context;

        public IndexModel(LMSRazorPages.Data.LMSRazorPagesContext context)
        {
            _context = context;
        }

        public IList<User> User { get;set; } = default!;

        public async Task OnGetAsync()
        {
            User = await _context.User.ToListAsync();
        }
    }
}
