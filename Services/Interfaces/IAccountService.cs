using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IAccountService
    {
        public Account GetAccount(string email, string password);
        public List<Account> GetAccounts();
        public Account CreateAccount(Account acocunt);
    }
}
