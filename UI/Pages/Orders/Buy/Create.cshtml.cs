using AutoMapper;
using BusinessObjects;
using BusinessObjects.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;
using System.Diagnostics.Metrics;
using UI.Helper;
using UI.Payload.AccountPayload;
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
        private readonly IMaterialService _materialService;
        private readonly IMapper _mapper;

        public CreateModel(IAccountService accountService, IOrderService orderService, IMetalService metalService, IJewelryService jewelryService, IMaterialService materialService, IMapper mapper)
        {
            _accountService = accountService;
            _orderService = orderService;
            _metalService = metalService;
            _jewelryService = jewelryService;
            _materialService = materialService;
            _mapper = mapper;
        }

        public IList<MetalResponse>? Metals { get; set; }
        [BindProperty]
        public IList<MetalItem> MetalCart { get; set; }
        public IList<CartItem> Cart { get; set; }
        public GetAccountRequest CurrentCustomer { get; set; }
        public string Message { get; set; }

        public void OnGet()
        {
            string role = HttpContext.Session.GetString("ROLE");
            if (role != "STAFF" && role != "MANAGER")
            {
                RedirectToPage("/AccessDenied");
            }
            LoadData();
           
        }

		[BindProperties]
		public class CartItem
		{
            public int Index { get; set; }
            public double BuyPrice { get; set; }
			public GetJewelryRequest Jewelry { get; set; }
			public int Quantity { get; set; }
            public Category Category { get; set; }
            public double Weight { get; set; }
		}

        [BindProperties]
		public class MetalItem
		{
			public MetalResponse? Item { get; set; }
			public decimal Weight { get; set; }
		}

        public IActionResult OnPostRemoveFromCart(int index, int jewelryId)
        {
            LoadData();

            if (jewelryId == 0)
            {
                Cart.RemoveAt(index);
            }

            return Page();
        }

        private void LoadData()
        {
            LoadMetalCart();
            LoadPrice();
            LoadCart();
            LoadCustomer();
        }

        public IActionResult OnPostAddMetalToCart(string metal, int i)
        {
            LoadData();
            MetalCart ??= new List<MetalItem>();

            //if (MetalCart != null && MetalCart.Count > 0)
            //{
            //    foreach (var item in MetalCart)
            //    {
            //        // check metal already in cart
            //        if (item.Item.Metal.Equals(metal))
            //        {
            //            return Page();
            //        }
            //    }
            //}

            MetalItem metalCart = new()
            {
                Item = Metals.FirstOrDefault(x => x.Metal.Equals(metal))
            };

            MetalCart.Add(metalCart);
            HttpContext.Session.SetObjectAsJson("METALCART", MetalCart);

            return Page();
        }

        public IActionResult OnPostUpdateCart(double weight, int index, string metal)
        {
            LoadData();

            if (weight <= 0)
            {
                Message = "Weight must be greater than 0.";
                return Page();
            }

            // update in the same cart
            // check metal already in the cart
            if (Cart != null && Cart.Count > 0)
            {
                var existedItem = Cart.FirstOrDefault(x => x.Index == index);
                if (existedItem != null)
                {
                    existedItem.Weight = weight;
                    return Page();
                }
            }

            GetJewelryRequest jewelry = new()
            { 
                JewelryName = metal,
                Description = "FROM CUSTOMER",
                TotalWeight = weight
            };

            CartItem cartItem = new CartItem()
            {
                Index = index,
                BuyPrice = Metals.FirstOrDefault(x => x.Metal.Equals(metal)).Rate.Bid,
                Jewelry = jewelry,
                Weight = weight
            };

            Cart ??= new List<CartItem>();
            Cart.Add(cartItem);

            HttpContext.Session.SetObjectAsJson("CART", Cart);

            return Page();
        }

        public async Task<IActionResult> OnPostCreateOrderAsync()
        {
            LoadData();
            if (CurrentCustomer == null)
            {
                Message = "Please enter customer";
                return Page();
            }

            List<OrderDetail> orderDetails = new List<OrderDetail>();
            if (Cart != null && Cart.Count > 0)
            {
                foreach (var item in Cart)
                {
                    // jewelryMaterials.Add(new JewelryMaterial { Material = material, JewelryWeight = metal.MaterialQuantWeight, Jewelry = jewelry });

                    JewelryMaterial jewelryMaterial = new JewelryMaterial()
                    {
                        Material = _materialService.GetMaterialByName(item.Jewelry.JewelryName.ToLower()),
                        JewelryWeight = item.Jewelry.TotalWeight
                    };

                    IList<JewelryMaterial> jewelryMaterials = new List<JewelryMaterial>();
                    jewelryMaterials.Add(jewelryMaterial);

                    Jewelry jewelry = new()
                    {
                        JewelryName = item.Jewelry.JewelryName,
                        Description = "From customer",
                        JewelryMaterials = jewelryMaterials,
                        CategoryId = 8
                    };

                    _jewelryService.AddJewelry(jewelry);
                    if (jewelry != null)
                    {

                        OrderDetail orderDetail = new OrderDetail()
                        {
                            JewelryId = jewelry.JewelryId,
                            UnitPrice = Cart.FirstOrDefault(x => x.Jewelry.JewelryName.Equals(jewelry.JewelryName.ToLower())).BuyPrice
                        };

                        orderDetails.Add(orderDetail);
                    }
                }

                Order order = new Order()
                {
                    CustomerId = CurrentCustomer.AccountId,
                    OrderDate = DateTime.Now,
                    OrderType = OrderEnum.OLD
                };

                var newOrder = _orderService.CreateOrderAsync(order, orderDetails).Result;

                if (newOrder != null)
                {
                    return RedirectToPage("./Detail", new { id = newOrder.OrderId });
                }

                return Page();
            }

            return Page();
        }

        public IActionResult OnPostFindCustomer(string phoneNumber)
        {
            LoadData();
            GetAccountRequest account = _mapper.Map<GetAccountRequest>(_accountService.GetAccounts().FirstOrDefault(x => x.PhoneNumber.Equals(phoneNumber)));

            if (account != null)
            {
                CurrentCustomer = account;
                HttpContext.Session.SetObjectAsJson("ACCOUNT", CurrentCustomer);
                return Page();
            }

            return Page();
        }

        public IActionResult OnPostRemoveTempCart()
        {
            HttpContext.Session.Remove("CART");
            HttpContext.Session.Remove("METALCART");
            LoadData();
            return Page();
        }

        private void LoadMetalCart() => MetalCart = HttpContext.Session.GetObjectFromJson<IList<MetalItem>>("METALCART");
        private void LoadPrice() => Metals = _metalService.GetPrices();
        private void LoadCart() => Cart = HttpContext.Session.GetObjectFromJson<IList<CartItem>>("CART");
        private void LoadCustomer() => CurrentCustomer = HttpContext.Session.GetObjectFromJson<GetAccountRequest>("ACCOUNT");
    }
}
