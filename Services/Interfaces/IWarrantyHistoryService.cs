using BusinessObjects;
using BusinessObjects.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
	public interface IWarrantyHistoryService
	{
		WarrantyHistory GetWarrantyHistoryById(int warrantyHistoryId);
        List<WarrantyHistory> GetAllWarrantyHistoryByWarrantyId(int warrantyId);
		List<WarrantyHistory> GetAllWarrantyHistory();
		void AddWarrantyHistory(WarrantyHistory warrantyHistory);
		WarrantyHistory UpdateWarrantyHistory(WarrantyHistory warrantyHistory);
		WarrantyHistory UpdateWarrantyHistoryStatus(int warrantyHistoryId, WarrantyFixStatus status);
		public bool IsDateWithinWarrantyPeriod(DateTime dateCreated, DateTime warrantyActivateDate, DateTime warrantyEndDate);
		void DeleteWarrantyHistory(int warrantyHistoryId);

	}
}
