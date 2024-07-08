using BusinessObjects;
using Castle.Core.Internal;
using DTOs;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.Extensions.DependencyInjection;
using Repositories.Interfaces;
using Services.Helpers;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Services.Impls
{
    public class JewelryService : IJewelryService
    {
        public Task<bool> CreateJewelry(JewelryDTO jewelryDTO)
        {
            throw new NotImplementedException();
        }

        public Task<IList<GetJewelryDTO>> GetJewelries()
        {
            throw new NotImplementedException();
        }
    }
}