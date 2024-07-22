using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects;
using BusinessObjects.Enums;
using DataAccessObjects;

namespace UI.Pages.Accounts
{
    public class CreateModel : PageModel
    {
        private readonly AppDBContext _context;

        public CreateModel(AppDBContext context)
        {
            _context = context;
            SetRoleOptions();
        }

        [BindProperty]
        public Account Account { get; set; } = default!;

        public List<SelectListItem> RoleOptions { get; set; }
        public string Message { get; set; }


        public IActionResult OnGet()
        {
            string role = HttpContext.Session.GetString("ROLE");
            if (role != "ADMIN")
            {
                return RedirectToPage("/AccessDenied");
            }

            SetRoleOptions();

            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            string role = HttpContext.Session.GetString("ROLE");
            if (role != "ADMIN")
            {
                return RedirectToPage("/AccessDenied");
            }
            try
            {
                Account.CreatedDate = DateTime.Now;
                Account.ObjectStatus = ObjectStatus.ACTIVE;
                _context.Accounts.Add(Account);
                await _context.SaveChangesAsync();
            }catch(Exception ex)
            {
                Message = ex.Message;
                return Page();
            }
            

            return RedirectToPage("./Index");
        }

        private void SetRoleOptions()
        {
            RoleOptions = Enum.GetValues<AccountRole>()
                .Select(r => new SelectListItem
                {
                    Value = r.ToString(),
                    Text = r.ToString()
                })
                .ToList();
                             
        }
    }
}
