using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;

namespace UI.Pages.Orders.Buy
{
    public class CreateModel : PageModel
    {
        private readonly IAccountService _accountService;
        private readonly IOrderService _orderService;
        private readonly IMetalService _metalService;
        private readonly IJewelryService _jewelryService;
        private readonly IMapper _mapper;

        public CreateModel(IAccountService accountService, IOrderService orderService, IMetalService metalService, IJewelryService jewelryService, IMapper mapper)
        {
            _accountService = accountService;
            _orderService = orderService;
            _metalService = metalService;
            _jewelryService = jewelryService;
            _mapper = mapper;
        }

        public IList<MetalResponse>? Metals { get; set; }

        public void OnGet()
        {
            Metals = _metalService.GetPrices();
        }
    }
}
