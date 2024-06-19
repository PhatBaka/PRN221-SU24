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
    public class DetailsModel : PageModel
    {
        private readonly DataAccessObjects.AppDBContext _context;

        public DetailsModel(DataAccessObjects.AppDBContext context)
        {
            _context = context;
        }

      public WarrantyRequest WarrantyRequest { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.WarrantyRequests == null)
            {
                return NotFound();
            }

            var warrantyrequest = await _context.WarrantyRequests.FirstOrDefaultAsync(m => m.WarrantyRequestId == id);
            if (warrantyrequest == null)
            {
                return NotFound();
            }
            else 
            {
                WarrantyRequest = warrantyrequest;
            }
            return Page();
        }
    }
}
