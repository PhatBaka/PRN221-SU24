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
using AutoMapper;
using BusinessObjects.Enums;
using UI.Payload.WarrantyPayload;

namespace UI.Pages.Warranties
{
    public class CreateModel : PageModel
    {
        private readonly IWarrantyService warrantySerivce;
        private readonly IJewelryService jewelryService;
        private IMapper mapper;

        public CreateModel(IServiceProvider service)
        {
            jewelryService = service.GetRequiredService<IJewelryService>();
            warrantySerivce = service.GetRequiredService<IWarrantyService>();
            mapper = service.GetRequiredService<IMapper>();
        }


		//public SelectList jewelrySelectList { get; set; }
		public SelectList timeEnumSelectList { get; set; }
        [BindProperty(SupportsGet = true)]
        public int? OrderId { get; set; }
        [BindProperty]
		public List<WarrantyCreateRequest> WarrantyRequests { get; set; } = new List<WarrantyCreateRequest>();
		private List<Warranty> Warranties { get; set; } = default!;
		public string Message { get; set; }


		public IActionResult OnGet()
        {
            try
            {
                Console.WriteLine("ORDER ID" + OrderId);
                if (OrderId == null)
                {
                    Message = "Enter order ID to query the items that can be in warranty";
                }
                else
                {
                    
                    List<Warranty> warranties = warrantySerivce.GetJewelriesInWarrantyByOrderId(OrderId.Value);
                    if (warranties == null || warranties.Count == 0)
                    {
                        Message = "There are no existing items in orders that qualify for warranty";
                    }
                    else
                    {
                        if (WarrantyRequests.Count > 0)
                        {
                            WarrantyRequests.Clear();
                        }
                        foreach (var warranty in warranties)
                        {
                            Console.WriteLine(warranty.JewelryId);
                            ViewData[$"JewelryName_{warranty.JewelryId}"] = warranty.Jewelry.JewelryName;
                            WarrantyRequests.Add(mapper.Map<WarrantyCreateRequest>(warranty));
                        }
                    }
                }
                SetUpPreData();
                return Page();
            }
            catch(Exception ex)
            {
                Message = ex.Message;
                SetUpPreData();
                return Page();
            }
          
         
        }

        public IActionResult OnGetJewelryBeWarrantyActive()
        {
			SetUpPreData();
			return Page();
		}

        private void SetUpPreData()
        {
			var timeEnumValues = Enum.GetValues(typeof(TimeEnum)).Cast<TimeEnum>().Select(e => new { Value = e.ToString(), Text = e.ToString() }).ToList() ;
			timeEnumSelectList = new SelectList(timeEnumValues, "Value", "Text"); 
		}


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return OnGet();
            }

            try
            {
                if(WarrantyRequests == null || WarrantyRequests.Count == 0)
                {
					ModelState.AddModelError("Error", "Empty warranty item to active");
					return Page();
				}
                else
                {
                    foreach (var warrantyRequest in WarrantyRequests)
                    {
                        Warranty warranty = mapper.Map<Warranty>(warrantyRequest);
                        if(WarrantyStatus.INACTIVE == warranty.WarrantyStatus)
                        {
                           await warrantySerivce.AddWarrantyAsync(warranty);
                        }
                        
                    }
                }
            }catch(Exception ex)
            {
				ModelState.AddModelError("Error", ex.Message);
                Message = ex.Message;
				return OnGet();
			}

            return RedirectToPage("./Index");
        }
    }
}
