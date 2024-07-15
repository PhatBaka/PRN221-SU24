using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects;
using DataAccessObjects;

namespace UI.Pages
{
    public class CreateModel : PageModel
    {
        private readonly DataAccessObjects.AppDBContext _context;

        public CreateModel(DataAccessObjects.AppDBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["JewelryId"] = new SelectList(_context.Jewelries, "JewelryId", "Description");
        ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId");
            return Page();
        }

        [BindProperty]
        public Warranty Warranty { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Warranties == null || Warranty == null)
            {
                return Page();
            }

            _context.Warranties.Add(Warranty);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
