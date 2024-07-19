using BusinessObjects;
using Repositories;
using Repositories.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Impls
{
    public class AccountService : IAccountService
    {
        private readonly IGenericRepository<Account> _genericAccountRepository;
        private readonly IAccountRepository _accountRepository;

        public AccountService(IGenericRepository<Account> genericAccountRepository,
                                IAccountRepository accountRepository)
        {
            _genericAccountRepository = genericAccountRepository;
            _accountRepository = accountRepository;
        }

        public Account GetAccount(string email, string password)
        {
            try
            {
                return _accountRepository.GetAccountByEmailAndPassword(email, password);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Account> GetAccounts()
        {
            try
            {
                return _accountRepository.GetAllAsync().Result.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
