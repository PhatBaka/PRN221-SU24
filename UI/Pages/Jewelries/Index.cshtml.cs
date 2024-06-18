using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using DataAccessObjects;
using Services.Interfaces;
using Services.Impls;
using BusinessObjects.Enums;

namespace UI.Pages.Jewelries
{
    public class IndexModel : PageModel
    {
        private IJewelryService jewelryService;

        public IndexModel(IServiceProvider service)
        {
            jewelryService = service.GetRequiredService<IJewelryService>() ?? throw new ArgumentNullException("null jewelrt service");
        }

        public List<Jewelry> Jewelry { get;set; } = default!;

        public async Task OnGetAsync()
        {
           List<Jewelry> jewelryList = jewelryService.GetJewelries();
            if(jewelryList == null)
            {
                throw new Exception("Cannot get jewelry list");
            }
            Jewelry = jewelryList;
            //Jewelry.ForEach(jewelry => ViewData[$"ItemStatusSale_{jewelry.JewelryId}"] = StatusSaleExtension.GetDisplayName(jewelry.StatusSale));
            foreach (var item in Jewelry)
            {
                ViewData[$"ItemStatusSale_{item.JewelryId}"] = StatusSaleExtension.GetDisplayName(item.StatusSale);
                ViewData[$"ItemBasePrice_{item.JewelryId}"] = Decimal.Parse("200000000");
            }

        }
    }
}
