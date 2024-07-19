//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using BusinessObjects;
//using Services.Interfaces;
//using System.Threading.Tasks;

//namespace UI.Pages.Orders
//{
//    public class OrderConfirmationModel : PageModel
//    {
//        private readonly IOrderService _orderService;

//        public OrderConfirmationModel(IOrderService orderService)
//        {
//            _orderService = orderService;
//        }

//        public Order Order { get; set; }

//        public async Task<IActionResult> OnGetAsync(int orderId)
//        {
 //               string role = HttpContext.Session.GetString("ROLE");
//                if (role != "STAFF" && role != "MANAGER")
 //               {
  //                  RedirectToPage("/AccessDenied");
 //               }
//            Order = await _orderService.GetOrderByIdAsync(orderId);

//            if (Order == null)
//            {
//                return RedirectToPage("./Cart"); // Redirect to cart if order not found
//            }

//            return Page();
//        }
//    }
//}
