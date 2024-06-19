using BusinessObjects;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IJewelryService
    {
        public Task<IList<JewelryDTO>> GetJewelries();
        public Task<object> CreateJewelry(Jewelry jewelry, IList<MaterialViewModel> materials);
    }
}
