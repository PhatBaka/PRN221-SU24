using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using DataAccessObjects;
using AutoMapper;
using Services.Interfaces;
using BusinessObjects.Enums;

namespace UI.Pages.Warranties
{
	public class EditModel : PageModel
	{
		private readonly IWarrantyService warrantySerivce;
		private readonly IJewelryService jewelryService;
		private IMapper mapper;


		public EditModel(IServiceProvider service)
		{
			jewelryService = service.GetRequiredService<IJewelryService>();
			warrantySerivce = service.GetRequiredService<IWarrantyService>();
			mapper = service.GetRequiredService<IMapper>();
		}

		public SelectList timeEnumSelectList { get; set; }
		public string Message { get; set; }

		[BindProperty]
		public Warranty Warranty { get; set; } = default!;

		public async Task<IActionResult> OnGetAsync(int? id)
		{
			try
			{
				if (id == null)
				{
					throw new Exception("Warranty ID is required");
				}

				var warranty = warrantySerivce.GetWarrantyById(id.Value);
				if (warranty == null)
				{
					throw new Exception("Warranty not found");
				}
				Warranty = warranty;
			}
			catch (Exception ex)
			{
				Message = ex.Message;
				SetUpPreData();
				return Page();
			}


			SetUpPreData();
			return Page();
		}


		private void SetUpPreData()
		{
			var timeEnumValues = Enum.GetValues(typeof(TimeEnum)).Cast<TimeEnum>().Select(e => new { Value = e.ToString(), Text = e.ToString() }).ToList();
			timeEnumSelectList = new SelectList(timeEnumValues, "Value", "Text");
		}

		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see https://aka.ms/RazorPagesCRUD.
		public async Task<IActionResult> OnPostAsync()
		{
			try
			{
			  await	warrantySerivce.UpdateWarrantyAsync(Warranty);
			}
			catch (Exception ex)
			{
				Message = ex.Message;
				return await OnGetAsync(Warranty.WarrantyId);
			}
			return RedirectToPage("./Index");
		}
		public async Task<IActionResult> OnPostCancel(int id)
		{
			try
			{
				if (id == null)
				{
					throw new Exception("Warranty ID is required");
				}
				Warranty existedWarranty = warrantySerivce.GetWarrantyById(id);

				if (existedWarranty == null)
				{
					throw new Exception("Warranty not found");
				}
				await warrantySerivce.UpdateWarrantyStatusAsync(existedWarranty, WarrantyStatus.CANCELLED);
			}
			catch (Exception ex)
			{
				Message = ex.Message;
				return await OnGetAsync(id);
			}
			return RedirectToPage("./Index");
		}

		public async Task<IActionResult> OnPostExpired(int id)
		{
			try
			{
				if (id == null)
				{
					throw new Exception("Warranty ID is required");
				}
				Warranty existedWarranty = warrantySerivce.GetWarrantyById(id);

				if (existedWarranty == null)
				{
					throw new Exception("Warranty not found");
				}
				await warrantySerivce.UpdateWarrantyStatusAsync(existedWarranty, WarrantyStatus.EXPIRED);
			}
			catch (Exception ex)
			{
				Message = ex.Message;
				return await OnGetAsync(id);
			}
			return RedirectToPage("./Index");
		}
	}
}
