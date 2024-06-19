using AutoMapper;
using BusinessObjects;
using DTOs;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Impls
{
    public class JewelryService : IJewelryService
    {
        private readonly IJewelryRepository _jewelryRepository;
        private readonly IMapper _mapper;

        public JewelryService(IJewelryRepository jewelryRepository, IMapper mapper)
        {
            _jewelryRepository = jewelryRepository;
            _mapper = mapper;
        }

        public async Task<IList<JewelryDTO>> GetJewelries()
        {
            try
            {
                return _mapper.Map<IList<JewelryDTO>>(_jewelryRepository.GetAllAsync().Result.ToList());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<object> CreateJewelry(Jewelry jewelry, IList<MaterialViewModel> materials)
        {
            try
            {
                var id = await _jewelryRepository.AddAsync(jewelry);
                if (await _jewelryRepository.AddMaterialIntoJewelry(jewelry, materials))
                {
                    return id;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
