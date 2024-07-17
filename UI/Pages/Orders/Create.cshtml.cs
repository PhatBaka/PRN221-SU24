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

namespace UI.Pages.Orders
{
    public class CreateModel : PageModel
    {
        private readonly IAccountService _accountService;
        private readonly IMaterialService _materialService;
        private readonly IJewelryService _jewelryService;
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public CreateModel(IJewelryService jewelryService, IOrderService orderService, IMaterialService materialService, IAccountService accountService, IMapper mapper)
        {
            _materialService = materialService;
            _orderService = orderService;
            _jewelryService = jewelryService;
            _accountService = accountService;
            _mapper = mapper;
        }

        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
        public PaginatedList<Jewelry> Jewelries { get; set; }
        public string? CurrentFilter { get; set; }
        private IList<MetalPrice> Prices = new List<MetalPrice>();
        private String Message { get; set; }

        public void OnGet(String currentFilter, string searchString, int? pageIndex)
        {
            LoadJewelries(currentFilter, searchString, pageIndex);
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
            LoadCart();
            LoadJewelries("", "", 1);
            UpdatePrice();
            Jewelry jewelry = _jewelryService.GetJewelryById(JewelryId);
            // total gem + total current price + labor price
            decimal unitPrice = 0;

            foreach (var material in jewelry.JewelryMaterials)
            {
                var currentMaterial = _materialService.GetMaterialById(material.MaterialId);

                if (currentMaterial.IsMetail)
                {
                    unitPrice += (decimal) currentMaterial.OfferPrice * (decimal) material.JewelryWeight;
                }
                else
                {
                    unitPrice += (decimal) currentMaterial.MaterialCost;
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
                        // if jewelry don't have gem in the cart
                        if (addedItem.Quantity >= jewelry.Quantity)
                        {
                            return Page();
                        }
                        addedItem.Quantity += 1;
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
            LoadJewelries("", "", 0);
            LoadCart();
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
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostSubmitOrder(String phoneNumber)
        {
            LoadJewelries("", "", 0);
            LoadCart();
            UpdatePrice();

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
                return RedirectToPage();
            }

            // Create new order
            var newOrder = new Order
            {
                OrderDate = DateTime.Now,
                OrderType = OrderEnum.NEW,
                CustomerId = _accountService.GetAccounts().FirstOrDefault(x => x.PhoneNumber == phoneNumber).AccountId
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
                    UnitPrice = (double)cartItem.Jewelry.LaborPrice,
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
            await _orderService.CreateOrderAsync(newOrder, items);

            // Clear cart
            HttpContext.Session.Remove("Cart");

            // Redirect to order confirmation page
            return RedirectToPage("OrderConfirmation", new { orderId = newOrder.OrderId });
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
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var gold = JsonSerializer.Deserialize<MetalPrice>(Utils.ReadJsonFile("gold.json"), options);
            var silver = JsonSerializer.Deserialize<MetalPrice>(Utils.ReadJsonFile("silver.json"), options);
            var palladium = JsonSerializer.Deserialize<MetalPrice>(Utils.ReadJsonFile("palladium.json"), options);
            if (gold != null && silver != null && palladium != null)
            {
                Prices.Add(gold);
                Prices.Add(silver);
                Prices.Add(palladium);
            }

            var metals = _materialService.GetMaterials();
            foreach (var metal in metals.Where(x => x.IsMetail == true))
            {
                foreach (var price in Prices)
                {
                    if (price.Metal.Equals(metal.MaterialName))
                    {
                        metal.BidPrice = price.Rate.Bid;
                        metal.OfferPrice = price.Rate.Ask;
                        _materialService.UpdateMaterial(metal);
                        break;
                    }
                }
            }
        }
    }
}
