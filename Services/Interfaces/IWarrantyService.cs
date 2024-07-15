using BusinessObjects;
using BusinessObjects.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
	public interface IWarrantyService
	{
		Warranty GetWarrantyById(int warrantyId);
        List<Warranty> GetJewelriesInWarrantyByOrderId(int orderId);
		List<Warranty> GetAllWarrantys();
		Task<Warranty> AddWarrantyAsync(Warranty warranty);
		Task<Warranty> UpdateWarrantyAsync(Warranty warranty);
		Task<Warranty> UpdateWarrantyStatusAsync(Warranty warranty, WarrantyStatus status);
        void DeleteWarranty(int warrantyId);
	}
}
