using BusinessObjects;
using DataAccessObjects.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects.Impls
{
    public class WarrantyHistoryDAO : BaseDAO<WarrantyRequest>, IWarrantyHistoryDAO
    {
        private readonly AppDBContext _context;

        public WarrantyHistoryDAO(AppDBContext context) : base(context)
        {
            _context = context;
        }
    }
}
