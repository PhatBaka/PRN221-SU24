using BusinessObjects;
using DTOs;
using DTOs.Carts;
using DTOs.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Interfaces;
using UI.Helper;

namespace UI.Pages.Jewelries
{
    public class CreateModel : PageModel
    {
        private readonly IJewelryService _jewelryService;
        private readonly IGemService _gemService;
        private readonly IMetalService _metalService;

        public CreateModel(IJewelryService jewelryService, 
                                IGemService gemService,
                                IMetalService metalService)
        {
            _jewelryService = jewelryService;
            _gemService = gemService;
            _metalService = metalService;
            JewelryOptions = Enum.GetValues(typeof(JewelryCategoryEnum))
                     .Cast<JewelryCategoryEnum>()
                     .Select(e => new SelectListItem
                     {
                         Value = e.ToString(),
                         Text = e.ToString()
                     }).ToList();
        }

        [BindProperty]
        public JewelryDTO? Jewelry { get; set; }
        public IList<GetPriceDTO> PriceDTOS { get; set; }
        public IList<GetGemDTO> GemDTOs { get; set; }
        public IList<MetalCart> MetalCarts { get; set; }
        public IList<GetMaterialDTO> MaterialCarts { get; set; }
        public DateTime UpdatedDate { get; set; }
        public List<SelectListItem>? JewelryOptions { get; set; }

        public async void OnGet()
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            LoadMetalPrice();
            LoadMetalCart();
            LoadMaterialCart();
            await LoadGem();
        }

        private void LoadMetalPrice()
        {
            PriceDTOS ??= new List<GetPriceDTO>();
            PriceDTOS = HttpContext.Session.GetObjectFromJson<IList<GetPriceDTO>>("METALLIST");
            UpdatedDate = PriceDTOS.FirstOrDefault(x => x.Metal == "gold").Timestamp;
        } 

        private async Task LoadGem()
        {
            GemDTOs ??= new List<GetGemDTO>();
            GemDTOs = await _gemService.GetGems();
        }

        private void LoadMetalCart() => MetalCarts = HttpContext.Session.GetObjectFromJson<IList<MetalCart>>("METALCART");

        public async Task<IActionResult> OnPostAddMetalToCart(string metalName)
        {
            await LoadData();
            
            MetalCarts ??= new List<MetalCart>();

            var metal = PriceDTOS.FirstOrDefault(x => x.Metal.Equals(metalName));
            // Check metal exist in the cart
            if (MetalCarts != null && MetalCarts.FirstOrDefault(x => x.Name.Equals(metal.Metal)) != null)
            {
                return Page();
            }

            MetalCart metalCart = new MetalCart()
            {
                Name = metal.Metal,
                CurrentPrice = metal.Rate.Bid,
                ImagePath = metal.ImagePath
            };

            MetalCarts.Add(metalCart);
            HttpContext.Session.SetObjectAsJson("METALCART", MetalCarts);

            return Page();
        }

        public async Task<IActionResult> OnPostHandleMetalCart(String metalName, decimal weight, string action)
        {
            await LoadData();

            switch (action)
            {
                case "update":
                    MaterialCarts ??= new List<GetMaterialDTO>();
                    if (MaterialCarts != null && MaterialCarts.Count > 0)
                    {
                        if (MaterialCarts.FirstOrDefault(x => x.IsMetal == true) != null)
                        {
                            var metal = MaterialCarts.FirstOrDefault(x => x.Name.Equals(metalName.ToUpper()));
                            if (metal != null)
                            {
                                metal.Weight = weight;
                                metal.SellPrice = metal.CurrentPrice * weight;
                                return Page();
                            }
                        }
                    }

                    decimal currentPrice = PriceDTOS.FirstOrDefault(x => x.Metal.Equals(metalName)).Rate.Ask;
                    GetMaterialDTO materialDTO = new GetMaterialDTO()
                    {
                        Name = metalName.ToUpper(),
                        CurrentPrice = currentPrice,
                        Weight = weight,
                        CreatedDate = DateTime.Now,
                        SellPrice = currentPrice * weight,
                        IsMetal = true
                    };

                    MaterialCarts.Add(materialDTO);
                    HttpContext.Session.SetObjectAsJson("MATERIALCART", MaterialCarts);

                    break;
                    
                case "remove":
                    var metalToRemove = MetalCarts.FirstOrDefault(x => x.Name.Equals(metalName));
                    MetalCarts.Remove(metalToRemove);
                    HttpContext.Session.SetObjectAsJson("METALCART", MetalCarts);
                    break;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAddGemToCart(Guid MaterialId)
        {
            await LoadData();

            MaterialCarts ??= new List<GetMaterialDTO>();

            if(MaterialCarts != null && MaterialCarts.Count > 0)
            {
                if (MaterialCarts.FirstOrDefault(x => x.IsMetal == false && x.MaterialId == MaterialId) != null)
                {
                    return Page();
                }
            }

            GetGemDTO gem = await _gemService.GetGemById(MaterialId);

            GetMaterialDTO materialDTO = new GetMaterialDTO()
            {
                CertificateCode = gem.CertificateCode,
                SellPrice = gem.SellPrice,
                MaterialImageData = gem.MaterialImageData,
                Name = gem.Name,
                MaterialId = gem.MaterialId
            };

            MaterialCarts.Add(materialDTO);
            HttpContext.Session.SetObjectAsJson("MATERIALCART", MaterialCarts);

            return Page();
        }

        public void LoadMaterialCart()
        {
            MaterialCarts = HttpContext.Session.GetObjectFromJson<IList<GetMaterialDTO>>("MATERIALCART");
        }

        public async Task<IActionResult> OnPostCreateJewelry()
        {
            await LoadData();

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if(await _jewelryService.CreateJewelry(Jewelry, MaterialCarts, PriceDTOS) != null)
            {
                HttpContext.Session.Remove("MATERIALCART");
                HttpContext.Session.Remove("METALCART");
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}
