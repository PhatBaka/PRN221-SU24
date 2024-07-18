using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;
using UI.Helper;
using UI.Payload.JewelryPayload;

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
        public IList<MetalCart> MetalCarts { get; set; }

        public void OnGet()
        {
            LoadData();
           
        }

		[BindProperties]
		public class CartItem
		{
			public GetJewelryRequest Jewelry { get; set; }
			public int Quantity { get; set; }
			public decimal UnitPrice { get; set; }
			public decimal DiscountValue { get; set; }
			public decimal GetTotalPrice() => UnitPrice * Quantity;
			public decimal GetDiscountPrice() => GetTotalPrice() * DiscountValue / 100;
			public decimal GetFinalPrice() => GetTotalPrice() - GetDiscountPrice();
		}

        [BindProperties]
		public class MetalCart
		{
			public MetalResponse? Item { get; set; }
			public decimal Weight { get; set; }
		}

        public IActionResult OnPostRemoveMetalFromCart(decimal weight)
        {
            LoadData();

            return Page();
        }

        private void LoadData()
        {
            LoadMetalCart();
            LoadPrice();
        }

        public IActionResult OnPostAddMetalToCart(string metal)
        {
            LoadData();
            MetalCarts ??= new List<MetalCart>();

            if (MetalCarts != null && MetalCarts.Count > 0)
            {
                foreach (var item in MetalCarts)
                {
                    // check metal already in cart
                    if (item.Item.Metal.Equals(metal))
                    {
                        return Page();
                    }
                }
            }

            MetalCart metalCart = new()
            {
                Item = Metals.FirstOrDefault(x => x.Metal.Equals(metal))
            };

            MetalCarts.Add(metalCart);
            HttpContext.Session.SetObjectAsJson("METALCARTS", MetalCarts);

            return Page();
        }

        private void LoadMetalCart() => MetalCarts = HttpContext.Session.GetObjectFromJson<IList<MetalCart>>("METALCARTS");
        private void LoadPrice() => Metals = _metalService.GetPrices();
    }
}
