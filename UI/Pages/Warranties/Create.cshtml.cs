using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects;
using DataAccessObjects;

namespace UI.Pages.Warranties
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
        ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "FullName");
        ViewData["WarrantyOrderId"] = new SelectList(_context.WarrantyOrders, "WarrantyOrderId", "WarrantyOrderId");
            return Page();
        }

        [BindProperty]
        public WarrantyRequest WarrantyRequest { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.WarrantyRequests == null || WarrantyRequest == null)
            {
                return Page();
            }

            _context.WarrantyRequests.Add(WarrantyRequest);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
