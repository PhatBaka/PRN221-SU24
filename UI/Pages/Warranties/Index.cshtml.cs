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
using Services.Interfaces;
using BusinessObjects.Enums;
using Castle.Core.Internal;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace UI.Pages.Warranties
{
	public class IndexModel : PageModel
	{
		private readonly IWarrantyService warrantyService;
		private readonly IJewelryService jewelryService;
		private IMapper mapper;

		public IndexModel(IServiceProvider service)
		{
			jewelryService = service.GetRequiredService<IJewelryService>();
			warrantyService = service.GetRequiredService<IWarrantyService>();
			mapper = service.GetRequiredService<IMapper>();
		}

		public IList<Warranty> Warranty { get; set; } = default!;
		[BindProperty(SupportsGet = true)]
		public string Keyword { get; set; }
		[BindProperty(SupportsGet = true)]
		public string WarrantyId { get; set; }
		[BindProperty(SupportsGet = true)]
		public string JewelryId { get; set; }
		[BindProperty(SupportsGet = true)]
		public String WarrantyStatusSelect { get; set; }
		[BindProperty(SupportsGet = true)]
		public string SortBy { get; set; }

		public SelectList WarrantyStatusOptions { get; set; }

		public async Task OnGetAsync()
		{
			var warranties = warrantyService.GetAllWarrantys().AsQueryable();

			if (!string.IsNullOrEmpty(Keyword))
			{
				warranties = warranties.Where(w => w.Jewelry.JewelryName.Contains(Keyword, StringComparison.OrdinalIgnoreCase) ||
												   w.Order.Customer.FullName.Contains(Keyword, StringComparison.OrdinalIgnoreCase) ||
												   w.Order.Customer.PhoneNumber.Contains(Keyword));
			}

			if (!string.IsNullOrEmpty(WarrantyId))
			{
				warranties = warranties.Where(w => w.WarrantyId.ToString() == WarrantyId);
			}

			if (!string.IsNullOrEmpty(JewelryId))
			{
				warranties = warranties.Where(w => w.JewelryId.ToString() == JewelryId);
			}

			if (!string.IsNullOrEmpty(WarrantyStatusSelect))
			{
				warranties = warranties.Where(w => w.WarrantyStatus.ToString().Equals(WarrantyStatusSelect));
			}

			if (!string.IsNullOrEmpty(SortBy))
			{
				switch (SortBy)
				{
					case "ActivateDate":
						warranties = warranties.OrderBy(w => w.ActiveDate);
						break;
					case "EndDate":
						warranties = warranties.OrderBy(w => w.EndDate);
						break;
				}
			}
			Warranty = warranties.ToList();
		}




	}
}
