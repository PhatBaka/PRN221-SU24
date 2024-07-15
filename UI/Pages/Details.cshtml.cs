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
    public class DetailsModel : PageModel
    {
        private readonly DataAccessObjects.AppDBContext _context;

        public DetailsModel(DataAccessObjects.AppDBContext context)
        {
            _context = context;
        }

      public Warranty Warranty { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Warranties == null)
            {
                return NotFound();
            }

            var warranty = await _context.Warranties.FirstOrDefaultAsync(m => m.WarrantyId == id);
            if (warranty == null)
            {
                return NotFound();
            }
            else 
            {
                Warranty = warranty;
            }
            return Page();
        }
    }
}
