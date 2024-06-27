﻿using System;
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
using Microsoft.CodeAnalysis;
using System.Diagnostics;
using NuGet.Protocol.Core.Types;
using BusinessObjects.FilterModels;

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

        [BindProperty(SupportsGet = true)]
        public String search { get; set; } = default!;

        public void OnGetAsync()
        {
           List<Jewelry> jewelryList = jewelryService.GetJewelries();
            if(!string.IsNullOrEmpty(search))
            {
                
                JewelryFilter searchFilter = new JewelryFilter
                {
                    SearchKeyword = search
                };
			
			jewelryList = jewelryService.SearchFilterJewelries(searchFilter);
		}

            if (jewelryList == null)
            {
                throw new Exception("Cannot get jewelry list");
            }
            Jewelry = jewelryList;
            foreach (var item in Jewelry)
            {
                ViewData[$"ItemStatusSale_{item.JewelryId}"] = StatusSaleExtension.GetDisplayName(item.StatusSale);
                ViewData[$"ItemBasePrice_{item.JewelryId}"] = (decimal) jewelryService.GetJewelrySalePrice(item);
            }

        }

        public IActionResult OnPostBuy(int id)
        {
            var jewelry = jewelryService.GetJewelryById(id);
            if (jewelry != null)
            {

                Dictionary<int, int> cart;
                var cartString = HttpContext.Session.GetString("Cart");
                if (string.IsNullOrEmpty(cartString))
                {
                    cart = new Dictionary<int, int>();
                }
                else
                {
                    cart = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<int, int>>(cartString);
                }

                if (cart.ContainsKey(id))
                {
                    cart[id]++;
                }
                else
                {
                    cart[id] = 1;
                }

                HttpContext.Session.SetString("Cart", Newtonsoft.Json.JsonConvert.SerializeObject(cart));
            }

            return RedirectToPage();
        }
    }
}
