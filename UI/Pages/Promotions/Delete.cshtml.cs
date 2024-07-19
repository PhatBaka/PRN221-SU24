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
    public class DeleteModel : PageModel
    {
        private readonly IPromotionService _promotionService;

        public DeleteModel(IPromotionService promotionService)
        {
            _promotionService = promotionService;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
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

            try
            {
                await _promotionService.DeletePromotionAsync(id.Value);
            }
            catch (Exception ex)
            {
                // Log the exception (e.g., using ILogger)
                ModelState.AddModelError(string.Empty, "An error occurred while deleting the promotion.");
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
