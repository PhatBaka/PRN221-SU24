using DataAccessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Pages
{
    public class DashBoardModel : PageModel
    {
        private readonly AppDBContext _context;

        public DashBoardModel(AppDBContext context)
        {
            _context = context;
        }

        public List<CategoryPriceData> CategoryDataList { get; set; }
        public List<OrderPriceData> OrderDataList { get; set; }

        public class OrderPriceData
        {
            public int OrderId { get; set; }
            public double TotalPrice { get; set; }
        }
        public class CategoryPriceData
        {
            public string CategoryName { get; set; }
            public double TotalPrice { get; set; }
        }
        public double FullPrice = 0;
        public async Task<IActionResult> OnGetAsync()
        {
			string role = HttpContext.Session.GetString("ROLE") ?? "";
			if (role != "ADMIN")
			{
				return RedirectToPage("/AccessDenied");
			}
			CategoryDataList = await _context.Jewelries
                .Join(_context.Categories, j => j.CategoryId, c => c.CategoryId, (j, c) => new { j, c })
                .Join(_context.JewelryMaterials, jc => jc.j.JewelryId, jm => jm.JewelryId, (jc, jm) => new { jc.j, jc.c, jm })
                .Join(_context.Materials, jcm => jcm.jm.MaterialId, m => m.MaterialId, (jcm, m) => new
                {
                    jcm.j,
                    jcm.c,
                    jcm.jm,
                    m
                })
                .GroupBy(x => x.c.CategoryName)
                .Select(g => new CategoryPriceData
                {
                    CategoryName = g.Key,
                    TotalPrice = g.Sum(x => (double)x.j.LaborPrice + (x.jm.JewelryWeight * x.m.MaterialCost))
                })
                .ToListAsync();

                OrderDataList = await _context.Orders
                .Join(_context.OrderDetails, o => o.OrderId, od => od.OrderId, (o, od) => new { o, od })
                .Join(_context.Jewelries, o_od => o_od.od.JewelryId, j => j.JewelryId, (o_od, j) => new
                {
                    o_od.o.OrderId,
                    o_od.od.Quantity,
                    o_od.od.UnitPrice,
                    o_od.od.DiscountPercent
                })
                .GroupBy(x => x.OrderId)
                .Select(g => new OrderPriceData
                {
                    OrderId = g.Key,
                    TotalPrice = g.Sum(x => x.Quantity * x.UnitPrice * (1 - x.DiscountPercent / 100))
                })
                .ToListAsync();

            OrderDataList.ForEach(g => FullPrice += g.TotalPrice);

            return Page();
        }
    }
}
