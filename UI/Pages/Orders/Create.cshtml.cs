using DTOs;
using DTOs.Carts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using UI.Helper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Helpers;

namespace UI.Pages.Orders
{
    public class CreateModel : PageModel
    {
        private readonly IJewelryService _jewelryService;
        private readonly IOrderService _orderService;

        public CreateModel(IJewelryService jewelryService, IOrderService orderService)
        {
            _jewelryService = jewelryService;
            _orderService = orderService;
        }

        public PaginatedList<GetJewelryDTO> Jewelries { get; set; }
        public IList<JewelryCart> JewelryCarts { get; set; }
        public string? CurrentFilter { get; set; }

        public async Task OnGetAsync(string currentFilter,
                                        string searchString,
                                        int? pageIndex)
        {
            LoadData(currentFilter, searchString, pageIndex);
        }

        public async Task<IActionResult> OnPostAddToCart(Guid JewelryId)
        {
            LoadData("", "", null);
            JewelryCarts ??= new List<JewelryCart>();
            var jewelryCart = new JewelryCart
            {
                GetJewelryDTO = await _jewelryService.GetJewelryById(JewelryId),
                Quantity = 1
            };
            JewelryCarts.Add(jewelryCart);
            HttpContext.Session.SetObjectAsJson("JEWELRYCART", JewelryCarts);
            return Page();
        }

        private void LoadCart()
        {
            JewelryCarts = HttpContext.Session.GetObjectFromJson<List<JewelryCart>>("JEWELRYCART") ?? new List<JewelryCart>();
        }

        private void LoadJewelry(string currentFilter,
                                        string searchString,
                                        int? pageIndex)
        {
            if (searchString != null)
                pageIndex = 1;
            else
                searchString = currentFilter;
            CurrentFilter = searchString;

            IQueryable<GetJewelryDTO> jewelryDTO = _jewelryService.GetJewelries().Result.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
                jewelryDTO = jewelryDTO.Where(s => s.JewelryId.ToString() == searchString);

            Jewelries = PaginatedList<GetJewelryDTO>.Create(
                jewelryDTO.AsNoTracking(), pageIndex ?? 1, 5);
        }

        private void LoadData(string currentFilter,
                                       string searchString,
                                       int? pageIndex)
        {
            LoadCart();
            LoadJewelry(currentFilter, searchString, pageIndex);
        }

        public async Task<IActionResult> OnPostCreateOrder(string phoneNumber)
        {
            LoadData("", "", 1);
            await _orderService.CreateOrder(JewelryCarts, phoneNumber);
            return Page();
        }
    }
}
