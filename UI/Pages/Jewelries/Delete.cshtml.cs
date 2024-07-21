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
        public string message { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            string role = HttpContext.Session.GetString("ROLE");
            if (role != "MANAGER")
            {
                return RedirectToPage("/AccessDenied");
            }
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
                message = $"Jewelry iD {id} not found!";
                return Page();
            }
            var jewelry = await _context.Jewelries.FindAsync(id);
            try {
				if (jewelry != null)
				{
					Jewelry = jewelry;
					_context.Jewelries.Remove(Jewelry);
					await _context.SaveChangesAsync();
				}

			}catch(Exception ex)
            {
                message = "Cannot delete this jewelry id " + id + " because of in using constraint";
                Console.WriteLine(ex.Message);
                return Page();
            }


			return RedirectToPage("./Index");
        }
    }
}
