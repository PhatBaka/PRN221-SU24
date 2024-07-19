using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessObjects;
using Repositories.Interfaces;
using Services.Interfaces;

namespace Services
{
    public class PromotionService : IPromotionService
    {
        private readonly IPromotionRepository _promotionRepository;

        public PromotionService(IPromotionRepository promotionRepository)
        {
            _promotionRepository = promotionRepository;
        }

        public async Task<IList<Promotion>> GetAllPromotionsAsync()
        {
            return await _promotionRepository.GetAllPromotionsAsync();
        }

        public async Task<Promotion> GetPromotionByIdAsync(int id)
        {
            return await _promotionRepository.GetPromotionByIdAsync(id);
        }

        public async Task AddPromotionAsync(Promotion promotion)
        {
            await _promotionRepository.AddPromotionAsync(promotion);
        }

        public async Task UpdatePromotionAsync(Promotion promotion)
        {
            await _promotionRepository.UpdatePromotionAsync(promotion);
        }

        public async Task DeletePromotionAsync(int id)
        {
            await _promotionRepository.DeletePromotionAsync(id);
        }
    }
}
