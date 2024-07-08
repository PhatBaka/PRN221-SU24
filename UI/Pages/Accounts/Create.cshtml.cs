using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects;
using DataAccessObjects;
using Services.Interfaces;
using DTOs;
using DTOs.Enums;

namespace UI.Pages.Accounts
{
    public class CreateModel : PageModel
    {
        private readonly IAccountService _accountService;

        public CreateModel(IAccountService accountService)
        {
            _accountService = accountService;
            RoleOptions = Enum.GetValues(typeof(RoleEnum))
                                 .Cast<RoleEnum>()
                                 .Select(e => new SelectListItem
                                 {
                                     Value = ((int)e).ToString(),
                                     Text = e.ToString()
                                 }).ToList();
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public AccountDTO Account { get; set; } = default!;
        public List<SelectListItem>? RoleOptions { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || Account == null)
            {
                return Page();
            }

            if (await _accountService.CreateAccount(Account))
            {
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
