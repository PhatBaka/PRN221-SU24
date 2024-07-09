using DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Services.Helpers;
using Services.Impls;
using Services.Interfaces;

namespace UI.Pages.Orders
{
    public class CreateModel : PageModel
    {
        private readonly IJewelryService _jewelryService;

        public CreateModel(IJewelryService jewelryService)
        {
            _jewelryService = jewelryService;
        }

        public PaginatedList<GetJewelryDTO> Jewelries { get; set; }
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

            IQueryable<GetJewelryDTO> jewelryDTO = _jewelryService.GetJewelries().Result.AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
                jewelryDTO = jewelryDTO.Where(s => s.JewelryId.ToString() == searchString);

            Jewelries = PaginatedList<GetJewelryDTO>.Create(
                jewelryDTO.AsNoTracking(), pageIndex ?? 1, 5);
        }
    }
}
