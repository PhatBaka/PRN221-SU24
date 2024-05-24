using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using DataAccessObjects;

namespace UI.Pages.Jewelries
{
    public class EditModel : PageModel
    {
        private readonly DataAccessObjects.AppDBContext _context;

        public EditModel(DataAccessObjects.AppDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Jewelry Jewelry { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Jewelry == null)
            {
                return NotFound();
            }

            var jewelry =  await _context.Jewelry.FirstOrDefaultAsync(m => m.JewelryId == id);
            if (jewelry == null)
            {
                return NotFound();
            }
            Jewelry = jewelry;
           ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Jewelry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JewelryExists(Jewelry.JewelryId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool JewelryExists(int id)
        {
          return (_context.Jewelry?.Any(e => e.JewelryId == id)).GetValueOrDefault();
        }
    }
}
