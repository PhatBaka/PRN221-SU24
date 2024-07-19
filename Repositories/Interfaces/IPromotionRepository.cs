using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessObjects;

namespace Repositories.Interfaces
{
    public interface IPromotionRepository
    {
        Task<IList<Promotion>> GetAllPromotionsAsync();
        Task<Promotion> GetPromotionByIdAsync(int id);
        Task AddPromotionAsync(Promotion promotion);
        Task UpdatePromotionAsync(Promotion promotion);
        Task DeletePromotionAsync(int id);
    }
}
