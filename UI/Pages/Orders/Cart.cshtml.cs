using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects;
using Services.Interfaces;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Services.Impls;
using System.Diagnostics;
using BusinessObjects.Enums;

namespace UI.Pages.Orders
{
    public class CartModel : PageModel
    {
        private readonly IJewelryService _jewelryService;
        private readonly IOrderService _orderService;

        public CartModel(IServiceProvider service)
        {
            _jewelryService = service.GetRequiredService<IJewelryService>() ?? throw new ArgumentNullException(nameof(_jewelryService));
            _orderService = service.GetRequiredService<IOrderService>();
        }

        [BindProperty]
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();

        public void OnGet()
        {
            var cartJson = HttpContext.Session.GetString("Cart");
            if (!string.IsNullOrEmpty(cartJson))
            {
                var cart = JsonConvert.DeserializeObject<Dictionary<int, int>>(cartJson);
                if (cart != null)
                {
                    foreach (var item in cart)
                    {
                        var jewelry = _jewelryService.GetJewelryById(item.Key);
                        if (jewelry != null)
                        {
                            CartItems.Add(new CartItem
                            {
                                Jewelry = jewelry,
                                Quantity = item.Value
                            });
                        }
                    }
                }
            }
        }

        public class CartItem
        {
            public Jewelry Jewelry { get; set; }
            public int Quantity { get; set; }
        }

        public IActionResult OnPostRemove(int id)
        {
            var cartJson = HttpContext.Session.GetString("Cart");
            if (!string.IsNullOrEmpty(cartJson))
            {
                var cart = JsonConvert.DeserializeObject<Dictionary<int, int>>(cartJson);
                if (cart != null && cart.ContainsKey(id))
                {
                    cart.Remove(id);
                    HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart));
                }
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostSubmitOrder()
        {
            
            if (CartItems == null || CartItems.Count == 0)
            {
                Debug.WriteLine("Count: " + CartItems.Count);
                return RedirectToPage();
            }

            var newOrder = new Order
            {
                OrderDate = DateTime.Now,
                OrderType = OrderEnum.NEW, // Assuming 1 is the type for customer orders
                CustomerId = 1
            };

            List<OrderDetail> items = new List<OrderDetail>();
            foreach (var cartItem in CartItems)
            {
                Debug.WriteLine(cartItem.Jewelry.JewelryName);
                var orderDetail = new OrderDetail
                {
                    OrderId = newOrder.OrderId,
                    JewelryId = cartItem.Jewelry.JewelryId,
                    Quantity = cartItem.Quantity,
                    PromotionDetailId = null,
                    UnitPrice = (double)cartItem.Jewelry.LaborPrice,
                    DiscountPercent = 0
                };
                items.Add(orderDetail);
            }
            await _orderService.CreateOrderAsync(newOrder, items);
            return RedirectToPage("OrderConfirmation", new { orderId = newOrder.OrderId });
        }
    }
}
