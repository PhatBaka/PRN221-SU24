using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;
using BusinessObjects;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace UI.Pages.Orders
{
    public class IndexModel : PageModel
    {
        private readonly IOrderService _orderService;

        public IndexModel(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public List<Order> UserOrders { get; set; } = new List<Order>();

        public async Task<IActionResult> OnGetAsync()
        {
            int? accountID = HttpContext.Session.GetInt32("ID");
            if (accountID == null)
            {
                return RedirectToPage("/Login");
            }

            UserOrders = (await _orderService.GetOrdersByUserIdAsync(accountID.Value)).ToList();

            return Page();
        }
    }
}
