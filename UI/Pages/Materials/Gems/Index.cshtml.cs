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

namespace UI.Pages.Materials.Gems
{
    public class IndexModel : PageModel
    {
        private readonly IMaterialService _materialService;

        public IndexModel(IMaterialService materialService)
        {
            _materialService = materialService;
        }

        public IList<Material> Material { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Material = _materialService.GetMaterials().Where(x => x.IsMetail == false).ToList();
        }
    }
}
