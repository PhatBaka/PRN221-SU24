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
    public class DetailsModel : PageModel
    {
        private readonly IPromotionService _promotionService;

        public DetailsModel(IPromotionService promotionService)
        {
            _promotionService = promotionService;
        }

        public Promotion Promotion { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var role = HttpContext.Session.GetString("ROLE");
            if (role != "ADMIN")
            {
                return RedirectToPage("/AccessDenied");
            }

            if (id == null)
            {
                return NotFound();
            }

            var promotion = await _promotionService.GetPromotionByIdAsync(id.Value);
            if (promotion == null)
            {
                return NotFound();
            }

            Promotion = promotion;
            return Page();
        }
    }
}
