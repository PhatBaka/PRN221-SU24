using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IPromotionService
    {
        public IList<Promotion> GetPromotions();
        public PromotionDetail GetPromotionDetailByJewleryId(int jewelryId);
    }
}
