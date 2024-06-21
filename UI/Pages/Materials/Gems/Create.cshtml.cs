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
using UI.Payload.MaterialPayload.GemPayload;
using BusinessObjects.Enums;
using Services.Helpers;

namespace UI.Pages.Materials.Gems
{
    public class CreateModel : PageModel
    {
        private readonly IMaterialService _materialService;
        private readonly IMapper _mapper;

        public CreateModel(IMaterialService materialService,
                            IMapper mapper)
        {
            _materialService = materialService;
            _mapper = mapper;

            ClarityOptions = Enum.GetValues(typeof(ClarityEnum))
                     .Cast<ClarityEnum>()
                     .Select(e => new SelectListItem
                     {
                         Value = e.ToString(),
                         Text = e.ToString()
                     }).ToList();
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public CreateGemRequest Gem { get; set; } = default!;
        public List<SelectListItem> ClarityOptions { get; set; }
        public string Message { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var gem = _mapper.Map<Material>(Gem);
            gem.MaterialImage = await ImageHelper.ConvertToByteArrayAsync(Gem.MaterialImageData);
            gem.GemCertificate = await ImageHelper.ConvertToByteArrayAsync(Gem.GemCertificateData);
            if (_materialService.AddMaterial(gem) != null)
            {

            }

            return RedirectToPage("./Index");
        }
    }
}
