using DataAccessObjects;
using DataAccessObjects.Helpers;
using System.Reflection;
using UI.AppStarts;

var builder = WebApplication.CreateBuilder(args);
    
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.ConfigDI();
builder.Services.AddAutoMapper(typeof(MapperConfig).Assembly);
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapRazorPages();

app.Run();
