using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using DataAccessObjects;

namespace UI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly DataAccessObjects.AppDBContext _context;

        public IndexModel(DataAccessObjects.AppDBContext context)
        {
            _context = context;
        }

        public IList<Warranty> Warranty { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Warranties != null)
            {
                Warranty = await _context.Warranties
                .Include(w => w.Jewelry)
                .Include(w => w.Order).ToListAsync();
            }
        }
    }
}
