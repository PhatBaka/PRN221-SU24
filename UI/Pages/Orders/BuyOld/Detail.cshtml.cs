//using BusinessObjects;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using Services.Interfaces;

//namespace UI.Pages.Orders.Buy
//{
//    public class DetailModel : PageModel
//    {
//        private readonly IOrderService _orderService;

//        public DetailModel(IOrderService orderService)
//        {
//            _orderService = orderService;
//        }

//        public Order Order { get; set; }

//        public void OnGet(int id)
//        {
//            Order = _orderService.GetOrderByIdAsync(id).Result;
//        }
//    }
//}
