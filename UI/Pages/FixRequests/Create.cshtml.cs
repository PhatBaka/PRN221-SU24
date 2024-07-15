using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects;
using DataAccessObjects;
using AutoMapper;
using Services.Interfaces;
using UI.Payload.WarrantyPayload;
using BusinessObjects.Enums;

namespace UI.Pages.Warranties.FixRequests
{
    public class CreateModel : PageModel
    {

        private readonly IWarrantyService warrantySerivce;
        private readonly IJewelryService jewelryService;
        private readonly IWarrantyHistoryService warrantyHistoryService;
        private IMapper mapper;

        public CreateModel(IServiceProvider service)
        {
            jewelryService = service.GetRequiredService<IJewelryService>();
            warrantySerivce = service.GetRequiredService<IWarrantyService>();
            warrantyHistoryService = service.GetRequiredService<IWarrantyHistoryService>();
            mapper = service.GetRequiredService<IMapper>();
        }

        [BindProperty]
        public WarrantyFixRequest WarrantyFixRequest { get; set; } = default!;
        public Warranty WarrantyRequestToFix { get; set; } = default!;
        private WarrantyHistory WarrantyHistory { get; set; } = default!;
		[BindProperty(SupportsGet = true)]
		public int? OrderId { get; set; }
		public string Message { get; set; }
        public Boolean hasExpired { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {
            if (OrderId == null)
            {
                Message = "Enter warranty id to create warranty request";
                return Page();
            }
            else
            {
				Warranty warranty = warrantySerivce.GetWarrantyById(OrderId.Value);
				if (warranty == null)
				{
					Message = $"Warranty ID {OrderId.Value} not found";
					return Page();
                }
                else
                {
					WarrantyRequestToFix = warranty;
					hasExpired = warrantyHistoryService.IsDateWithinWarrantyPeriod(DateTime.Now, warranty.ActiveDate, warranty.EndDate);
				}	
			}
            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            WarrantyHistory warrantyHistory = new WarrantyHistory();
            warrantyHistory = mapper.Map<WarrantyHistory>(WarrantyFixRequest);
            try
            {
                warrantyHistoryService.AddWarrantyHistory(warrantyHistory);
            }catch(Exception ex)
            {
				Message = ex.Message;
                OrderId = WarrantyFixRequest.WarrantyId;
				return await OnGetAsync();
			}
            return RedirectToPage("./Index");
        }

		
	}
}
