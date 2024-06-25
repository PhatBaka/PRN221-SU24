﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects;
using DataAccessObjects;
using Services.Interfaces;
//using Services.RequestModels.Account;
using UI.Payload.AccountPayload;
using BusinessObjects.Enums;
using BusinessObjects.Commons;

namespace UI.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IAccountService _accountService;

        public LoginModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public LoginRequest Account { get; set; } = default!;
        public string Message { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || Account == null)
            {
                return Page();
            }

            var adminEmail = StringConstants.ADMIN_EMAIL;
            var adminPassword = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AdminAccount:Password").Value;

            if (Account.Email == adminEmail && Account.Password == adminPassword)
            {
                HttpContext.Session.SetString("ROLE", "ADMIN");
            }

            var existedAccount = _accountService.GetAccount(Account.Email, Account.Password);
            
            if (existedAccount != null)
            {
                switch (existedAccount.Role)
                {
                    case AccountRole.STAFF:
                        HttpContext.Session.SetInt32("ID", existedAccount.AccountId);
                        HttpContext.Session.SetString("ROLE", "STAFF");
                        return RedirectToPage("./Jewelries/Index");
                    case AccountRole.MANAGER:
                        HttpContext.Session.SetInt32("ID", existedAccount.AccountId);
                        HttpContext.Session.SetString("ROLE", "MANAGER");
                        return RedirectToPage("./Accounts/Index");
                }
            }
            else
            {
                Message = "Can't find account";
            }

            return RedirectToPage("./Index");
        }
    }
}
