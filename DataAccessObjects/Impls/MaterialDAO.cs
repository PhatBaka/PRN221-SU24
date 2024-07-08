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
    public class MaterialDAO : GenericDAO<Material>, IMaterialDAO
    {
        private readonly AppDBContext _context;

        public MaterialDAO(AppDBContext context) : base(context)
        {
            _context = context;
        }
    }
}
