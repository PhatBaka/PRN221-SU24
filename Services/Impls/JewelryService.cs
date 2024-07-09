using AutoMapper;
using BusinessObjects;
using Castle.Core.Internal;
using DTOs;
using DTOs.Enums;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.Extensions.DependencyInjection;
using Repositories.Interfaces;
using Services.Helpers;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Services.Impls
{
    public class JewelryService : IJewelryService
    {
        private readonly IMaterialRepository _materialRepository;
        private readonly IJewelryRepository _jewelryRepository;
        private readonly IMapper _mapper;

        public JewelryService(IMaterialRepository materialRepository, IJewelryRepository jewelryRepository, IMapper mapper)
        {
            _materialRepository = materialRepository;
            _jewelryRepository = jewelryRepository;
            _mapper = mapper;
        }

        public async Task<GetJewelryDTO> CreateJewelry(JewelryDTO jewelryDTO, IList<GetMaterialDTO> materialCart, IList<GetPriceDTO> priceDTOs)
        {
            try
            {
                Jewelry entity = _mapper.Map<Jewelry>(jewelryDTO);

                decimal gemWeight = 0;
                decimal metalWeight = 0;

                decimal sellGemPrice = 0;
                decimal buyGemPrice = 0;

                decimal sellMetalPrice = 0;
                decimal buyMetalPrice = 0;

                foreach (var material in materialCart)
                {
                    // add metal to database
                    if (material.IsMetal)
                    {
                        var materialPrice = priceDTOs.FirstOrDefault(x => x.Metal.ToUpper().Equals(material.Name));
                        Material metal = new Material()
                        {
                            AskPrice = materialPrice.Rate.Ask,
                            BidPrice = materialPrice.Rate.Bid,
                            Name = material.Name.ToUpper(),
                            BuyPrice = materialPrice.Rate.Bid * material.Weight,
                            SellPrice = materialPrice.Rate.Ask * material.Weight,
                            IsMetal = true,
                            CreatedDate = DateTime.Now,
                            Weight = material.Weight
                        };

                        metalWeight += material.Weight;
                        sellMetalPrice += metal.SellPrice;
                        buyMetalPrice += metal.BuyPrice;

                        Material newMetal = await _materialRepository.AddAsync(metal);

                        entity.Materials.Add(newMetal);
                    }
                    else
                    {
                        Material existedGem = await _materialRepository.GetByIdAsync(material.MaterialId);
                        gemWeight += existedGem.Weight;
                        buyGemPrice += existedGem.BuyPrice;
                        sellGemPrice += existedGem.SellPrice;
                        entity.Materials.Add(existedGem);
                    }
                }

                entity.Status = ObjectStatusEnum.ACTIVE.ToString();
                entity.CreatedDate = DateTime.Now;

                entity.TotalWeight = gemWeight + metalWeight;
                entity.TotalGemWeight = gemWeight;
                entity.TotalMetalWeight = metalWeight;
                
                entity.TotalSellGemPrice = sellGemPrice;
                entity.TotalBuyGemPrice = buyGemPrice;

                entity.TotalBuyMetalPrice = buyMetalPrice;
                entity.TotalSellMetalPrice = sellGemPrice;

                entity.JewelryImageData = await ImageHelper.ConvertToByteArrayAsync(jewelryDTO.JewelryImageFile);

                var newJewelry = await _jewelryRepository.AddAsync(entity);
                return _mapper.Map<GetJewelryDTO>(newJewelry);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<IList<GetJewelryDTO>> GetJewelries()
        {
            throw new NotImplementedException();
        }
    }
}