using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects;
using BusinessObjects.Enums;
using AutoMapper;
using Services.Interfaces;

namespace UI.Pages.FixRequests
{
    public class EditModel : PageModel
    {
        private readonly IWarrantyService warrantySerivce;
        private readonly IJewelryService jewelryService;
        private readonly IWarrantyHistoryService warrantyHistoryService;
        private IMapper mapper;

        public EditModel(IServiceProvider service)
        {
            jewelryService = service.GetRequiredService<IJewelryService>();
            warrantySerivce = service.GetRequiredService<IWarrantyService>();
            warrantyHistoryService = service.GetRequiredService<IWarrantyHistoryService>();
            mapper = service.GetRequiredService<IMapper>();
        }

        [BindProperty]
        public WarrantyHistory WarrantyHistory { get; set; } = default!;
        public string Message { get; set; }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
			string role = HttpContext.Session.GetString("ROLE");
			if (role == "ADMIN")
			{
				return RedirectToPage("/AccessDenied");
			}
			if (id == null)
            {
                Message = $"Warranty History ID {id} not found to update";
                return Page();
            }

            WarrantyHistory = warrantyHistoryService.GetWarrantyHistoryById(id.Value);
            if (WarrantyHistory == null)
            {
                Message = $"Warranty History ID {id} not found to update";
                return Page();
            }

            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
			string role = HttpContext.Session.GetString("ROLE");
			if (role == "ADMIN")
			{
				return RedirectToPage("/AccessDenied");
			}
			try
            {
                warrantyHistoryService.UpdateWarrantyHistory(WarrantyHistory);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                return await OnGetAsync(WarrantyHistory.WarrantyHistoryId);
            }
            return RedirectToPage("./Index");
        }

        public IActionResult OnPostChangeStatus(int id, WarrantyFixStatus statusChange)
        {
            try
            {
                if (id == null)
                {
                    throw new("Warranty History ID is required");
                }
                WarrantyHistory existedWarrantyHistory = warrantyHistoryService.GetWarrantyHistoryById(id);

                if (existedWarrantyHistory == null)
                {
                    throw new("Warranty not found");
                }
                warrantyHistoryService.UpdateWarrantyHistoryStatus(id, statusChange);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                return OnGetAsync(id).Result;
            }
            return RedirectToPage("./Index");
        }
    }
}
