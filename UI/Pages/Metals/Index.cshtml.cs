using DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Helpers;
using System.Text.Json;
using UI.Helper;

namespace UI.Pages.Metals
{
    public class IndexModel : PageModel
    {
        public IList<GetPriceDTO> MetalDTOs = new List<GetPriceDTO>();
        private IConfiguration Configuration { get; set; }
        public DateTime UpdatedDate { get; set; }

        public IndexModel(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void OnGetAsync()
        {
            MetalDTOs = HttpContext.Session.GetObjectFromJson<IList<GetPriceDTO>>("METALLIST");
            UpdatedDate = MetalDTOs.FirstOrDefault(x => x.Metal == "gold").Timestamp;
        }
    }
}
