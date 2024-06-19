using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using DataAccessObjects;
using Services.Interfaces;
using DTOs;
using System.Configuration;
using Microsoft.Data.SqlClient;
using UI.Helpers;

namespace UI.Pages
{
    public class PriceModel : PageModel
    {
        public string NameSort { get; set; }
        public string CurrentFilter { get; set; }

        private readonly IMaterialService _materialService;

        public PriceModel(IMaterialService materialService)
        {
            _materialService = materialService;
        }

        public PaginatedList<MaterialDTO> Material { get; set; } = default!;

        public async Task OnGetAsync(int? pageIndex, string searchString, string currentFilter)
        {
            var materialIQs = _materialService.GetMaterials().Result.AsQueryable();
            if (searchString != null)
                pageIndex = 1;
            else
                searchString = currentFilter;

            CurrentFilter = searchString;
            if (!String.IsNullOrEmpty(searchString))
                materialIQs = materialIQs.Where(s => s.MaterialName.Contains(searchString));
            var pageSize = 10;
            Material = PaginatedList<MaterialDTO>.Create(
                materialIQs.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
