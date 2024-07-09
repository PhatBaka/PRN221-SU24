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
                // Map JewelryDTO to Jewelry entity
                Jewelry entity = _mapper.Map<Jewelry>(jewelryDTO);

                decimal gemWeight = 0;
                decimal metalWeight = 0;

                decimal sellGemPrice = 0;
                decimal buyGemPrice = 0;

                decimal sellMetalPrice = 0;
                decimal buyMetalPrice = 0;

                foreach (var material in materialCart)
                {
                    Material materialEntity = null;

                    if (material.IsMetal)
                    {
                        // Check if the material already exists
                        materialEntity = await _materialRepository.GetByIdAsync(material.MaterialId);
                        if (materialEntity == null)
                        {
                            // Fetch the price for the metal
                            var materialPrice = priceDTOs.FirstOrDefault(x => x.Metal.ToUpper().Equals(material.Name));
                            if (materialPrice == null)
                                throw new Exception($"Price information for metal '{material.Name}' is missing.");

                            // Create new material if it does not exist
                            materialEntity = new Material
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

                            await _materialRepository.AddAsync(materialEntity);
                        }

                        // Create a new JewelryMaterial entry
                        var jewelryMaterial = new JewelryMaterial
                        {
                            Jewelry = entity,
                            Material = materialEntity,
                        };

                        entity.JewelryMaterials.Add(jewelryMaterial);

                        // Update pricing and weight
                        metalWeight += materialEntity.Weight;
                        sellMetalPrice += materialEntity.SellPrice;
                        buyMetalPrice += materialEntity.BuyPrice;
                    }
                    else
                    {
                        // Get existing gem material
                        materialEntity = await _materialRepository.GetByIdAsync(material.MaterialId);
                        if (materialEntity != null)
                        {
                            gemWeight += materialEntity.Weight;
                            buyGemPrice += materialEntity.BuyPrice;
                            sellGemPrice += materialEntity.SellPrice;

                            // Create a new JewelryMaterial entry
                            var jewelryMaterial = new JewelryMaterial
                            {
                                Jewelry = entity,
                                Material = materialEntity,
                            };

                            entity.JewelryMaterials.Add(jewelryMaterial);
                        }
                        else
                        {
                            throw new Exception($"Material with ID {material.MaterialId} does not exist.");
                        }
                    }
                }

                // Set Jewelry properties
                entity.Status = ObjectStatusEnum.ACTIVE.ToString();
                entity.CreatedDate = DateTime.Now;

                entity.TotalWeight = gemWeight + metalWeight;
                entity.TotalGemWeight = gemWeight;
                entity.TotalMetalWeight = metalWeight;

                entity.TotalSellGemPrice = sellGemPrice;
                entity.TotalBuyGemPrice = buyGemPrice;

                entity.TotalBuyMetalPrice = buyMetalPrice;
                entity.TotalSellMetalPrice = sellMetalPrice;

                entity.TotalBuyMaterialPrice = buyGemPrice + buyMetalPrice;
                entity.TotalSellMaterialPrice = sellMetalPrice + sellGemPrice;

                entity.BuyJewelryPrice = entity.TotalBuyMaterialPrice +  (decimal) jewelryDTO.ManufacturingFees;
                entity.SellJewelryPrice = entity.TotalSellMaterialPrice + (decimal) jewelryDTO.ManufacturingFees;

                entity.JewelryImageData = await ImageHelper.ConvertToByteArrayAsync(jewelryDTO.JewelryImageFile);

                // Add the Jewelry entity to the repository
                var newJewelry = await _jewelryRepository.AddAsync(entity);

                return _mapper.Map<GetJewelryDTO>(newJewelry);
            }
            catch (Exception ex)
            {
                // Log the exception or rethrow with more details
                throw new Exception("An error occurred while creating jewelry.", ex);
            }
        }

        public Task<IList<GetJewelryDTO>> GetJewelries()
        {
            throw new NotImplementedException();
        }
    }
}