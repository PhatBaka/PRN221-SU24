using AutoMapper;
using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Impls;
using Services.Interfaces;
using System.Text.Json;
using System.Text.RegularExpressions;
using UI.Helper;
using UI.Payload.AccountPayload;
using UI.Payload.MaterialPayload;

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

        public IList<MetalResponse> Metals = new List<MetalResponse>();
        public GetAccountRequest CurrentCustomer { get; set; }
        public Account Account { get; set; }
        public string Message { get; set; }
        public IList<GetMaterialRequest> MaterialCart { get; set; }

        public void OnGet()
        {
            IList<GetMaterialRequest> carts = _mapper.Map<IList<GetMaterialRequest>>(_materialService.GetMaterials().Where(x => x.IsMetail == true));
            HttpContext.Session.SetObjectAsJson("BUYCART", carts);

            LoadData();
        }

        private void LoadData()
        {
            LoadMetals();
            LoadCart();
        }

        private void LoadMetals()
        {
            Metals = _metalService.GetPrices();
        }

        public IActionResult OnPostAddMetalToCart(string metal)
        {
            LoadData();

            return Page();
        }

        public IActionResult OnPostFindCustomer(string phoneNumber)
        {
            LoadData();

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

        public async Task<IActionResult> OnPostSubmitOrderJsonAsync()
        {
            using (var reader = new StreamReader(Request.Body))
            {
                var body = await reader.ReadToEndAsync();
                var cartData = JsonSerializer.Deserialize<List<GetMaterialRequest>>(body);

                // Process the cartData...
            }

            return new JsonResult(new { success = true });
        }

        public void LoadCart() => MaterialCart = HttpContext.Session.GetObjectFromJson<IList<GetMaterialRequest>>("BUYCART");
    }
}
