﻿using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IAccountRepository : IGenericRepository<Account>
    {
        public Account GetAccountByEmailAndPassword(string email, string password);
    }
}
