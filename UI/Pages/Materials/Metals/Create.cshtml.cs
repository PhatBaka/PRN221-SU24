//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using BusinessObjects;
//using DataAccessObjects;
//using Services.Interfaces;
//using AutoMapper;
//using UI.Payload.MaterialPayload.MetalPayload;

//namespace UI.Pages.Materials.Metals
//{
//    public class CreateModel : PageModel
//    {
//        private readonly IMaterialService _materialService;
//        private readonly IMapper _mapper;

//        public CreateModel(IMaterialService materialService,
//                            IMapper mapper)
//        {
//            _materialService = materialService;
//            _mapper = mapper;
//        }

//        public IActionResult OnGet()
//        {
//            return Page();
//        }
        //public IActionResult OnGet()
        //{
        //    string role = HttpContext.Session.GetString("ROLE");
        //    if (role != "MANAGER")
        //    {
        //        return RedirectToPage("/AccessDenied");
        //    }
        //    return Page();
        //}

//        [BindProperty]
//        public CreateMetalRequest Metal { get; set; } = default!;
//        public string Message { get; set; }

//        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
//        public async Task<IActionResult> OnPostAsync()
//        {
//          if (!ModelState.IsValid || Metal == null)
//            {
//                return Page();
//            }

//            Material entity = _mapper.Map<Material>(Metal);
//            entity.IsMetail = true;
//            _materialService.AddMaterial(entity);

//            return RedirectToPage("./Index");
//        }
//    }
//}
