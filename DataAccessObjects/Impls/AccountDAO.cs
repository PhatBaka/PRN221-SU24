using BusinessObjects;
using DataAccessObjects.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects.Impls
{
    public class AccountDAO : GenericDAO<Account>, IAccountDAO
    {
        private readonly AppDBContext _context;

        public AccountDAO(AppDBContext context) : base(context)
        {
            _context = context;
        }

        public Account GetAccountByEmailAndPassword(string email, string password)
        {
            try
            {
                return _context.Accounts.FirstOrDefault(x => x.Email == email 
                                                            && x.Password == password);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
