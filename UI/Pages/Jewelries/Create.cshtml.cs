using AutoMapper;
using BusinessObjects;
using BusinessObjects.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Helpers;
using Services.Interfaces;
using UI.Helper;
using UI.Payload.JewelryPayload;
using UI.Payload.MaterialPayload;
using UI.Payload.MaterialPayload.GemPayload;
using static UI.Pages.Jewelries.CreateJewelryModel;

namespace UI.Pages.Jewelries
{
    public class CreateJewelryModel : PageModel
    {
        private readonly IJewelryService _jewelryService;
        private readonly IMaterialService _materialService;
        private readonly IMetalService _metalService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public string Message;

        public CreateJewelryModel(IJewelryService jewelryService,
                                IMaterialService materialService,
                                IMetalService metalService,
                                ICategoryService categoryService,
                                IMapper mapper)
        {
            _jewelryService = jewelryService;
            _metalService = metalService;
            _categoryService = categoryService;
            _materialService = materialService;
            _mapper = mapper;
        }

        [BindProperty]
        public CreateJewelryRequest? Jewelry { get; set; }

        public IList<GetMaterialRequest> Gems { get; set; }

        public IList<MetalItem> MetalCart { get; set; }
        public IList<MaterialItem> MaterialCart { get; set; }
        [BindProperty]
        public List<Category> JewelryOptions { get; set; }


        public class MetalItem
        {
            public int MaterialId { get; set; }
            public string? MetalName { get; set; }
            public double Weight { get; set; }
        }

        public class MaterialItem
        {
            public GetMaterialRequest Material { get; set; }
            public double Weight { get; set; }
        }

        public async void OnGet()
        {
            string role = HttpContext.Session.GetString("ROLE");
            if (role != "MANAGER")
            {
                RedirectToPage("/AccessDenied");
            }
            IList<GetMaterialRequest> metals = _mapper.Map<IList<GetMaterialRequest>>(_materialService.GetMaterials().Where(x => x.IsMetail == true));
            MetalCart ??= new List<MetalItem>();
            foreach (var metal in metals)
            {
                MetalItem metalItem = new()
                {
                    MaterialId = metal.MaterialId,
                    MetalName = metal.MaterialName,
                    Weight = 0.1
                };
                MetalCart.Add(metalItem);
            }
            HttpContext.Session.SetObjectAsJson("METALCART", MetalCart);
            JewelryOptions = _categoryService.GetCategories();
            Gems = _mapper.Map<IList<GetMaterialRequest>>(_materialService.GetGemsNotInJewelry());
            HttpContext.Session.SetObjectAsJson("GEMS", Gems);
        }

        private void LoadData()
        {
            JewelryOptions = _categoryService.GetCategories();
            LoadGem();

            LoadMetalCart();
            LoadMaterialCart();
        }

        private void LoadGem()
        {
            Gems ??= new List<GetMaterialRequest>();
            Gems = HttpContext.Session.GetObjectFromJson<IList<GetMaterialRequest>>("GEMS");
        }

        private void LoadMetalCart() => MetalCart = HttpContext.Session.GetObjectFromJson<IList<MetalItem>>("METALCART");

        public async Task<IActionResult> OnPostAddMetalToCart(int id)
        {
            LoadData();

            if (MetalCart != null && MetalCart.FirstOrDefault(x => x.MaterialId == id) != null)
            {
                return Page();
            }

            Material material = _materialService.GetMaterialById(id);

            MetalItem metalCart = new MetalItem()
            {
                MaterialId = id,
                MetalName = material.MaterialName,
                Weight = 0.1
            };

            MetalCart ??= new List<MetalItem>();
            MetalCart.Add(metalCart);

            HttpContext.Session.SetObjectAsJson("METALCART", MetalCart);

            return Page();
        }

        public IActionResult OnPostHandleMetalCart(int materialId, double weight, string action)
        {
            LoadData();

            var errorMessages = new List<string>();

            // Validate weight input
            if (weight <= 0)
            {
                errorMessages.Add("Weight must be greater than 0.");
            }

            if (errorMessages.Count > 0)
            {
                Message = string.Join(" ", errorMessages);
                foreach (var error in errorMessages)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
                return Page();
            }

            switch (action)
            {
                case "update":
                    MaterialCart ??= new List<MaterialItem>();

                    bool duplicate = false;

                    foreach (var metal in MaterialCart.Where(x => x.Material.IsMetail == true))
                    {
                        if (metal.Material.MaterialId == materialId)
                        {
                            metal.Weight = weight;
                            duplicate = true;
                            break;
                        }
                    }

                    if (!duplicate)
                    {
                        MaterialItem materialItem = new()
                        {
                            Material = _mapper.Map<GetMaterialRequest>(_materialService.GetMaterialById(materialId)),
                            Weight = weight
                        };
                        MaterialCart.Add(materialItem);
                    }
                    break;
            }

            HttpContext.Session.SetObjectAsJson("MATERIALCART", MaterialCart);

            return Page();
        }


        public IActionResult OnPostAddGemToCart(int MaterialId)
        {
            LoadData();

            MaterialCart ??= new List<MaterialItem>();

            if (MaterialCart != null && MaterialCart.Count > 0)
            {
                if (MaterialCart.FirstOrDefault(x => x.Material.IsMetail == false && x.Material.MaterialId == MaterialId) != null)
                {
                    return Page();
                }
            }

            GetMaterialRequest gem = _mapper.Map<GetMaterialRequest>(_materialService.GetMaterialById(MaterialId));

            MaterialItem materialItem = new()
            {
                Material = gem
            };

            MaterialCart.Add(materialItem);
            HttpContext.Session.SetObjectAsJson("MATERIALCART", MaterialCart);

            return Page();
        }

        public void LoadMaterialCart()
        {
            MaterialCart = HttpContext.Session.GetObjectFromJson<IList<MaterialItem>>("MATERIALCART");
        }

        public IActionResult OnPostCreateJewelry(int selectedCategory)
        {
            LoadData();

            IList<JewelryMaterial> jewelryMaterials = new List<JewelryMaterial>();
            double totalWeight = 0;
            List<string> errorMessages = new List<string>();

            // Check if MaterialCart is null or empty
            if (MaterialCart == null || MaterialCart.Count == 0)
            {
                errorMessages.Add("Material cart is empty or not initialized.");
            }

            // Validate other properties
            if (string.IsNullOrEmpty(Jewelry.JewelryName))
            {
                errorMessages.Add("Jewelry name is required.");
            }

            // Validate LaborPrice: check if it's provided and greater than zero
            if (Jewelry.LaborPrice <= 0)
            {
                if (Jewelry.LaborPrice == 0)
                {
                    errorMessages.Add("Labor price must be provided.");
                }
                else
                {
                    errorMessages.Add("Labor price must be greater than zero.");
                }
            }

            if (Jewelry.Quantity <= 0)
            {
                errorMessages.Add("Quantity must be greater than 0");
            }

            // Process the materials if no errors so far
            if (errorMessages.Count == 0)
            {
                foreach (var material in MaterialCart)
                {
                    totalWeight += material.Weight;
                    jewelryMaterials.Add(new JewelryMaterial
                    {
                        JewelryWeight = material.Weight,
                        MaterialId = material.Material.MaterialId
                    });
                }

                Jewelry jewelry = new Jewelry
                {
                    CategoryId = selectedCategory,
                    Description = Jewelry.Description,
                    JewelryImage = Util.ToByteArrayAsync(Jewelry.ImageDataFile).Result,
                    JewelryName = Jewelry.JewelryName,
                    JewelryMaterials = jewelryMaterials,
                    LaborPrice = Jewelry.LaborPrice,
                    Quantity = Jewelry.Quantity,
                    StatusSale = StatusSale.NEW,
                    TotalWeight = (decimal)totalWeight
                };

                var newJewelry = _jewelryService.AddJewelry(jewelry);
                if (newJewelry != null)
                {
                    HttpContext.Session.Remove("MATERIALCART");
                    return RedirectToPage("./Index");
                }

                errorMessages.Add("Failed to create jewelry. Please try again later.");
            }

            // Concatenate error messages into a single string
            Message = string.Join(" ", errorMessages);
            return Page();
        }


        public IActionResult OnPostRemoveFromMaterialCart(int materialId)
        {
            LoadData();
            foreach (var item in MaterialCart)
            {
                if (item.Material.MaterialId == materialId)
                {
                    MaterialCart.Remove(item);
                    HttpContext.Session.SetObjectAsJson("MATERIALCART", MaterialCart);
                    break;
                }
            }
            return Page();
        }
    }
}
