using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using DataAccessObjects;

namespace UI.Pages.Warranties
{
    public class IndexModel : PageModel
    {
        private readonly DataAccessObjects.AppDBContext _context;

        public IndexModel(DataAccessObjects.AppDBContext context)
        {
            _context = context;
        }

        public IList<WarrantyRequest> WarrantyRequest { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.WarrantyRequests != null)
            {
                WarrantyRequest = await _context.WarrantyRequests
                .Include(w => w.Customer)
                .Include(w => w.WarrantyOrder).ToListAsync();
            }
        }
    }
}
