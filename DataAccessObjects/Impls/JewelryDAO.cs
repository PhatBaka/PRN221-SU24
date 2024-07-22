using BusinessObjects;
using DataAccessObjects.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects.Impls
{
    public class JewelryDAO : GenericDAO<Jewelry>, IJewelryDAO
    {
        private readonly AppDBContext _context;

        public JewelryDAO(AppDBContext context) : base(context)
        {
            _context = context;
        }

        public IList<Jewelry> GetJewelries()
        {
            try
            {
                return _context.Jewelries
                                .Include(x => x.Warranties)
                                .Include(x => x.JewelryMaterials)
                                    .ThenInclude(x => x.Material)
                                .Include(x => x.Category)
                                .Include(x => x.OrderDetails)
                                    .ThenInclude(x => x.Order)
                                .Include(x => x.PromotionDetails)
                                    .ThenInclude(x => x.Promotion)
                                        .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
