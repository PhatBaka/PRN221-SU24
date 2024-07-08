using DTOs;
using DTOs.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Services.Interfaces;

namespace UI.Pages.Gems
{
    public class CreateModel : PageModel
    {
        private readonly IGemService _gemService;

        public CreateModel(IGemService gemService)
        {
            _gemService = gemService;
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
            CutOptions = Enum.GetValues(typeof(CutEnum))
                     .Cast<CutEnum>()
                     .Select(e => new SelectListItem
                     {
                         Value = e.ToString(),
                         Text = e.ToString()
                     }).ToList();
            ShapeOptions = Enum.GetValues(typeof(ShapeEnum))
                     .Cast<ShapeEnum>()
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

        [BindProperty]
        public GemDTO? Gem { get; set; }
        public List<SelectListItem>? ClarityOptions { get; set; }
        public List<SelectListItem>? GemOptions { get; set; }
        public List<SelectListItem>? CutOptions { get; set; }
        public List<SelectListItem>? ShapeOptions { get; set; }
        public List<SelectListItem>? ColorOptions { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (await _gemService.CreateGem(Gem))
            {
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
