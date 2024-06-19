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
using Newtonsoft.Json;
using DTOs;
using Services.Impls;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using DTOs.Enums;
using UI.Helpers;

namespace UI.Pages.Jewelries
{
    public class CreateModel : PageModel
    {
        private readonly IJewelryService _jewelryService;
        private readonly IMaterialService _materialService;
        private readonly IMapper _mapper;

        public CreateModel(IJewelryService jewelryService, 
                            IMaterialService materialService,
                            IMapper mapper)
        {
            _jewelryService = jewelryService;
            _materialService = materialService;
            _mapper = mapper;
        }

        public string CurrentFilter { get; set; }
        [BindProperty]
        public Jewelry Jewelry { get; set; } = default!;
        [BindProperty]
        public PaginatedList<MaterialViewModel> Materials { get; set; }
        public List<MaterialViewModel> SelectedMaterials
        {
            get
            {
                var sessionValue = HttpContext.Session.GetString("SelectedMaterial");
                return sessionValue == null ? new List<MaterialViewModel>() : JsonConvert.DeserializeObject<List<MaterialViewModel>>(sessionValue);
            }
            set
            {
                HttpContext.Session.SetString("SelectedMaterial", JsonConvert.SerializeObject(value));
            }
        }

        public string Message { get; private set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostCreateJewelry()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            Jewelry.ObjectStatus = ObjectStatusEnum.ACTIVE.ToString();
            Jewelry.Category = CategoryEnum.RING.ToString();

            var id = await _jewelryService.CreateJewelry(Jewelry, SelectedMaterials);
            
            return RedirectToPage("./Index");
        }

        public async Task OnGet(int? pageIndex, string searchString, string currentFilter)
        {
            await LoadData(pageIndex, searchString, currentFilter);
        }

        private async Task LoadData(int? pageIndex = null, string? searchString = null, string? currentFilter = null)
        {
            var materialIQs = _materialService.GetMaterials().Result.AsQueryable();
            if (searchString != null)
                pageIndex = 1;
            else
                searchString = currentFilter;

            CurrentFilter = searchString;
            if (!String.IsNullOrEmpty(searchString))
                materialIQs = materialIQs.Where(s => s.MaterialName.Contains(searchString));
            var pageSize = 18;

            var materials = _mapper.ProjectTo<MaterialViewModel>(materialIQs);
            Materials = PaginatedList<MaterialViewModel>.Create(
                materials.AsNoTracking(), pageIndex ?? 1, pageSize);
        }

        public async Task<IActionResult> OnPostAddMaterialToSession(int? materialId)
        {
            await LoadData();
            var materiral = Materials.Find(t => t.MaterialId == materialId);
            if (materiral != null)
            {
                var selectedMaterial = SelectedMaterials;
                if (!selectedMaterial.Any(t => t.MaterialId == materialId)) // Avoid duplicate entries
                {
                    selectedMaterial.Add(materiral);
                    SelectedMaterials = selectedMaterial;
                    Message = "Add tag successfully";
                }
                else
                {
                    Message = "This tag alreary in the storage";
                }
            }
            return Page();
        }

        public async Task<IActionResult> OnPostRemoveMaterialFromSession(int? materialId)
        {
            var selectedMaterial = SelectedMaterials.ToList();
            var materialToRemove = selectedMaterial.SingleOrDefault(x => x.MaterialId == materialId);
            if (materialToRemove != null)
            {
                selectedMaterial.Remove(materialToRemove);
                SelectedMaterials = selectedMaterial;
                Message = "Remove material successfully";
            }
            await LoadData();
            return Page();
        }
    }
}
