using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects;
using Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace UI.Pages.Promotions
{
    public class EditModel : PageModel
    {
        private readonly IPromotionService _promotionService;

        public EditModel(IPromotionService promotionService)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _promotionService.UpdatePromotionAsync(Promotion);
           

            return RedirectToPage("./Index");
        }

    }
}
