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
        public Task<IList<GetJewelryDTO>> GetJewelries();
        public Task<bool> CreateJewelry(JewelryDTO jewelryDTO);
    }
}
