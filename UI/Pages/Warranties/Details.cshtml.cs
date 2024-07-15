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

namespace UI.Pages.Warranties
{
    public class DetailsModel : PageModel
    {

        private readonly IWarrantyService warrantySerivce;
        private readonly IWarrantyHistoryService warrantyHistoryService;
        private readonly IJewelryService jewelryService;
        private IMapper mapper;

        public DetailsModel(IServiceProvider service)
        {
            jewelryService = service.GetRequiredService<IJewelryService>();
            warrantySerivce = service.GetRequiredService<IWarrantyService>();
            warrantyHistoryService = service.GetRequiredService<IWarrantyHistoryService>();
            mapper = service.GetRequiredService<IMapper>();
        }

        public Warranty Warranty { get; set; } = default!;
        public List<WarrantyHistory> WarrantyHistory { get; set; } = default!;
        public string Message { get; set; }

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
                WarrantyHistory = warrantyHistoryService.GetAllWarrantyHistoryByWarrantyId(id.Value);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                return Page();
            }
            return Page();
        }
    }
}
