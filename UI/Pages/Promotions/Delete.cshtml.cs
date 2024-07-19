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
    public class DeleteModel : PageModel
    {
        private readonly DataAccessObjects.AppDBContext _context;

        public DeleteModel(DataAccessObjects.AppDBContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Promotion Promotion { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Promotions == null)
            {
                return NotFound();
            }

            var promotion = await _context.Promotions.FirstOrDefaultAsync(m => m.PromotionId == id);

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Promotions == null)
            {
                return NotFound();
            }
            var promotion = await _context.Promotions.FindAsync(id);

            if (promotion != null)
            {
                Promotion = promotion;
                _context.Promotions.Remove(Promotion);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
