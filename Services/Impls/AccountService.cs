using AutoMapper;
using BusinessObjects;
using DTOs;
using Repositories.Interfaces;
using Services.Interfaces;
using System;
using System.Collections;
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

        public async Task<bool> CreateAccount(AccountDTO accountDTO)
        {
            try
            {
                var entity = _mapper.Map<Account>(accountDTO);
                return await _accountRepository.AddAsync(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteAccount(Guid id)
        {
            try
            {
                var entity = await _accountRepository.GetByIdAsync(id);
                if (entity != null)
                {
                    return await _accountRepository.DeleteAsync(entity);
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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

        public async Task<IList<GetAccountDTO>> GetAccounts()
        {
            try
            {
                var entities = _accountRepository.GetAllAsync().Result.ToList();
                return _mapper.Map<IList<GetAccountDTO>>(entities);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateAccount(AccountDTO accountDTO, Guid id)
        {
            try
            {
                var entity = await _accountRepository.GetByIdAsync(id);
                if (entity != null)
                {
                    _mapper.Map(entity, accountDTO);
                    await _accountRepository.UpdateAsync(entity);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
