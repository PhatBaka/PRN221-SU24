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
    public class CustomerDAO : GenericDAO<Customer>, ICustomerDAO
    {
        private AppDBContext _context;

        public CustomerDAO(AppDBContext context) : base(context)
        {
            _context = context;
        }
    }
}
