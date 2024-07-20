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
      public string message;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            string role = HttpContext.Session.GetString("ROLE");
            if (role != "MANAGER")
            {
                return RedirectToPage("/AccessDenied");
            }
            if (id == null || _context.Jewelries == null)
            {
                message = "Not found the jewelry id " + id;
                return Page();
            }

            var jewelry = await _context.Jewelries.FirstOrDefaultAsync(m => m.JewelryId == id);

            if (jewelry == null)
            {
                message = "Not found the jewelry id " + id;
                return Page();
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
                message = "Not found the jewelry id " + id;
                return Page();
            }
            var jewelry = await _context.Jewelries.FindAsync(id);
            try
            {
                if (jewelry != null)
                {
                    Jewelry = jewelry;
                    _context.Jewelries.Remove(Jewelry);
                    await _context.SaveChangesAsync();
                }
            }
            catch(Exception ex)
            {
                message = "Can not delete because the jewelry is in used.";
                    return Page();
            }
            

            return RedirectToPage("./Index");
        }
    }
}
