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

namespace UI.Pages.Warranties.FixRequests
{
    public class DeleteModel : PageModel
    {
		private readonly IWarrantyService warrantySerivce;
		private readonly IJewelryService jewelryService;
		private readonly IWarrantyHistoryService warrantyHistoryService;

		public DeleteModel(IServiceProvider service)
		{
			jewelryService = service.GetRequiredService<IJewelryService>();
			warrantySerivce = service.GetRequiredService<IWarrantyService>();
			warrantyHistoryService = service.GetRequiredService<IWarrantyHistoryService>();
		}
		[BindProperty]
		public WarrantyHistory WarrantyHistory { get; set; } = default!;
		public string Message { get; set; }


		public async Task<IActionResult> OnGetAsync(int? id)
        {
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
			try
			{
				var WarrantyHistory = warrantyHistoryService.GetWarrantyHistoryById(id.Value);
				if (WarrantyHistory == null)
				{
					throw new Exception($"Warranty History ID {id} not found to delete");
				}
				warrantyHistoryService.DeleteWarrantyHistory(id.Value);
			}
			catch (Exception ex)
			{
				Message = ex.Message;
				return Page();
			}
			return RedirectToPage("./Index");
        }
    }
}
