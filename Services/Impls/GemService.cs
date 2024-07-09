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
    public class GemService : IGemService
    {
        private readonly IMaterialRepository _materialRepository;
        private readonly IMapper _mapper;

        public GemService(IMaterialRepository materialRepository,
                            IMapper mapper)
        {
            _materialRepository = materialRepository;
            _mapper = mapper;
        }

        public async Task<GemDTO> CreateGem(GemDTO gemDTO)
        {
            try
            {
                var entity = _mapper.Map<Material>(gemDTO);
                entity.BuyPrice = gemDTO.SellPrice * 70 / 100;
                entity.CertificateImageData = await ImageHelper.ConvertToByteArrayAsync(gemDTO.CertificateImageFile);
                entity.MaterialImageData = await ImageHelper.ConvertToByteArrayAsync(gemDTO.MaterialImageFile);
                entity.CreatedDate = DateTime.Now;
                return _mapper.Map<GemDTO>(await _materialRepository.AddAsync(entity));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<GetGemDTO> GetGemById(Guid id)
        {
            try
            {
                var entity = await _materialRepository.GetByIdAsync(id);
                return _mapper.Map<GetGemDTO>(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IList<GetGemDTO>> GetGems()
        {
            try
            {
                var entities = _materialRepository.GetAllAsync().Result.ToList().Where(x => x.IsMetal == false);
                return _mapper.Map<IList<GetGemDTO>>(entities);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
