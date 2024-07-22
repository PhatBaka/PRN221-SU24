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
using BusinessObjects.Enums;

namespace UI.Pages.Accounts
{
    public class EditModel : PageModel
    {
        private readonly DataAccessObjects.AppDBContext _context;

        public EditModel(DataAccessObjects.AppDBContext context)
        {
            _context = context;
            SetRoleOptions();
        }

        [BindProperty]
        public Account Account { get; set; } = default!;
        public string Message { get; set; }
        public List<SelectListItem> RoleOptions { get; set; }



        public async Task<IActionResult> OnGetAsync(int? id)
        {
            string role = HttpContext.Session.GetString("ROLE");
            if (role != "ADMIN")
            {
                return RedirectToPage("/AccessDenied");
            }
            if (id == null || _context.Accounts == null)
            {
                Message = "Account ID " + id + "is not found!";
                SetRoleOptions();
                return Page();
            }

            var account =  await _context.Accounts.FirstOrDefaultAsync(m => m.AccountId == id);
            if (account == null)
            {
                Message = "Account ID " + id + "is not found!";
                SetRoleOptions();
                return Page();
            }
            Account = account;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            string role = HttpContext.Session.GetString("ROLE");
            if (role != "ADMIN")
            {
                return RedirectToPage("/AccessDenied");
            }
            if (!ModelState.IsValid)
            {
                SetRoleOptions();
                return Page();
            }


            try
            {
                _context.Attach(Account).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(Account.AccountId))
                {
                    Message = "Save new account failed! Account ID " + Account.AccountId + " is not found!";
                    SetRoleOptions();
                    return Page();
                }
                else
                {
                    Message = "Save new account failed! Account ID " + Account.AccountId + " is not found!";
                    SetRoleOptions();
                    return Page();
                }
            }catch(Exception ex)
            {
                Message = ex.Message;
                SetRoleOptions();
                return Page();
            }

            return RedirectToPage("./Index");
        }

        private bool AccountExists(int id)
        {
          return (_context.Accounts?.Any(e => e.AccountId == id)).GetValueOrDefault();
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
