using BusinessObjects;
using BusinessObjects.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Repositories;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Impls
{
    public class WarrantyService : IWarrantyService
    {
        private IGenericRepository<Warranty> _warrantyRepo;
        private IOrderService _orderService;
        private IGenericRepository<WarrantyHistory> _warrantyHistoryRepo;
        private IJewelryService _jewelryService;


        public WarrantyService(IServiceProvider service)
        {
            _warrantyRepo = service.GetRequiredService<IGenericRepository<Warranty>>();
            _orderService = service.GetRequiredService<IOrderService>();
            _warrantyHistoryRepo = service.GetRequiredService<IGenericRepository<WarrantyHistory>>();
            _jewelryService = service.GetRequiredService<IJewelryService>();
        }
		
        public async Task<Warranty> AddWarrantyAsync(Warranty warranty)
		{
			// Validate order existence
			var order = await _orderService.GetOrderByIdAsync(warranty.OrderId);
			if (order == null)
			{
				throw new Exception($"Cannot create warranty for jewelry id {warranty.JewelryId} in order id {warranty.OrderId} because order not found");
			}

			// Validate jewelry existence
			var jewelry = _jewelryService.GetJewelryById(warranty.JewelryId);
			if (jewelry == null)
			{
				throw new Exception($"Cannot create warranty for jewelry id {warranty.JewelryId} in order id {warranty.OrderId} because jewelry not found");
			}

			// Validate jewelry presence in order
			var orderDetail = order.OrderDetails.FirstOrDefault(item => item.JewelryId == warranty.JewelryId);
			if (orderDetail == null)
			{
				throw new Exception($"Cannot create warranty for jewelry id {warranty.JewelryId} in order id {warranty.OrderId} because jewelry not found in order");
			}

			// Check for existing warranty
			var existedWarranty = (await _warrantyRepo.GetAllAsync())
				.FirstOrDefault(w => w.JewelryId == jewelry.JewelryId && w.OrderId == order.OrderId);
			if (existedWarranty != null)
			{
				return existedWarranty;
			}

			// Validate warranty period
			ValidatedPeriodWarranty(warranty, out bool success, out string errorMessage);
			if (!success)
			{
				throw new Exception($"Error creating warranty for jewelry id {warranty.JewelryId} in order id {warranty.OrderId}: {errorMessage}");
			}
			if (warranty.EndDate < warranty.ActiveDate)
			{
				throw new Exception("End date must be greater than or equal to the active date");
			}

			// Set warranty status
			warranty.WarrantyStatus = WarrantyStatus.ACTIVATED;

			try
			{
				// Save warranty
				bool successSaveWarranty = await _warrantyRepo.InsertAsync(warranty);
				if (!successSaveWarranty)
				{
					throw new Exception("Failed to save warranty");
				}

				// Verify and return saved warranty
				var warrantySuccessSaved = (await _warrantyRepo.GetAllAsync())
					.FirstOrDefault(w => w.OrderId == warranty.OrderId && w.JewelryId == warranty.JewelryId);
				return warrantySuccessSaved;
			}
			catch (Exception ex)
			{
				throw new Exception($"Error creating warranty for jewelry id {warranty.JewelryId} in order id {warranty.OrderId}: {ex.Message}");
			}
		}


		public void DeleteWarranty(int warrantyId)
        {
            try
            {
                Warranty warranty = _warrantyRepo.GetByIdAsync(warrantyId).Result;
                if (warranty == null)
                {
                    throw new Exception("Cannot delete warranty because warranty not found");
                }
                bool successDeleteWarranty = _warrantyRepo.DeleteAsync(warranty).Result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public List<Warranty> GetAllWarrantys()
        {
            return _warrantyRepo.GetAllAsync().Result.ToList();
        }

        public List<Warranty> GetJewelriesInWarrantyByOrderId(int orderId)
        {
            Order order = _orderService.GetOrderByIdAsync(orderId).Result;
            if (order == null)
            {
                throw new Exception("Cannot get warranty because order not found");
            }
            List<OrderDetail> orderDetails = order.OrderDetails.ToList();
            if (orderDetails == null || orderDetails.Count == 0)
            {
                throw new Exception("Cannot get warranty because order details is empty");
            }
            List<Jewelry> jewelriesInOrderAllowWarranty = orderDetails.Select(item => item.Jewelry).ToList();
            List<Warranty> warranties = new List<Warranty>();
            List<Warranty> existedWarraties = _warrantyRepo.GetAllAsync().Result.ToList();
            foreach (Jewelry jewelry in jewelriesInOrderAllowWarranty)
            {
                WarrantyStatus warrantyStatus = WarrantyStatus.INACTIVE;
                DateTime activeDate = order.OrderDate;
                DateTime endDate = DateTime.Now;
                TimeEnum periodUnitMeasure = TimeEnum.Day;
                double warrantyPeriod = 0;
                Warranty existedWarranty = existedWarraties.FirstOrDefault(warranty => warranty.JewelryId == jewelry.JewelryId && warranty.OrderId == order.OrderId);
                if (existedWarranty != null)
                {
                    warrantyStatus = existedWarranty.WarrantyStatus;
                    activeDate = existedWarranty.ActiveDate;
                    endDate = existedWarranty.EndDate;
                    periodUnitMeasure = existedWarranty.PeriodUnitmeasure;
                    warrantyPeriod = existedWarranty.WarrantyPeriod;
                }
                warranties.Add(new Warranty()
                {
                    Jewelry = jewelry,
                    JewelryId = jewelry.JewelryId,
                    Order = order,
                    OrderId = order.OrderId,
                    WarrantyStatus = warrantyStatus,
                    ActiveDate = activeDate,
                    EndDate = endDate,
                    PeriodUnitmeasure = periodUnitMeasure,
                    WarrantyPeriod = warrantyPeriod
                });
            }

            return warranties;
        }

        public Warranty GetWarrantyById(int warrantyId)
        {
            return _warrantyRepo.GetByIdAsync(warrantyId).Result;
        }

        public async Task<Warranty> UpdateWarrantyAsync(Warranty warranty)
        {
            Warranty warrantyExisted = await _warrantyRepo.GetByIdAsync(warranty.WarrantyId);
            ValidateWarrantyBeForeUpdate(warranty);
            if (warrantyExisted.WarrantyStatus != WarrantyStatus.ACTIVATED)
            {
                throw new Exception($"Cannot update warranty because this warranty has been in status {warrantyExisted.WarrantyStatus}");
            }
            try
            {
                warranty = ValidatedPeriodWarranty(warranty, out bool success, out string errorMessage);
                if (success == false)
                {
                    throw new Exception($"Error update warranty to jewelry id {warranty.JewelryId} in order id {warranty.OrderId} : {errorMessage}");
                }
                else if (warranty.EndDate < warranty.ActiveDate)
				{
                    throw new Exception("End date must be greater or equan active date");
				}
				warranty.WarrantyStatus = WarrantyStatus.ACTIVATED;
                bool successSaveWarranty = await _warrantyRepo.UpdateByIdAsync(warranty, warrantyExisted.WarrantyId);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
			Warranty warrantySuccessSaved = (await _warrantyRepo.GetAllAsync())
					.FirstOrDefault(w => w.OrderId == warranty.OrderId && w.JewelryId == warranty.JewelryId);
            return warrantySuccessSaved;
        }

        public async Task<Warranty> UpdateWarrantyStatusAsync(Warranty warranty, WarrantyStatus status)
        {
            Warranty warrantyExisted = await _warrantyRepo.GetByIdAsync(warranty.WarrantyId);
            ValidateWarrantyBeForeUpdate(warranty);
            try
            {
                warranty.WarrantyStatus = status;
                bool successSaveWarranty = await _warrantyRepo.UpdateByIdAsync(warranty, warrantyExisted.WarrantyId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
			Warranty warrantySuccessSaved = (await _warrantyRepo.GetAllAsync())
								.FirstOrDefault(w => w.OrderId == warranty.OrderId && w.JewelryId == warranty.JewelryId);
			return warrantySuccessSaved;
        }

        private Warranty ValidatedPeriodWarranty(Warranty warranty, out bool success, out string errorMessage)
        {
            success = true;
            errorMessage = string.Empty;
            double warrantyPeriod = warranty.WarrantyPeriod;
            DateTime ActivateDate = warranty.ActiveDate;
            DateTime EndDate = warranty.EndDate;
            TimeEnum periodUnitMeasure = warranty.PeriodUnitmeasure;

            switch (periodUnitMeasure)
            {
                case TimeEnum.Day:
                    DateTime EndDateShouldBe = ActivateDate.AddDays(warrantyPeriod);
                    if (EndDate != EndDateShouldBe)
                    {
                        success = false;
                        errorMessage = $"End date should be {EndDateShouldBe} when warranty period is {warrantyPeriod} and period unit measure is {periodUnitMeasure} ";
                    }
                    break;
                case TimeEnum.Month:
                    DateTime EndMonthShouldBe = ActivateDate.AddMonths(Convert.ToInt16(warrantyPeriod));
                    if (EndDate != EndMonthShouldBe)
                    {
                        success = false;
                        errorMessage = $"End date should be {EndMonthShouldBe} when warranty period is {warrantyPeriod} and period unit measure is {periodUnitMeasure} ";
                    }
                    break;
                case TimeEnum.Year:
                    DateTime EndYearShouldBe = ActivateDate.AddYears(Convert.ToInt16(warrantyPeriod));
                    if (EndDate != EndYearShouldBe)
                    {
                        success = false;
                        errorMessage = $"End date should be {EndYearShouldBe} when warranty period is {warrantyPeriod} and period unit measure is {periodUnitMeasure} ";
                    }
                    break;
                
            }
            return warranty;
        }

        private Warranty ValidateWarrantyBeForeUpdate(Warranty warranty)
        {
            Warranty warrantyExisted = _warrantyRepo.GetByIdAsync(warranty.WarrantyId).Result;
            if (warrantyExisted == null)
            {
                throw new Exception("Cannot update warranty because warranty has not found or has not been activated");
            }
            Order order = _orderService.GetOrderByIdAsync(warranty.OrderId).Result;
            if (order == null)
            {
                throw new Exception("Cannto update warranty because order id of this warranty is not found");
            }
            Jewelry jewelry = _jewelryService.GetJewelryById(warranty.JewelryId);
            if (jewelry == null)
            {
                throw new Exception("Cannot update warranty because jewelry id of this warranty is not found");
            }
            OrderDetail orderDetail = order.OrderDetails.AsParallel().FirstOrDefault(item => item.JewelryId == warranty.JewelryId);
            if (orderDetail == null)
            {
                throw new Exception("Cannot update warranty because jewelry not found in order");
            }
            if (warranty.WarrantyStatus == WarrantyStatus.INACTIVE)
            {
                throw new Exception("Cannot update warranty because this warranty has not been activated");
            }
            return warranty;
        }
    }
}
