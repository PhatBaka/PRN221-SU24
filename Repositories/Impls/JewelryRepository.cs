using BusinessObjects;
using DataAccessObjects;
using DataAccessObjects.Interfaces;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Impls
{
    public class JewelryRepository : GenericRepository<Jewelry>, IJewelryRepository
    {
        private readonly IJewelryDAO _jewelryDAO;

        public JewelryRepository(IGenericDAO<Jewelry> dao, IJewelryDAO jewelryDAO) : base(dao)
        {
            _jewelryDAO = jewelryDAO;
        }

        public IList<Jewelry> GetJewelries()
        {
            try
            {
                return _jewelryDAO.GetJewelries();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
