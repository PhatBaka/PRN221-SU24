using BusinessObjects;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IJewelryRepository : IBaseRepository<Jewelry>
    {
        public Task<bool> AddMaterialIntoJewelry(Jewelry jewelry, IList<MaterialViewModel> materials);
    }
}
