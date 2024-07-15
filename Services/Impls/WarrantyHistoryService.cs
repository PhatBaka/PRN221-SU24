using BusinessObjects;
using BusinessObjects.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repositories;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Impls
{
    public class WarrantyHistoryService : IWarrantyHistoryService
    {

        private IGenericRepository<Warranty> _warrantyRepo;
        private IOrderService _orderService;
        private IGenericRepository<WarrantyHistory> _warrantyHistoryRepo;
        private IJewelryService _jewelryService;
        private IWarrantyService _warrantyService;


        public WarrantyHistoryService(IServiceProvider service)
        {
            _warrantyRepo = service.GetRequiredService<IGenericRepository<Warranty>>();
            _orderService = service.GetRequiredService<IOrderService>();
            _warrantyHistoryRepo = service.GetRequiredService<IGenericRepository<WarrantyHistory>>();
            _jewelryService = service.GetRequiredService<IJewelryService>();
            _warrantyService = service.GetRequiredService<IWarrantyService>();
        }
        public void AddWarrantyHistory(WarrantyHistory warrantyHistory)
        {
            Warranty warranty = _warrantyService.GetWarrantyById(warrantyHistory.WarrantyId);

            if (warranty == null)
            {
                throw new Exception("Can not create new warranty fix request because warranty id is not found");
            }
            if (warranty.WarrantyStatus == WarrantyStatus.INACTIVE)
            {
                throw new Exception($"Can not create new warranty fix request because warranty of this jewelry id {warranty.JewelryId} - name {warranty.Jewelry.JewelryName} is has not been activated");
            }
            if (warranty.WarrantyStatus == WarrantyStatus.CANCELLED)
            {
                throw new Exception($"Can not create new warranty fix request because warranty of this jewelry id {warranty.JewelryId} - name {warranty.Jewelry.JewelryName} is already cancelled");
            }
            WarrantyHistory warrantyHistories = _warrantyHistoryRepo.GetAllAsync().Result.Where(w => w.WarrantyId == warranty.WarrantyId && w.status == WarrantyFixStatus.PROCESSING).FirstOrDefault();
            if(warrantyHistories != null)
            {
                throw new Exception($"Can not create new warranty fix request because warranty of this jewelry id {warranty.JewelryId} - name {warranty.Jewelry.JewelryName} is already in processing has not been successful returned");
            }
            warrantyHistory.DateCreated = DateTime.Now;
            warrantyHistory.DateModified = DateTime.Now;
            bool isExpired = IsDateWithinWarrantyPeriod(warrantyHistory.DateCreated, warranty.ActiveDate, warranty.EndDate);
            if(!isExpired)
            {
                throw new Exception($"Can not create new warranty fix request because warranty of this jewelry id {warranty.JewelryId} - name {warranty.Jewelry.JewelryName} has expired");
            }
			else if (warrantyHistory.ReturnDate < warrantyHistory.ReceivedDate)
			{
				throw new Exception("Return Date date must be greater or equan Received Date");
			}
			try
            {
                bool success = _warrantyHistoryRepo.InsertAsync(warrantyHistory).Result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteWarrantyHistory(int warrantyHistoryId)
        {
            WarrantyHistory warrantyHistory = _warrantyHistoryRepo.GetByIdAsync(warrantyHistoryId).Result;
            if (warrantyHistory == null)
            {
                throw new Exception("Can not delete warranty history because warranty history id is not found");
            }
            else
            {
                try
                {
                    bool success = _warrantyHistoryRepo.DeleteAsync(warrantyHistory).Result;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public List<WarrantyHistory> GetAllWarrantyHistory()
        {
            return _warrantyHistoryRepo.GetAllAsync().Result.ToList();
        }

        public List<WarrantyHistory> GetAllWarrantyHistoryByWarrantyId(int warrantyId)
        {
            Warranty warranty = _warrantyService.GetWarrantyById(warrantyId);
            if (warranty == null)
            {
                throw new Exception("Can not get warranty history because warranty id is not found");
            }
            return _warrantyHistoryRepo.GetAllAsync().Result.Where(w => w.WarrantyId == warrantyId).ToList();
        }

        public WarrantyHistory GetWarrantyHistoryById(int warrantyHistoryId)
        {
            WarrantyHistory warrantyHistory = _warrantyHistoryRepo.GetByIdAsync(warrantyHistoryId).Result;
            return warrantyHistory;
        }

        public WarrantyHistory UpdateWarrantyHistory(WarrantyHistory warrantyHistory)
        {
			WarrantyHistory warrantyHistoryOld = _warrantyHistoryRepo.GetAllAsync().Result.AsNoTracking().FirstOrDefault(w => w.WarrantyHistoryId == warrantyHistory.WarrantyHistoryId);

			if (warrantyHistoryOld == null)
            {
                throw new Exception("Can not update warranty history because warranty history id is not found");
            }

            if (warrantyHistoryOld.status == WarrantyFixStatus.SUCCESS_RETURNED)
            {
                throw new Exception("Can not update warranty history because warranty history is already success returned");
            }
            if (warrantyHistoryOld.status == WarrantyFixStatus.CANCELLED)
            {
                throw new Exception("Can not update warranty history because warranty history is already cancelled");
            }
			if (warrantyHistory.ReturnDate < warrantyHistory.ReceivedDate)
			{
				throw new Exception("Return Date date must be greater or equan Received Date");
			}
			try
            {
				warrantyHistory.DateCreated = warrantyHistoryOld.DateCreated;
				warrantyHistory.DateModified = DateTime.Now;
				//bool success = _warrantyHistoryRepo.UpdateByIdAsync(warrantyHistory, warrantyHistoryOld.WarrantyId).Result;
				bool success = _warrantyHistoryRepo.UpdateAsync(warrantyHistory).Result;

			}
			catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            WarrantyHistory warrantyHistorySaved = _warrantyHistoryRepo.GetAllAsync().Result.FirstOrDefault(w => w.WarrantyHistoryId == warrantyHistory.WarrantyHistoryId);

            return warrantyHistorySaved;
        }

        public WarrantyHistory UpdateWarrantyHistoryStatus(int warrantyHistoryId, WarrantyFixStatus status)
        {
            WarrantyHistory warrantyHistory = _warrantyHistoryRepo.GetByIdAsync(warrantyHistoryId).Result;
			if (warrantyHistory == null)
            {
                throw new Exception("Can not update warranty history status because warranty history id is not found");
            }
            try
            {
                warrantyHistory.status = status;
				warrantyHistory.DateCreated = warrantyHistory.DateCreated;
				warrantyHistory.DateModified = DateTime.Now;
				bool success = _warrantyHistoryRepo.UpdateAsync(warrantyHistory).Result;
			}
			catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            WarrantyHistory warrantyHistorySaved = _warrantyHistoryRepo.GetAllAsync().Result.FirstOrDefault(w => w.WarrantyHistoryId == warrantyHistoryId);
            return warrantyHistorySaved;
        }

        public bool IsDateWithinWarrantyPeriod(DateTime dateCreated, DateTime warrantyActivateDate, DateTime warrantyEndDate)
        {
            return dateCreated >= warrantyActivateDate && dateCreated <= warrantyEndDate;
        }


    }
}
