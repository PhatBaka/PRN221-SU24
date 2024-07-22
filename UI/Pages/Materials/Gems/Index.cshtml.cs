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
using Services.Helpers;
using UI.Payload.MaterialPayload.GemPayload;
using AutoMapper;

namespace UI.Pages.Materials.Gems
{
    public class IndexModel : PageModel
    {
        private readonly IMaterialService _materialService;
        private readonly IMapper _mapper;

        public IndexModel(IMaterialService materialService, IMapper mapper)
        {
            _materialService = materialService;
            _mapper = mapper;
        }

        public PaginatedList<GetGemRequest> Gems { get; set; }
        public string? CurrentFilter { get; set; }

        public async Task<IActionResult> OnGetAsync(string currentFilter,
                                        string searchString,
                                        int? pageIndex)
        {
            string role = HttpContext.Session.GetString("ROLE");
            if (role != "MANAGER")
            {
                return RedirectToPage("/AccessDenied");
            }
            if (searchString != null)
                pageIndex = 1;
            else
                searchString = currentFilter;

            CurrentFilter = searchString;

            IList<Material> materials = _materialService.GetMaterials().Where(x => x.IsMetail == false).ToList();

            IQueryable<GetGemRequest> gemsIQ = _mapper.Map<IList<GetGemRequest>>(materials).AsQueryable();

            Gems = PaginatedList<GetGemRequest>.Create(
                gemsIQ.AsNoTracking(), pageIndex ?? 1, 5);
            return Page();
        }
    }
}