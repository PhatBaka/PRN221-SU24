using AutoMapper;
using BusinessObjects;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects.Helpers
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Account, AccountDTO>().ReverseMap();

            CreateMap<Jewelry, JewelryDTO>().ReverseMap();

            CreateMap<Material, MaterialDTO>().ReverseMap();
            CreateMap<MaterialDTO, MaterialViewModel>().ReverseMap();
        }
    }
}
