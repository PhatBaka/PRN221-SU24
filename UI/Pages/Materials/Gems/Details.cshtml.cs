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
using UI.Payload.MaterialPayload.GemPayload;
using AutoMapper;
using BusinessObjects.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.StaticFiles.Infrastructure;

namespace UI.Pages.Materials.Gems
{
    public class DetailsModel : PageModel
    {
        private readonly IMaterialService _materialService;
        private readonly IMapper _mapper;

        public DetailsModel(IMaterialService materialService, IMapper mapper)
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
            GemOptions = Enum.GetValues(typeof(GemTypeEnum))
                     .Cast<GemTypeEnum>()
                     .Select(e => new SelectListItem
                     {
                         Value = e.ToString(),
                         Text = e.ToString()
                     }).ToList();
            SharpOptions = Enum.GetValues(typeof(SharpEnum))
                     .Cast<SharpEnum>()
                     .Select(e => new SelectListItem
                     {
                         Value = e.ToString(),
                         Text = e.ToString()
                     }).ToList();
            ColorOptions = Enum.GetValues(typeof(ColorEnum))
                     .Cast<ColorEnum>()
                     .Select(e => new SelectListItem
                     {
                         Value = e.ToString(),
                         Text = e.ToString()
                     }).ToList();
        }
        public List<SelectListItem>? ClarityOptions { get; set; }
        public List<SelectListItem>? GemOptions { get; set; }
        public List<SelectListItem>? SharpOptions { get; set; }
        public List<SelectListItem>? ColorOptions { get; set; }
        public GetGemRequest Gem { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var material = _materialService.GetMaterialById((int)id);
            if (material == null)
            {
                return NotFound();
            }
            else
            {
                Gem = _mapper.Map<GetGemRequest>(material);
            }
            return Page();
        }
    }
}
