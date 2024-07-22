using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using DataAccessObjects;
using System.Diagnostics;

namespace UI.Pages.Accounts
{
    public class IndexModel : PageModel
    {
        private readonly DataAccessObjects.AppDBContext _context;

        public IndexModel(DataAccessObjects.AppDBContext context)
        {
            _context = context;
        }

        public IList<Account> Account { get;set; } = default;

		public async Task OnGetAsync()
		{
			string role = HttpContext.Session.GetString("ROLE");

			if (role == null || role != "ADMIN")
			{
				Response.Redirect("/AccessDenied");
			}
			if (_context.Accounts != null)
			{
				Account = await _context.Accounts.ToListAsync();
			}
		}
	}
}
