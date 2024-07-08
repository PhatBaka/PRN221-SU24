using AutoMapper;
using BusinessObjects;
using DTOs;
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
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public AccountService(IAccountRepository accountRepository,
                                IMapper mapper)
        {
            _mapper = mapper;
            _accountRepository = accountRepository;
        }

        public async Task<GetAccountDTO> GetAccount(string email, string password)
        {
            try
            {
                var account = await _accountRepository.GetFirstOrDefaultAsync(x => x.Email.Equals(email) && x.Password.Equals(password));
                return _mapper.Map<GetAccountDTO>(account);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
