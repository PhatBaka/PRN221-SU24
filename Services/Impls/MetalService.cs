using AutoMapper;
using BusinessObjects;
using DTOs;
using DTOs.Enums;
using Repositories.Interfaces;
using Services.Helpers;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Impls
{
    public class MetalService : IMetalService
    {
        private readonly IMaterialRepository _materialRepository;
        private readonly IMapper _mapper;

        public MetalService(IMaterialRepository materialRepository,
                                IMapper mapper)
        {
            _materialRepository = materialRepository;
            _mapper = mapper;
        }

        public async Task<MetalDTO> CreateMetal(MetalDTO metalDTO)
        {
            try
            {
                var entity = _mapper.Map<Material>(metalDTO);
                entity.CreatedDate = DateTime.Now;
                return _mapper.Map<MetalDTO>(await _materialRepository.AddAsync(entity));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
