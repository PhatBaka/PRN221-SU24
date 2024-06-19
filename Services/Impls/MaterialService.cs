using AutoMapper;
using BusinessObjects;
using DTOs;
using Microsoft.AspNetCore.Http;
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
    public class MaterialService : IMaterialService
    {
        private readonly IMaterialRepository _materialRepository;
        private readonly IMapper _mapper;

        public MaterialService(IMaterialRepository materialRepository,
                                IMapper mapper)
        {
            _mapper = mapper;
            _materialRepository = materialRepository;
        }

        public async Task<MaterialDTO> GetMaterialById(int id)
        {
            try
            {
                return _mapper.Map<MaterialDTO>(_materialRepository.GetByIdAsync(id).Result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IList<MaterialDTO>> GetMaterials()
        {
            try
            {
                return _mapper.Map<IList<MaterialDTO>>(_materialRepository.GetAllAsync().Result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateMaterial(MaterialDTO materialDTO, IFormFile materialImage)
        {
            try
            {
                if (materialImage != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await materialImage.CopyToAsync(memoryStream);
                        materialDTO.MaterialImage = memoryStream.ToArray();
                    }
                }
                return await _materialRepository.UpdateAsync(_mapper.Map<Material>(materialDTO), materialDTO.MaterialId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
