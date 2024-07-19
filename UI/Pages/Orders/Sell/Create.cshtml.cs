using AutoMapper;
using BusinessObjects.Enums;
using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Services.Helpers;
using Services.Interfaces;
using System.Diagnostics;
using UI.Helper;
using System.Text.Json;
using UI.Payload.MaterialPayload;
using JsonSerializer = System.Text.Json.JsonSerializer;
using UI.Payload.JewelryPayload;
using UI.Payload.AccountPayload;
using System.Text.RegularExpressions;
using Services.Impls;

namespace UI.Pages.Orders.Sell
{
    public class CreateModel : PageModel
    {
        private readonly IMetalService _metalService;
        private readonly IAccountService _accountService;
        private readonly IMaterialService _materialService;
        private readonly IJewelryService _jewelryService;
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public CreateModel(IJewelryService jewelryService, IOrderService orderService, IMaterialService materialService, IAccountService accountService, IMetalService metalService, IMapper mapper)
        {
            _materialService = materialService;
            _metalService = metalService;
            _orderService = orderService;
            _jewelryService = jewelryService;
            _accountService = accountService;
            _mapper = mapper;
        }

        public GetAccountRequest? CurrentCustomer { get; set; }
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
        public PaginatedList<Jewelry>? Jewelries { get; set; }
        public string? CurrentFilter { get; set; }
        public IList<MetalResponse> Metals = new List<MetalResponse>();
        public string? Message { get; set; }
        [BindProperty]
        public Account Account { get; set; }

        public void OnGet(string currentFilter, string searchString, int? pageIndex)
        {
            string role = HttpContext.Session.GetString("ROLE");
            if (role != "STAFF" && role != "MANAGER")
            {
                RedirectToPage("/AccessDenied");
            }
            LoadJewelries(currentFilter, searchString, pageIndex);
            Metals = _metalService.GetPrices();
            HttpContext.Session.SetObjectAsJson("PRICE", Metals);
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

        public IActionResult OnPostAddToCart(int JewelryId)
        {
            LoadData("", "", 1);
            Jewelry jewelry = _jewelryService.GetJewelryById(JewelryId);
            // total gem + total current price + labor price
            decimal unitPrice = 0;

            foreach (var material in jewelry.JewelryMaterials)
            {
                var currentMaterial = _materialService.GetMaterialById(material.MaterialId);

                if (currentMaterial.IsMetail)
                {
                    unitPrice += currentMaterial.OfferPrice * (decimal)material.JewelryWeight;
                }
                else
                {
                    unitPrice += (decimal)currentMaterial.MaterialCost;
                }
            }

            unitPrice += jewelry.LaborPrice;

            // check jewelry have gem or not
            // if jewelry have gem, don't show quantity
            // if jewelry don't have gem, show quantity

            if (CartItems != null && CartItems.Count > 0)
            {
                // check if item already in the cart
                foreach (var addedItem in CartItems)
                {
                    if (addedItem.Jewelry.JewelryId == jewelry.JewelryId)
                    {
                        // if jewelry have gem already in the cart
                        foreach (var material in addedItem.Jewelry.JewelryMaterials)
                        {
                            if (!material.Material.IsMetail)
                            {
                                Message = "This item already in the cart";
                                return Page();
                            }
                        }
                        addedItem.Quantity += 1;

                        if (addedItem.Quantity > jewelry.Quantity)
                        {
                            Message = $"Curren quantity of this item is: {jewelry.Quantity}";
                            addedItem.Quantity -= 1;
                            return Page();
                        }

                        SaveCart(CartItems);
                        return Page();
                    }
                }
            }

            CartItem item = new CartItem()
            {
                Jewelry = _mapper.Map<GetJewelryRequest>(jewelry),
                Quantity = 1,
                UnitPrice = unitPrice
            };

            CartItems.Add(item);

            SaveCart(CartItems);

            return Page();
        }

        private void SaveCart(List<CartItem> cartItems) => HttpContext.Session.SetObjectAsJson("CART", cartItems);
        private void LoadCart()
        {
            CartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>("CART");
            CartItems ??= new List<CartItem>();
        }

        public IActionResult OnPostRemoveFromCart(int id)
        {
            LoadData("", "", 1);
            //var cartJson = HttpContext.Session.GetString("Cart");
            //if (!string.IsNullOrEmpty(cartJson))
            //{
            //    var cart = JsonConvert.DeserializeObject<Dictionary<int, int>>(cartJson);
            //    if (cart != null && cart.ContainsKey(id))
            //    {
            //        cart.Remove(id);
            //        HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart));
            //    }
            //}
            foreach (var cartItem in CartItems)
            {
                if (cartItem.Jewelry.JewelryId == id)
                {
                    CartItems.Remove(cartItem);
                    SaveCart(CartItems);
                    break;
                }
            }
            return Page();
        }

        public async Task<IActionResult> OnPostSubmitOrder(string phoneNumber)
        {
            LoadData("", "", 1);

            //// Retrieve account ID from session
            //int? accountID = HttpContext.Session.GetInt32("ID");
            //if (accountID == null)
            //{
            //    // Handle case where user is not logged in
            //    return RedirectToPage("/Login");
            //}

            // Check if cart is empty
            if (CartItems == null || CartItems.Count == 0)
            {
                Message = "Cart is empty";
                return Page();
            }

            if (CurrentCustomer == null)
            {
                Message = "Please enter customer";
                return Page();
            }

            // Create new order
            var newOrder = new Order
            {
                OrderDate = DateTime.Now,
                OrderType = OrderEnum.NEW,
                CustomerId = CurrentCustomer.AccountId
            };

            // Initialize list of order details
            List<OrderDetail> items = new List<OrderDetail>();

            // Add order details from cart items
            foreach (var cartItem in CartItems)
            {
                Debug.WriteLine(cartItem.Jewelry.JewelryName);
                var orderDetail = new OrderDetail
                {
                    JewelryId = cartItem.Jewelry.JewelryId,
                    Quantity = cartItem.Quantity,
                    UnitPrice = (double) cartItem.GetFinalPrice(),
                    DiscountPercent = 0
                };

                items.Add(orderDetail);
            }

            foreach (var item in items)
            {
                var jewelry = _jewelryService.GetJewelryById(item.JewelryId);
                jewelry.Quantity -= item.Quantity;
                await _jewelryService.UpdateJewelryAsync(jewelry);
            }

            // Save order and order details
            Order order = await _orderService.CreateOrderAsync(newOrder, items);

            if (order != null)
            {
                HttpContext.Session.Remove("CART");
                return RedirectToPage("./Detail", new { id = newOrder.OrderId });
            }
            // Clear cart

            // Redirect to order confirmation page
            return Page();
        }

        private void LoadJewelries(string currentFilter,
                                        string searchString,
                                        int? pageIndex)
        {
            if (searchString != null)
                pageIndex = 1;
            else
                searchString = currentFilter;
            CurrentFilter = searchString;

            IQueryable<Jewelry> jewelries = _jewelryService.GetJewelries().Where(j =>
                                                !(j.JewelryMaterials.Any(m => !m.Material.IsMetail) && j.OrderDetails.Count > 0) &&
                                                !(j.JewelryMaterials.All(m => m.Material.IsMetail) && j.Quantity == 0)).AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
                jewelries = jewelries.Where(s => s.JewelryId.ToString() == searchString);

            Jewelries = PaginatedList<Jewelry>.Create(
                jewelries.AsNoTracking(), pageIndex ?? 1, 5);
        }

        private void UpdatePrice()
        {
            //var options = new JsonSerializerOptions
            //{
            //    PropertyNameCaseInsensitive = true
            //};
            //var gold = JsonSerializer.Deserialize<MetalPrice>(Utils.ReadJsonFile("gold.json"), options);
            //var silver = JsonSerializer.Deserialize<MetalPrice>(Utils.ReadJsonFile("silver.json"), options);
            //var palladium = JsonSerializer.Deserialize<MetalPrice>(Utils.ReadJsonFile("palladium.json"), options);
            //if (gold != null && silver != null && palladium != null)
            //{
            //    Metals.Add(gold);
            //    Metals.Add(silver);
            //    Metals.Add(palladium);
            //}

            var metals = _materialService.GetMaterials();
            foreach (var metal in metals.Where(x => x.IsMetail == true))
            {
                foreach (var price in Metals)
                {
                    if (price.Metal.Equals(metal.MaterialName))
                    {
                        metal.BidPrice = (decimal)price.Rate.Bid;
                        metal.OfferPrice = (decimal)price.Rate.Ask;
                        _materialService.UpdateMaterial(metal);
                        break;
                    }
                }
            }
        }

        public IActionResult OnPostClearCart()
        {
            HttpContext.Session.Remove("CART");
            LoadData("", "", 1);
            return Page();
        }

        public void LoadData(String searchString, String currentFilter, int pageIndex)
        {
            LoadJewelries(searchString, currentFilter, pageIndex);
            LoadCart();
            LoadCustomer();
            LoadPrice();
        }

        private void LoadPrice() => Metals = HttpContext.Session.GetObjectFromJson<IList<MetalResponse>>("PRICE");

        public void LoadCustomer()
        {
            CurrentCustomer = HttpContext.Session.GetObjectFromJson<GetAccountRequest>("ACCOUNT");
        }


        public IActionResult OnPostFindCustomer(string phoneNumber)
        {
            LoadData("", "", 1);

            // Define regex pattern for phone number validation
            string phonePattern = @"^09\d{8}$"; // Phone number starting with 09 followed by 8 digits

            // Validate phone number
            if (!Regex.IsMatch(phoneNumber, phonePattern))
            {
                Message = "Invalid phone number format";
                return Page();
            }

            // Find the customer by phone number
            CurrentCustomer = _mapper.Map<GetAccountRequest>(_accountService.GetAccounts().FirstOrDefault(x => x.PhoneNumber.Equals(phoneNumber)));
            if (CurrentCustomer != null)
            {
                HttpContext.Session.SetObjectAsJson("ACCOUNT", CurrentCustomer);
            }
            else
            {
                Message = "Cannot find this account";
            }

            return Page();
        }


        public IActionResult OnPostDecreaseQuantity(int id)
        {
            LoadData("", "", 1);

            foreach (var item in CartItems)
            {
                if (item.Jewelry.JewelryId == id)
                {
                    item.Quantity -= 1;
                    if (item.Quantity == 0)
                    {
                        CartItems.Remove(item);
                        break;
                    }
                }
            }

            SaveCart(CartItems);

            return Page();
        }

        public IActionResult OnPostIncreaseQuantity(int id)
        {
            LoadData("", "", 1);

            var jewelry = _jewelryService.GetJewelryById(id);

            foreach (var item in CartItems)
            {
                if (item.Jewelry.JewelryId == id)
                {
                    item.Quantity += 1;
                    if (item.Quantity > jewelry.Quantity)
                    {
                        Message = $"The remaining of this item is {jewelry.Quantity}";
                        item.Quantity -= 1;
                        return Page();
                    }
                }
            }

            SaveCart(CartItems);
            return Page();
        }

        public IActionResult OnPostCreateAccount()
        {
            LoadData("", "", 1);

            // Define regex patterns
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$"; // Basic email validation pattern
            string phonePattern = @"^09\d{8}$"; // Phone number starting with 09 followed by 8 digits

            // Validate email
            if (!Regex.IsMatch(Account.Email, emailPattern))
            {
                Message = "Invalid email format";
                return Page();
            }

            // Validate phone number
            if (!Regex.IsMatch(Account.PhoneNumber, phonePattern))
            {
                Message = "Invalid phone number format";
                return Page();
            }

            // Check if email already exists
            if (_accountService.GetAccounts().FirstOrDefault(x => x.Email.Equals(Account.Email)) != null)
            {
                Message = "This email already exists";
                return Page();
            }

            // Check if phone number already exists
            else if (_accountService.GetAccounts().FirstOrDefault(x => x.PhoneNumber.Equals(Account.PhoneNumber)) != null)
            {
                Message = "This phone number already exists";
                return Page();
            }

            // Create new account
            Account account = new()
            {
                CreatedDate = DateTime.Now,
                Email = Account.Email,
                FullName = Account.FullName,
                ObjectStatus = ObjectStatus.ACTIVE,
                PhoneNumber = Account.PhoneNumber,
                Role = AccountRole.CUSTOMER
            };

            if (account != null)
            {
                CurrentCustomer = _mapper.Map<GetAccountRequest>(account);
                HttpContext.Session.SetObjectAsJson("ACCOUNT", CurrentCustomer);
            }

            return Page();
        }
    }
}
