using BusinessObjects;
using DataAccessObjects.Interfaces;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;

namespace Repositories.Impls
{
    public class AccountRepository : GenericRepository<Account>, IAccountRepository
	{
	}
}
