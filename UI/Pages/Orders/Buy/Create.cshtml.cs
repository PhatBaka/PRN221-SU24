using AutoMapper;
using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;
using UI.Helper;
using UI.Payload.JewelryPayload;
using UI.Payload.MaterialPayload;

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
        public IList<MetalItem> MetalCart { get; set; }
        public IList<CartItem> Cart { get; set; }

        public void OnGet()
        {
            LoadData();
           
        }

		[BindProperties]
		public class CartItem
		{
            public decimal SellPrice { get; set; }
			public GetJewelryRequest Jewelry { get; set; }
			public int Quantity { get; set; }
			public decimal UnitPrice { get; set; }
		}

        [BindProperties]
		public class MetalItem
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
            LoadCart();
        }

        public IActionResult OnPostAddMetalToCart(string metal)
        {
            LoadData();
            MetalCart ??= new List<MetalItem>();

            if (MetalCart != null && MetalCart.Count > 0)
            {
                foreach (var item in MetalCart)
                {
                    // check metal already in cart
                    if (item.Item.Metal.Equals(metal))
                    {
                        return Page();
                    }
                }
            }

            MetalItem metalCart = new()
            {
                Item = Metals.FirstOrDefault(x => x.Metal.Equals(metal))
            };

            MetalCart.Add(metalCart);
            HttpContext.Session.SetObjectAsJson("METALCART", MetalCart);

            return Page();
        }

        public IActionResult OnPostUpdateCart(decimal weight, string metal)
        {
            LoadData();

            // check metal already in the cart
            if (Cart != null && Cart.Count > 0)
            {
                var existedItem = Cart.FirstOrDefault(x => x.Jewelry.JewelryName.Equals(metal));
                if (existedItem != null)
                {
                    existedItem.Jewelry.TotalWeight = weight;
                    existedItem.UnitPrice = Metals.FirstOrDefault(x => x.Metal.Equals(metal)).Rate.Bid * weight;
                }
            }
            else
            {

                GetJewelryRequest jewelry = new()
                {
                    JewelryName = metal,
                    Description = "FROM CUSTOMER",
                    TotalWeight = weight
                };

                CartItem cartItem = new CartItem()
                {
                    SellPrice = Metals.FirstOrDefault(x => x.Metal.Equals(metal)).Rate.Bid,
                    Jewelry = jewelry,
                    UnitPrice = Metals.FirstOrDefault(x => x.Metal.Equals(metal)).Rate.Bid * weight
                };

                Cart ??= new List<CartItem>();
                Cart.Add(cartItem);
            }

            HttpContext.Session.SetObjectAsJson("CART", Cart);

            return Page();
        }

        private void LoadMetalCart() => MetalCart = HttpContext.Session.GetObjectFromJson<IList<MetalItem>>("METALCART");
        private void LoadPrice() => Metals = _metalService.GetPrices();
        private void LoadCart() => Cart = HttpContext.Session.GetObjectFromJson<IList<CartItem>>("CART");
    }
}
