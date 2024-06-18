using BusinessObjects;
using DataAccessObjects.Impls;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Impls
{
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
    }
}
