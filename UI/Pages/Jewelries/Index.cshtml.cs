using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using DataAccessObjects;
using Services.Interfaces;
using DTOs;

namespace UI.Pages.Jewelries
{
    public class IndexModel : PageModel
    {
        private readonly IJewelryService _jewelrySerivice;

        public IndexModel(IJewelryService jewelrySerivice)
        {
            _jewelrySerivice = jewelrySerivice;
        }

        public IList<JewelryDTO> Jewelry { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Jewelry = await _jewelrySerivice.GetJewelries();
        }
    }
}
