using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UI.Pages
{
	public class IndexModel : PageModel
	{
		public string? role { get; private set; }
		public int? userId { get; private set; }
		public string? fullname { get; private set; }
		public string? email { get; private set; }
		public string? isAuthenticated { get; private set; }

		public IActionResult OnGet()
		{
			// Retrieve session values
			role = HttpContext.Session.GetString("ROLE");
			userId = HttpContext.Session.GetInt32("ID");
			fullname = HttpContext.Session.GetString("FULLNAME");
			email = HttpContext.Session.GetString("EMAIL");
			isAuthenticated = HttpContext.Session.GetString("ISAUTHENTICATED");

			// Check if isAuthenticated is null or not equal to "true" (case-insensitive)
			if (string.IsNullOrWhiteSpace(isAuthenticated) || !isAuthenticated.Equals("true", StringComparison.OrdinalIgnoreCase))
			{
				// Redirect to the Login page
				return RedirectToPage("/Login");
			}

			// If authenticated, continue with the OnGet logic
			return Page();
		}
	}
}
