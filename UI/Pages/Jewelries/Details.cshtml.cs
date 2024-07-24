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

        public DetailsModel(IServiceProvider service)
        {
            _metalService = service.GetRequiredService<IMetalService>();
            jewerlryService = service.GetRequiredService<IJewelryService>();
            categoryService = service.GetRequiredService<ICategoryService>();
        }
        public Jewelry Jewelry { get; set; } = default!;
        public string ImageDataBase64String { get; set; }
        public IList<MetalResponse> Metals { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
			string role = HttpContext.Session.GetString("ROLE");
			if (role == "ADMIN")
			{
				return RedirectToPage("/AccessDenied");
			}

			Jewelry jewelry = jewerlryService.GetJewelryById(id.Value);
            
            ImageDataBase64String = StringConstants.IMAGE_DATABASE64_DEFAULT;
            if (jewelry.JewelryImage != null && jewelry.JewelryImage.Length > 0)
            {
                ImageDataBase64String = Convert.ToBase64String(jewelry.JewelryImage);
            }
            if (jewelry == null)
            {
                Message = "Jewelry is not fould";
                return Page();
            }
            else
            {
                Jewelry = jewelry;
                ViewData["StatusSaleDisplay"] = StatusSaleExtension.GetDisplayName(jewelry.StatusSale);
                ViewData["BasePriceDisplay"] = jewerlryService.GetJewelrySalePrice(jewelry).ToString("C2");
                
                Metals = _metalService.GetPrices();
                foreach(var metalmaterial in Jewelry.JewelryMaterials)
                {
                    double BIDPRICE ;
                    double OFFERPRICE;
                    if (metalmaterial.Material.IsMetail) {
                        _metalService.GetMetalSellPriceAndBuybackPriceByMetalName(metalmaterial.Material.MaterialName, out BIDPRICE, out OFFERPRICE);
                        metalmaterial.Material.BidPrice = (decimal) BIDPRICE;
                        metalmaterial.Material.OfferPrice = (decimal) OFFERPRICE;
                    }
                    
                }

            }
            return Page();
        }
    }
}
