using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using DataAccessObjects;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Interfaces;
using BusinessObjects.Enums;
using BusinessObjects.Commons;

namespace UI.Pages.Jewelries
{
    public class DetailsModel : PageModel
    {
        private readonly IJewelryService jewerlryService;
        private readonly ICategoryService categoryService;
        private readonly IMetalService _metalService;
        public string Message { get; set; }

        public DetailsModel(IServiceProvider service, IMetalService metalService)
        {
            _metalService = metalService;
            jewerlryService = service.GetRequiredService<IJewelryService>();
            categoryService = service.GetRequiredService<ICategoryService>();
        }
        public Jewelry Jewelry { get; set; } = default!;
        public string ImageDataBase64String { get; set; }
        public IList<MetalResponse> Metals { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            string role = HttpContext.Session.GetString("ROLE");
            if (role != "MANAGER")
            {
                RedirectToPage("/AccessDenied");
            }

            Jewelry jewelry = jewerlryService.GetJewelryById(id.Value);
            Metals = _metalService.GetPrices();
            ImageDataBase64String = StringConstants.IMAGE_DATABASE64_DEFAULT;
            if (jewelry.JewelryImage != null && jewelry.JewelryImage.Length > 0)
            {
                ImageDataBase64String = Convert.ToBase64String(jewelry.JewelryImage);
            }
            if (jewelry == null)
            {
                return NotFound();
            }
            else
            {
                Jewelry = jewelry;
                //ViewData["StatusSaleDisplay"] = StatusSaleExtension.GetDisplayName(jewelry.StatusSale);
                //ViewData["BasePriceDisplay"] = (Decimal) jewerlryService.GetJewelrySalePrice(jewelry);
            }
            return Page();
        }
    }
}
