using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects;
using DataAccessObjects;
using Services.Interfaces;
using System.ComponentModel.DataAnnotations;
using UI.Payload.JewelryPayload;
using Castle.Core.Internal;
using BusinessObjects.Enums;
using UI.AppStarts;
using AutoMapper;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Services.Helpers;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using System.Diagnostics.Metrics;

namespace UI.Pages.Jewelries
{
	public class CreateModel : PageModel
	{
		private readonly IJewelryService jewerlryService;
		private readonly ICategoryService categoryService;
		private readonly IMapper mapper;
		private readonly IMaterialService materialService;

		public List<SelectListItem> SaleStatusOptions { get; set; }
		public List<SelectListItem> CategoryOptions { get; set; }
		public List<SelectListItem> GemstoneOptions { get; set; }
		public List<SelectListItem> MetalOptions { get; set; }

		public string Message { get; set; }


		public CreateModel(IServiceProvider service)
		{
			jewerlryService = service.GetRequiredService<IJewelryService>();
			categoryService = service.GetRequiredService<ICategoryService>();
			materialService = service.GetRequiredService<IMaterialService>();
			mapper = service.GetRequiredService<IMapper>();
			this.UpdateSelectOptions();
		}

		public IActionResult OnGet()
		{
			return Page();
		}

		[BindProperty]
		public CreateJewelryRequest Jewelry { get; set; }

		public List<ProductMaterialDto> Metals { get; set; }

		[BindProperty]
		public string MetalsJson { get; set; }

		public List<ProductMaterialDto> Gemstones { get; set; }

		[BindProperty]
		public string GemstonesJson { get; set; }

		private static byte[] imageData = Convert.FromBase64String("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");

		// To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
		public IActionResult OnPost()
		{
			if (!MetalsJson.IsNullOrEmpty())
			{
				List<ProductMaterialDto> metalsInputList = JsonConvert.DeserializeObject<List<ProductMaterialDto>>(MetalsJson);
				Metals = metalsInputList;
			}

			if (!GemstonesJson.IsNullOrEmpty())
			{
				List<ProductMaterialDto> gemstonesInputList = JsonConvert.DeserializeObject<List<ProductMaterialDto>>(GemstonesJson);
				Gemstones = gemstonesInputList;
			}

			if (!ModelState.IsValid)
			{
				setupReturnPageWhenError(message: "Error handler");
				return Page();
			}
			if (!Jewelry.ImageDataFile.IsNullOrEmpty() && Jewelry.ImageDataFile.Any())
			{
				try
				{
					List<byte[]> imageDataBytes = ImageHelper.FormatImageFile(Jewelry.ImageDataFile);
					if (imageDataBytes.Count > 0)
					{
						imageData = imageDataBytes.FirstOrDefault();
					}
				}
				catch (Exception e)
				{
					setupReturnPageWhenError(message: e.Message);
					return Page();
				}
			}

			Jewelry jewelry = mapper.Map<Jewelry>(Jewelry);
			jewelry.JewelryImage = imageData;

			Category category = categoryService.GetCategoryByName(Jewelry.CategoryName);
			if (category == null)
			{
				category = new Category { CategoryName = Jewelry.CategoryName };
			}
			jewelry.Category = category;

			List<JewelryMaterial> jewelryMaterials = new List<JewelryMaterial>();
			if (Gemstones.IsNullOrEmpty() && Metals.IsNullOrEmpty())
			{
				setupReturnPageWhenError(message: "Gemstone or Metal is required");
				return Page();
			}
			foreach (var gemstone in Gemstones)
			{
				Material material = materialService.GetMaterialById(gemstone.MaterialId);
				if (material == null || material.IsMetail)
				{
					setupReturnPageWhenError(message: $"Gemstone {material.MaterialName} is not found");
					return Page();
				}
				var list = Gemstones.Where(x => x.MaterialId == gemstone.MaterialId).ToList();
				if (list.Count > 1)
				{
					setupReturnPageWhenError(message: $"Gemstone {material.MaterialName} is duplicated");
					return Page();
				}
				if (gemstone.MaterialQuantWeight <= 0)
				{
					setupReturnPageWhenError(message: $"Gemstone {material.MaterialName} must has the quantity of stone greater than 0");
					return Page();
				}

				jewelryMaterials.Add(new JewelryMaterial { Material = material, JewelryWeight = gemstone.MaterialQuantWeight, Jewelry = jewelry});
				
			}
			foreach (var metal in Metals)
			{
				Material material = materialService.GetMaterialById(metal.MaterialId);
				if (material == null || !material.IsMetail)
				{
					setupReturnPageWhenError(message: $"Metal {material.MaterialName} is not found");
					return Page();
				}
				var list = Metals.Where(x => x.MaterialId == metal.MaterialId).ToList();
				if (list.Count > 1)
				{
					setupReturnPageWhenError(message: $"Metal {material.MaterialName} is duplicated");
					return Page();
				}
				if (metal.MaterialQuantWeight <= 0)
				{
					setupReturnPageWhenError(message: $"Metal {material.MaterialName} must has the weight of metal greater than 0");
					return Page();
				}
	
				jewelryMaterials.Add(new JewelryMaterial { Material = material, JewelryWeight = metal.MaterialQuantWeight, Jewelry = jewelry });
				
			}

			jewelry.JewelryMaterials = jewelryMaterials;

			if (jewelry.Quantity > 0 && jewelry.StatusSale.Equals(StatusSale.OUT_OF_STOCK))
			{
				setupReturnPageWhenError(message: "The quantity of product in stock is greater than zero, the sale status mus be instock");
				return Page();
			}
			try
			{
				jewerlryService.AddJewelry(jewelry);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.StackTrace);
				ModelState.AddModelError("Error", e.Message);
				setupReturnPageWhenError(message: e.Message);
				return Page();
			}
			this.UpdateSelectOptions();
			return RedirectToPage("./Index");
		}

		private void UpdateSelectOptions()
		{
			CategoryOptions = categoryService.GetCategories().Select(x => new SelectListItem { Value = x.CategoryName, Text = x.CategoryName }).ToList();
			SaleStatusOptions = Enum.GetValues<StatusSale>().Select(
							   x => new SelectListItem
							   {
								   Value = x.ToString(),
								   Text = StatusSaleExtension.GetDisplayName(x)
							   }).ToList();
			GemstoneOptions = materialService.GetMaterials().Where(x => !x.IsMetail).Select(x => new SelectListItem { Value = x.MaterialId.ToString(), Text = x.MaterialName }).ToList();
			MetalOptions = materialService.GetMaterials().Where(x => x.IsMetail).Select(x => new SelectListItem { Value = x.MaterialId.ToString(), Text = x.MaterialName }).ToList();

		}

		public ActionResult OnGetMetaDetail(int id)
		{
			var metal = materialService.GetMaterialById(id);
			if (metal == null || !metal.IsMetail)
			{
				return NotFound();
			}
			var result = new
			{
				id = metal.MaterialId,
				purity = metal.Purity,
				bidPrice = metal.BidPrice,
				offerPrice = metal.OfferPrice
			};
			return new JsonResult(result);
		}

		public ActionResult OnGetGemDetail(int id)
		{
			var gem = materialService.GetMaterialById(id);
			if (gem == null || gem.IsMetail)
			{
				return NotFound();
			}
			var result = new
			{
				id = gem.MaterialId,
				clarity = gem.Clarity,
				color = gem.Color,
				sharp = gem.Sharp,
				cost = gem.MaterialCost,
			};
			return new JsonResult(result);
		}

		private void setupReturnPageWhenError(string message)
		{
			if (!Metals.IsNullOrEmpty())
				this.MetalsJson = JsonConvert.SerializeObject(Metals);
			else
				this.MetalsJson = "[]";
			if (!Gemstones.IsNullOrEmpty())
				this.GemstonesJson = JsonConvert.SerializeObject(Gemstones);
			else
				this.GemstonesJson = "[]";

			Message = message;
			
		}

	}
}
