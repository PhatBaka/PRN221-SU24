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

        [BindProperty]
        public CreateGemRequest? Gem { get; set; }
        public List<SelectListItem>? ClarityOptions { get; set; }
        public List<SelectListItem>? GemOptions { get; set; }
        public List<SelectListItem>? SharpOptions { get; set; }
        public List<SelectListItem>? ColorOptions { get; set; }
        public string Message { get; set; }

        public IActionResult OnGet()
        {
            string role = HttpContext.Session.GetString("ROLE");
            if (role == "ADMIN")
            {
                RedirectToPage("/AccessDenied");
            }
            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            string role = HttpContext.Session.GetString("ROLE");
            if (role != "MANAGER")
            {
                return RedirectToPage("/AccessDenied");
            }

            var validationErrors = new List<string>();

            // Manual validation logic
            if (string.IsNullOrWhiteSpace(Gem.MaterialName))
            {
                validationErrors.Add("Name is required");
            }

            if (Gem.MaterialCost <= 0)
            {
                validationErrors.Add("Cost must be greater than zero");
            }

            if (!Enum.IsDefined(typeof(ClarityEnum), Gem.Clarity))
            {
                validationErrors.Add("Invalid clarity value");
            }

            if (Gem.Purity < 0 || Gem.Purity > 100)
            {
                validationErrors.Add("Purity must be between 0 and 100");
            }

            if (string.IsNullOrWhiteSpace(Gem.Color))
            {
                validationErrors.Add("Color is required");
            }

            if (string.IsNullOrWhiteSpace(Gem.Sharp))
            {
                validationErrors.Add("Sharp is required");
            }

            if (Gem.MaterialImageData == null)
            {
                validationErrors.Add("Material image is required");
            }

            if (Gem.GemCertificateData == null)
            {
                validationErrors.Add("Gem certificate is required");
            }

            // Check for validation errors
            if (validationErrors.Count > 0)
            {
                Message = string.Join("; ", validationErrors);
                return Page();
            }

            var gem = _mapper.Map<Material>(Gem);
            gem.MaterialImage = await ImageHelper.ConvertToByteArrayAsync(Gem.MaterialImageData);
            gem.GemCertificate = await ImageHelper.ConvertToByteArrayAsync(Gem.GemCertificateData);

            var result = _materialService.AddMaterial(gem);
            if (result == null)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while adding the material.");
                return Page();
            }

            return RedirectToPage("./Index");
        }

    }
}
