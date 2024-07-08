using DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Services.Helpers;
using Services.Impls;
using Services.Interfaces;

namespace UI.Pages.Gems
{
    public class IndexModel : PageModel
    {
        private readonly IGemService _gemService;

        public IndexModel(IGemService gemService)
        {
            _gemService = gemService;
        }

        public PaginatedList<GetGemDTO> Gems { get; set; }
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

            IQueryable<GetGemDTO> gemsIQ = _gemService.GetGems().Result.AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
                gemsIQ = gemsIQ.Where(s => s.CertificateCode.Contains(searchString));

            Gems = PaginatedList<GetGemDTO>.Create(
                gemsIQ.AsNoTracking(), pageIndex ?? 1, 5);
        }
    }
}
