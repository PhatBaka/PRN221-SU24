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
    }
}
