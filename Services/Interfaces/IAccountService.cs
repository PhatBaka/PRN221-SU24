using BusinessObjects;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IAccountService
    {
        public Task<GetAccountDTO> GetAccount(string email, string password);
        public Task<IList<GetAccountDTO>> GetAccounts();
        public Task<bool> CreateAccount(AccountDTO accountDTO);
        public Task<bool> UpdateAccount(AccountDTO accountDTO, Guid id);
        public Task<bool> DeleteAccount(Guid id);
    }
}
