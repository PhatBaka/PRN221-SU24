using BusinessObjects;
using DataAccessObjects.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects.Impls
{
    public class WarrantyRequestDAO : GenericDAO<WarrantyRequest>, IWarrantyRequestDAO
    {
        private readonly AppDBContext _context;

        public WarrantyRequestDAO(AppDBContext context) : base(context)
        {
            _context = context;
        }
    }
}
