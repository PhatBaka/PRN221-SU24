using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UI.Pages
{
    public class LogoutModel : PageModel
    {
        public void OnGet()
        {
			
			HttpContext.Session.Clear();
			Response.Headers["Cache-Control"] = "no-store";
			Response.Headers["Pragma"] = "no-cache";
			Response.Headers["Expires"] = "0";
			Response.Redirect("/Login");
        }

        public IActionResult OnPost()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("Login");
        }
    }
}
