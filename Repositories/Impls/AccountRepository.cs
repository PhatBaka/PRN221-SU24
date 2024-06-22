using BusinessObjects;
using DataAccessObjects;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;

namespace Repositories.Impls
{
	public class AccountRepository : GenericRepository<Account>, IAccountRepository
	{
		private readonly IGenericDAO<Account> _accountGenericDAO;

		public AccountRepository(IGenericDAO<Account> accountGenericDAO) : base(accountGenericDAO)
		{
			_accountGenericDAO = accountGenericDAO;
		}

		public Account GetAccountByEmailAndPassword(string email, string password)
		{
			return _accountGenericDAO.FirstOrDefaultAsync(x => x.Email == email && x.Password == password).Result;
		}
	}
}
