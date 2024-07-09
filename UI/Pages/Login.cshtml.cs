using DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using UI.Helper;

namespace UI.Pages
{
	public class LoginModel : PageModel
	{
		[BindProperty]
		public LoginRequest? Account { get; set; }

		public void OnGet()
		{
			IList<GetPriceDTO> MetalDTOs = new List<GetPriceDTO>();
			var options = new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			};
			var gold = JsonSerializer.Deserialize<GetPriceDTO>(Utils.ReadJsonFile("gold.json"), options);
			gold.ImagePath = "/img/gold.jpg";

            var silver = JsonSerializer.Deserialize<GetPriceDTO>(Utils.ReadJsonFile("silver.json"), options);
			silver.ImagePath = "/img/silver.jpg";

			var palladium = JsonSerializer.Deserialize<GetPriceDTO>(Utils.ReadJsonFile("palladium.json"), options);
			palladium.ImagePath = "/img/palladium.jpg";

			if (gold != null && silver != null && palladium != null)
			{
				MetalDTOs.Add(gold);
				MetalDTOs.Add(silver);
				MetalDTOs.Add(palladium);
			}
			HttpContext.Session.SetObjectAsJson("METALLIST", MetalDTOs);
		}

		public async Task<IActionResult> OnPostAsync()
		{
			return Page();
		}
	}

	public class LoginRequest
	{
		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage = "Email is invalid")]
		public string? Email { get; set; }

		[Required(ErrorMessage = "Password is required")]
		public string? Password { get; set; }
	}
}
