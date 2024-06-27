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
    public class DeleteModel : PageModel
    {
        private readonly AppDBContext _context;

        public DeleteModel(AppDBContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Jewelry Jewelry { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Jewelries == null)
            {
                return NotFound();
            }

            var jewelry = await _context.Jewelries.FirstOrDefaultAsync(m => m.JewelryId == id);

            if (jewelry == null)
            {
                return NotFound();
            }
            else 
            {
                Jewelry = jewelry;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Jewelries == null)
            {
                return NotFound();
            }
            var jewelry = await _context.Jewelries.FindAsync(id);

            if (jewelry != null)
            {
                Jewelry = jewelry;
                _context.Jewelries.Remove(Jewelry);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
