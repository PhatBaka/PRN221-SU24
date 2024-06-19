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
using Services.Interfaces;
using DTOs;
using AutoMapper;

namespace UI.Pages.Materials
{
    public class EditModel : PageModel
    {
        private readonly IMaterialService _materialService;
        private readonly IMapper _mapper;

        public EditModel(IMaterialService materialService,
                            IMapper mapper)
        {
            _mapper = mapper;
            _materialService = materialService;
        }

        [BindProperty]
        public MaterialDTO Material { get; set; } = default!;
        public IFormFile? ImageData { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var material = await _materialService.GetMaterialById((int) id);
            if (material == null)
            {
                return NotFound();
            }
            Material = material;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var material = await _materialService.GetMaterialById(Material.MaterialId);

                if(await _materialService.UpdateMaterial(_mapper.Map(material, Material), ImageData))
                {

                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaterialExists(Material.MaterialId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool MaterialExists(int id)
        {
            return _materialService.GetMaterialById(id).Result != null;
        }
    }
}
