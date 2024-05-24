using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using DataAccessObjects;

namespace UI.Pages.Jewelries
{
    public class IndexModel : PageModel
    {
        private readonly DataAccessObjects.AppDBContext _context;

        public IndexModel(DataAccessObjects.AppDBContext context)
        {
            _context = context;
        }

        public IList<Jewelry> Jewelry { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Jewelry != null)
            {
                Jewelry = await _context.Jewelry
                .Include(j => j.Category).ToListAsync();
            }
        }
    }
}
