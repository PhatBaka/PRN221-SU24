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
        public Task<GetJewelryDTO> GetJewelryById(Guid id);
        public Task<IList<GetJewelryDTO>> GetJewelries();
        public Task<GetJewelryDTO> CreateJewelry(JewelryDTO jewelryDTO, IList<GetMaterialDTO> materialCart, IList<GetPriceDTO> priceDTOs);
    }
}
