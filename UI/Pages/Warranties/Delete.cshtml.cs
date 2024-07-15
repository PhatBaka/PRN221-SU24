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

namespace UI.Pages.Warranties
{
    public class DeleteModel : PageModel
    {

		private readonly IWarrantyService warrantySerivce;
		private readonly IJewelryService jewelryService;
		private IMapper mapper;

		public DeleteModel(IServiceProvider service)
		{
			jewelryService = service.GetRequiredService<IJewelryService>();
			warrantySerivce = service.GetRequiredService<IWarrantyService>();
			mapper = service.GetRequiredService<IMapper>();
		}
		[BindProperty]
		public Warranty Warranty { get; set; } = default!;
		public string Message { get; set; }

		public async Task<IActionResult> OnGetAsync(int? id)
        {
			if (id == null)
			{
				Message = $"Warranty History ID {id} not found to update";
				return Page();
			}

			Warranty = warrantySerivce.GetWarrantyById(id.Value);
			if (Warranty == null)
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
				var warranty = warrantySerivce.GetWarrantyById(id.Value);
				if (warranty == null)
				{
					throw new Exception($"Warranty History ID {id} not found to delete");
				}
				warrantySerivce.DeleteWarranty(id.Value);
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
