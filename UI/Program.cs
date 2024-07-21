using DataAccessObjects;
using UI.AppStarts;

var builder = WebApplication.CreateBuilder(args);
    
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.ConfigDI();
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
builder.Services.AddMvc();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapGet("/", async context =>
{
	context.Response.Redirect("/Login");
});
app.Run();
