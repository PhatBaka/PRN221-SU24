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
using Services.Impls;

namespace UI.Pages.Warranties.FixRequests
{
    public class IndexModel : PageModel
    {
        private readonly IWarrantyService warrantySerivce;
        private readonly IJewelryService jewelryService;
        private readonly IWarrantyHistoryService warrantyHistoryService;
        private IMapper mapper;

        public IndexModel(IServiceProvider service)
        {
            jewelryService = service.GetRequiredService<IJewelryService>();
            warrantySerivce = service.GetRequiredService<IWarrantyService>();
            warrantyHistoryService = service.GetRequiredService<IWarrantyHistoryService>();
            mapper = service.GetRequiredService<IMapper>();
        }

        public IList<WarrantyHistory> WarrantyHistory { get;set; } = default!;

		public IList<Warranty> Warranty { get; set; } = default!;
		[BindProperty(SupportsGet = true)]
		public string Keyword { get; set; }
		[BindProperty(SupportsGet = true)]
		public string WarrantyHistoryId { get; set; }
		[BindProperty(SupportsGet = true)]
		public string WarrantyId { get; set; }
		[BindProperty(SupportsGet = true)]
		public string JewelryId { get; set; }
		[BindProperty(SupportsGet = true)]
		public String WarrantyFixStatusSelect { get; set; }
		[BindProperty(SupportsGet = true)]
		public string SortBy { get; set; }
		
        public async Task OnGetAsync()
        {

            var warrantyHistory = warrantyHistoryService.GetAllWarrantyHistory().AsQueryable();

			if (!string.IsNullOrEmpty(Keyword))
			{
				warrantyHistory = warrantyHistory.Where(w => w.Warranty.Jewelry.JewelryName.Contains(Keyword, StringComparison.OrdinalIgnoreCase) ||
												   w.Warranty.Order.Customer.FullName.Contains(Keyword, StringComparison.OrdinalIgnoreCase) ||
												   w.Warranty.Order.Customer.PhoneNumber.Contains(Keyword));
			}

			if (!string.IsNullOrEmpty(WarrantyHistoryId))
			{
				warrantyHistory = warrantyHistory.Where(w => w.WarrantyHistoryId.ToString() == WarrantyHistoryId);
			}
			if (!string.IsNullOrEmpty(WarrantyId))
			{
				warrantyHistory = warrantyHistory.Where(w => w.WarrantyId.ToString() == WarrantyId);
			}

			if (!string.IsNullOrEmpty(JewelryId))
			{
				warrantyHistory = warrantyHistory.Where(w => w.Warranty.JewelryId.ToString() == JewelryId);
			}

			if (!string.IsNullOrEmpty(WarrantyFixStatusSelect))
			{
				warrantyHistory = warrantyHistory.Where(w => w.status.ToString().Equals(WarrantyFixStatusSelect));
			}

			if (!string.IsNullOrEmpty(SortBy))
			{
				switch (SortBy)
				{
					case "ReceivedDate":
						warrantyHistory = warrantyHistory.OrderBy(w => w.ReceivedDate);
						break;
					case "ReturnDate":
						warrantyHistory = warrantyHistory.OrderBy(w => w.ReturnDate);
						break;
				}
			}
			WarrantyHistory = warrantyHistory.ToList();
		}
    }
}
