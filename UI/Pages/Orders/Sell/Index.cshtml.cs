using BusinessObjects;
using BusinessObjects.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;

namespace UI.Pages.Orders.Sell
{
    public class IndexModel : PageModel
    {
        private readonly IOrderService _orderService;

        public IndexModel(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public IList<Order> Orders;

        public void OnGet()
        {
            Orders = _orderService.GetAllOrdersAsync().Result.Where(x => x.OrderType == OrderEnum.NEW).ToList();
        }
    }
}
