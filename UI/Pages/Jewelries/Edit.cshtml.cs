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
using UI.Payload.JewelryPayload;
using BusinessObjects.Enums;
using Castle.Core.Internal;
using Services.Helpers;

namespace UI.Pages.Jewelries
{
    public class EditModel : PageModel
    {
        private readonly IJewelryService jewerlryService;
        private readonly ICategoryService categoryService;
        private readonly IMapper mapper;

        public List<SelectListItem> SaleStatusOptions { get; set; }
        public List<SelectListItem> CategoryOptions { get; set; }
        public string Message { get; set; }

        public EditModel(IServiceProvider service)
        {
            jewerlryService = service.GetRequiredService<IJewelryService>();
            categoryService = service.GetRequiredService<ICategoryService>();
            mapper = service.GetRequiredService<IMapper>();
            this.UpdateSelectOptions();
        }

        [BindProperty]
        public CreateJewelryRequest Jewelry { get; set; }
        public byte[] ImageData;

        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jewelry =  jewerlryService.GetJewelryById(id.GetValueOrDefault());
            if (jewelry == null)
            {
                return NotFound();
            }
            Jewelry = mapper.Map<CreateJewelryRequest>(jewelry);
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            
            if (!ModelState.IsValid || Jewelry == null)
            {
                return Page();
            }
            int jewelryIdToUpdate = (int) Jewelry.JewelryId;
            if(jewelryIdToUpdate == null || jewerlryService.GetJewelryById(jewelryIdToUpdate) == null)
            {
                return NotFound();
            }
            
            Jewelry oldJewerry = jewerlryService.GetJewelryById(jewelryIdToUpdate);
            if (!Jewelry.ImageDataFile.IsNullOrEmpty() && Jewelry.ImageDataFile.Any())
            {
                try
                {
                    List<byte[]> imageDataBytes = ImageHelper.FormatImageFile(Jewelry.ImageDataFile);
                    if (imageDataBytes.Count > 0)
                    {
                        ImageData = imageDataBytes.FirstOrDefault();
                    }
                }
                catch (Exception e)
                {
                    Message = e.Message;
                    return Page();
                }
            }
            else
            {
                ImageData = oldJewerry.JewelryImage;
            }

            Jewelry jewelry = mapper.Map<Jewelry>(Jewelry);
            jewelry.JewelryImage = ImageData;

            Category category = categoryService.GetCategoryByName(Jewelry.CategoryName);
            if (category == null)
            {
                category = new Category { CategoryName = Jewelry.CategoryName };
            }
            jewelry.Category = category;

            try
            {
                jewerlryService.UpdateJewelry(jewelry);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                ModelState.AddModelError("Error", e.Message);
                Message = e.Message;
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
        }
    }
}
