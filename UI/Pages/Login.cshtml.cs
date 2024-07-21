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
//using Services.RequestModels.Account;
using UI.Payload.AccountPayload;
using BusinessObjects.Enums;
using BusinessObjects.Commons;
using System.Text.Json;
using UI.Helper;
using UI.Payload.MaterialPayload;
using System.Diagnostics;

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
            var role = HttpContext.Session.GetString("ROLE");
            Debug.WriteLine(222222222222);
            if (role != null)
            {
                switch (role)
                {
                    case "ADMIN":
                        return RedirectToPage("./IndexHome");
                    case "MANAGER":
                        return RedirectToPage("./IndexHome");
                    case "STAFF":
                        return RedirectToPage("./IndexHome");
                }
            }
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
				HttpContext.Session.SetString("EMAIL", adminEmail);
				HttpContext.Session.SetString("FULLNAME", adminEmail);
				HttpContext.Session.SetString("ISAUTHENTICATED", "True");
				return RedirectToPage("./Accounts/Index");

			}

            var existedAccount = _accountService.GetAccount(Account.Email, Account.Password);
            
            if (existedAccount != null && existedAccount.ObjectStatus == ObjectStatus.ACTIVE)
            {
				HttpContext.Session.SetString("ISAUTHENTICATED", "True");
				switch (existedAccount.Role)
                {
                    case AccountRole.STAFF:
                        HttpContext.Session.SetInt32("ID", existedAccount.AccountId);
						HttpContext.Session.SetString("FULLNAME", existedAccount.FullName);
						HttpContext.Session.SetString("EMAIL", existedAccount.Email);
						HttpContext.Session.SetString("ROLE", "STAFF");
                        return RedirectToPage("./IndexHome");
                    case AccountRole.MANAGER:
                        HttpContext.Session.SetInt32("ID", existedAccount.AccountId);
						HttpContext.Session.SetString("FULLNAME", existedAccount.FullName);
						HttpContext.Session.SetString("EMAIL", existedAccount.Email);
						HttpContext.Session.SetString("ROLE", "MANAGER");
                        return RedirectToPage("./IndexHome");
                    case AccountRole.ADMIN:
                        HttpContext.Session.SetInt32("ID", existedAccount.AccountId);
						HttpContext.Session.SetString("FULLNAME", existedAccount.FullName);
						HttpContext.Session.SetString("EMAIL", existedAccount.Email);
						HttpContext.Session.SetString("ROLE", "ADMIN");
                        return RedirectToPage("./Accounts");
                }
            }
            else
            {
				HttpContext.Session.SetString("ISAUTHENTICATED", "False");
				Message = "Can't find account";
            }

            return Page();
        }
    }
}
