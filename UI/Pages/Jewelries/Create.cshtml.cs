﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects;
using DataAccessObjects;

namespace UI.Pages.Jewelries
{
    public class CreateModel : PageModel
    {
        private readonly DataAccessObjects.AppDBContext _context;

        public CreateModel(DataAccessObjects.AppDBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryName");
            return Page();
        }

        [BindProperty]
        public Jewelry Jewelry { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Jewelry == null || Jewelry == null)
            {
                return Page();
            }

            _context.Jewelry.Add(Jewelry);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
