using BusinessObjects;
using DataAccessObjects;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Impls
{
    public class PromotionService : IPromotionService
    {
        private readonly IGenericDAO<Promotion> _genericDAO;

        public PromotionService(IGenericDAO<Promotion> genericDAO)
        {
            _genericDAO = genericDAO;
        }

        public PromotionDetail GetPromotionDetailByJewleryId(int jewelryId)
        {
            try
            {
                foreach (var promotion in _genericDAO.GetAllAsync().Result.Where(x => x.StartDate <= DateTime.Now && x.EndDate >= DateTime.Now).ToList())
                {
                    foreach (var jewelry in promotion.PromotionDetails)
                    {
                        if (jewelryId == jewelry.JewelryId)
                        {
                            return jewelry;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return null;
        }

        public IList<Promotion> GetPromotions()
        {
            try
            {
                return _genericDAO.GetAllAsync().Result.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
