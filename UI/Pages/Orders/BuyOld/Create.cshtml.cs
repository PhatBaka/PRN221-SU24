using AutoMapper;
using BusinessObjects;
using BusinessObjects.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UI.Helper;
using UI.Payload.AccountPayload;

namespace UI.Pages.Orders.BuyOld
{
    public class CreateModel : PageModel
    {
        private readonly IMaterialService _materialService;
        private readonly IMetalService _metalService;
        private readonly IOrderService _orderService;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public CreateModel(IMaterialService materialService, IMetalService metalService, IOrderService orderService, IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _materialService = materialService;
            _metalService = metalService;
            _orderService = orderService;
            _mapper = mapper;
        }

        public IList<MetalResponse> Metals { get; set; } = new List<MetalResponse>();
        public GetAccountRequest CurrentCustomer { get; set; }
        public Account Account { get; set; }
        public string Message { get; set; }
        [BindProperty]
        public IList<CartItem> MaterialCart { get; set; }

        public class CartItem
        {
            public int MaterialId { get; set; }
            public string MaterialName { get; set; }
            public decimal BidPrice { get; set; }
            public decimal Weight { get; set; }
            public bool IsMetal { get; set; }
        }

        public void OnGet()
        {
            IList<CartItem> carts = new List<CartItem>();
            foreach (var metal in _materialService.GetMaterials().Where(x => x.IsMetail))
            {
                CartItem cartItem = new CartItem()
                {
                    MaterialId = metal.MaterialId,
                    MaterialName = metal.MaterialName,
                    BidPrice = metal.BidPrice,
                    Weight = 0
                };
                carts.Add(cartItem);
            }
            MaterialCart = carts;
            //HttpContext.Session.SetObjectAsJson("BUYCART", carts);
            LoadData();
        }

        private void LoadData()
        {
            LoadMetals();
            // LoadCart();
        }

        private void LoadMetals()
        {
            Metals = _metalService.GetPrices();
        }

        public IActionResult OnPostFindCustomer(string phoneNumber)
        {
			IList<CartItem> carts = new List<CartItem>();
			foreach (var metal in _materialService.GetMaterials().Where(x => x.IsMetail))
			{
				CartItem cartItem = new CartItem()
				{
					MaterialId = metal.MaterialId,
					MaterialName = metal.MaterialName,
					BidPrice = metal.BidPrice,
					Weight = 0
				};
				carts.Add(cartItem);
			}
			MaterialCart = carts;
			//HttpContext.Session.SetObjectAsJson("BUYCART", carts);
			LoadData();

			string phonePattern = @"^09\d{8}$"; // Phone number starting with 09 followed by 8 digits

            if (!Regex.IsMatch(phoneNumber, phonePattern))
            {
                Message = "Invalid phone number format";
                return Page();
            }

            CurrentCustomer = _mapper.Map<GetAccountRequest>(_accountService.GetAccounts().FirstOrDefault(x => x.PhoneNumber.Equals(phoneNumber)));
            if (CurrentCustomer != null)
            {
                HttpContext.Session.SetObjectAsJson("ACCOUNT", CurrentCustomer.AccountId);
            }
            else
            {
                Message = "Cannot find this account";
            }

			return Page();
        }

        public IActionResult OnPostSubmitOrderJson()
        {
            LoadData();

            //Console.WriteLine("OnPostSubmitOrderJson called!");
            //Console.WriteLine($"cartData: {cartData}");

            //if (cartData == null || !cartData.Any())
            //{
            //    return new JsonResult(new { success = false, message = "No data provided" });
            //}
            string CurrentCustomer = HttpContext.Session.GetString("ACCOUNT");
			if (CurrentCustomer == null)
            {
                Message = "Please enter customer info";
                return Page();
            }

            ICollection<OrderDetail> orderDetails = new List<OrderDetail>();
            foreach (var item in MaterialCart)
            {
                Material material = _materialService.GetMaterialById(item.MaterialId);
                if (item.IsMetal && item.Weight != 0)
                {
                    orderDetails.Add(new OrderDetail()
                    {
                        MaterialId = item.MaterialId,
                        MetalWeight = item.Weight,
                        UnitPrice = (double)(item.BidPrice * item.Weight)
                    });
                }
                else if (!item.IsMetal)
                {
                    orderDetails.Add(new OrderDetail()
                    {
                        MaterialId = item.MaterialId,
                        UnitPrice = material.MaterialCost
                    });
                }
            }

            Order order = new()
            {
                OrderDate = DateTime.Now,
                CustomerId = Int32.Parse(CurrentCustomer),
                OrderType = OrderEnum.OLD,
                OrderDetails = new List<OrderDetail>()
            };

            if (_orderService.CreateOrderAsync(order, orderDetails) != null)
            {
                Message = "Order create successfully";
            }

            return new JsonResult(new { success = true, message = "Order processed successfully" });
        }

        //private void LoadCart()
        //{
        //    MaterialCart = HttpContext.Session.GetObjectFromJson<IList<CartItem>>("BUYCART");
        //}
    }
}
