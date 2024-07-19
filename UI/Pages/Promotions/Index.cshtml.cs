using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects;
using Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace UI.Pages.Promotions
{
    public class IndexModel : PageModel
    {
        private readonly IPromotionService _promotionService;

        public IndexModel(IPromotionService promotionService)
        {
            _promotionService = promotionService;
        }

        public IList<Promotion> Promotion { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            var role = HttpContext.Session.GetString("ROLE");
            if (role != "ADMIN")
            {
                return RedirectToPage("/AccessDenied");
            }

            Promotion = await _promotionService.GetAllPromotionsAsync();
            return Page();
        }
    }
}
