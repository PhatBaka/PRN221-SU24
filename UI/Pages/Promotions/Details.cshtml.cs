using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using DataAccessObjects;

namespace UI.Pages.Promotions
{
    public class DetailsModel : PageModel
    {
        private readonly DataAccessObjects.AppDBContext _context;

        public DetailsModel(DataAccessObjects.AppDBContext context)
        {
            _context = context;
        }

      public Promotion Promotion { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Promotion == null)
            {
                return NotFound();
            }

            var promotion = await _context.Promotion.FirstOrDefaultAsync(m => m.PromotionId == id);
            if (promotion == null)
            {
                return NotFound();
            }
            else 
            {
                Promotion = promotion;
            }
            return Page();
        }
    }
}
