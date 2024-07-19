using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects;
using Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;

namespace UI.Pages.Promotions
{
    public class CreateModel : PageModel
    {
        private readonly IPromotionService _promotionService;

        public CreateModel(IPromotionService promotionService)
        {
            _promotionService = promotionService;
        }

        public IActionResult OnGet()
        {
            var role = HttpContext.Session.GetString("ROLE");
            if (role != "ADMIN")
            {
                return RedirectToPage("/AccessDenied");
            }
            return Page();
        }

        [BindProperty]
        public Promotion Promotion { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || Promotion == null || DateTime.Compare(Promotion.StartDate, Promotion.EndDate) == -1)
            {
                if (Promotion != null && Promotion.StartDate > Promotion.EndDate)
                {
                    ModelState.AddModelError(nameof(Promotion.EndDate), "End Date must be later than Start Date.");
                }
                return Page();
            }

            try
            {
                await _promotionService.AddPromotionAsync(Promotion);
            }
            catch (Exception ex)
            {
                // Log the exception (e.g., using ILogger)
                Debug.WriteLine(ex.Message);
                ModelState.AddModelError(string.Empty, "An error occurred while creating the promotion.");
                return Page();
            }

            return RedirectToPage("./Index");
        }

    }
}
