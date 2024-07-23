using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using DataAccessObjects;

namespace UI.Pages.Accounts
{
    public class DetailsModel : PageModel
    {
        private readonly DataAccessObjects.AppDBContext _context;

        public DetailsModel(DataAccessObjects.AppDBContext context)
        {
            _context = context;
        }

      public Account Account { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            
            if (id == null || _context.Accounts == null)
            {
                return NotFound();
            }
            string role = HttpContext.Session.GetString("ROLE");
			var userId = HttpContext.Session.GetInt32("ID");
			if (role != "ADMIN" )
            {
                if(userId != id)
                {
					return RedirectToPage("/AccessDenied");
				}
                
            }

            var account = await _context.Accounts.FirstOrDefaultAsync(m => m.AccountId == id);
            if (account == null)
            {
                return NotFound();
            }
            else 
            {
                Account = account;
            }
            return Page();
        }
    }
}
