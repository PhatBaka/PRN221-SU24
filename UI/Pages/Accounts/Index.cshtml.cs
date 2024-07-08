using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using DataAccessObjects;
using Services.Interfaces;
using Services.Helpers;
using DTOs;

namespace UI.Pages.Accounts
{
    public class IndexModel : PageModel
    {
        private readonly IAccountService _accountService;
        private readonly IConfiguration Configuration;

        public IndexModel(IAccountService accountService, 
                            IConfiguration configuration)
        {
            _accountService = accountService;
            Configuration = configuration;
        }

        public PaginatedList<GetAccountDTO> Accounts { get;set; } = default!;
        public string? CurrentFilter { get; set; }

        public async Task OnGetAsync(string currentFilter, 
                                        string searchString,
                                        int? pageIndex)
        {
            if (searchString != null)
                pageIndex = 1;
            else
                searchString = currentFilter;

            CurrentFilter = searchString;

            IQueryable<GetAccountDTO> studentsIQ = _accountService.GetAccounts().Result.AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
                studentsIQ = studentsIQ.Where(s => s.Email.Contains(searchString));

            Accounts = PaginatedList<GetAccountDTO>.Create(
                studentsIQ.AsNoTracking(), pageIndex ?? 1, 5);
        }
    }
}
