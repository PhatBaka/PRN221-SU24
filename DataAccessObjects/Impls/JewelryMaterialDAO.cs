using BusinessObjects;
using DataAccessObjects.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects.Impls
{
    public class JewelryMaterialDAO : BaseDAO<JewelryMaterial>, IJewelryMaterialDAO
    {
        private readonly AppDBContext _context;

        public JewelryMaterialDAO(AppDBContext context) : base(context)
        {
            _context = context;
        }
    }
}
