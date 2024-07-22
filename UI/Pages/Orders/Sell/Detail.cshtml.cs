using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;

namespace UI.Pages.Orders.Sell
{
    public class DetailModel : PageModel
    {
        private readonly IOrderService _orderService;

        public DetailModel(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public Order Order { get; set; }

        public void OnGet(int id)
        {
            try
            {
                Order = _orderService.GetOrderByIdAsync(id).Result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
