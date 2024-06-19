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

namespace UI.Pages.Warranties
{
    public class EditModel : PageModel
    {
        private readonly DataAccessObjects.AppDBContext _context;

        public EditModel(DataAccessObjects.AppDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public WarrantyRequest WarrantyRequest { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.WarrantyRequests == null)
            {
                return NotFound();
            }

            var warrantyrequest =  await _context.WarrantyRequests.FirstOrDefaultAsync(m => m.WarrantyRequestId == id);
            if (warrantyrequest == null)
            {
                return NotFound();
            }
            WarrantyRequest = warrantyrequest;
           ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "FullName");
           ViewData["WarrantyOrderId"] = new SelectList(_context.WarrantyOrders, "WarrantyOrderId", "WarrantyOrderId");
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

            _context.Attach(WarrantyRequest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WarrantyRequestExists(WarrantyRequest.WarrantyRequestId))
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

        private bool WarrantyRequestExists(int id)
        {
          return (_context.WarrantyRequests?.Any(e => e.WarrantyRequestId == id)).GetValueOrDefault();
        }
    }
}
