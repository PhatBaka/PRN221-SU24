using BusinessObjects;
using BusinessObjects.Enums;
using BusinessObjects.FilterModels;
using Castle.Core.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.Extensions.DependencyInjection;
using Repositories;
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
using System.Transactions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Services.Impls
{
	public class JewelryService : IJewelryService
	{
		private IGenericRepository<Jewelry> _jewelryRepo;
		private IGenericRepository<JewelryMaterial> _jewelryMaterialRepo;
		private IGenericRepository<Category> _categoryRepo;
		private ICategoryService _categoryService;

		public JewelryService(IServiceProvider service)
		{
			_jewelryRepo = service.GetRequiredService<IGenericRepository<Jewelry>>();
			_categoryService = service.GetRequiredService<ICategoryService>();
			_jewelryMaterialRepo = service.GetRequiredService<IGenericRepository<JewelryMaterial>>();
			_categoryRepo = service.GetRequiredService<IGenericRepository<Category>>();
		}

		public Jewelry AddJewelry(Jewelry jewelry)
		{
			//check duplicate name
			jewelry.JewelryName = Util.CapitalizeFirstLetterOfSentence(Regex.Replace(jewelry.JewelryName, @"\s+", " ")).Trim();
			jewelry.Description = Util.CapitalizeFirstLetterOfSentence(Regex.Replace(jewelry.Description, @"\s+", " ")).Trim();
			Jewelry jewelryHasDuplicatedName = _jewelryRepo.FistOrDefault(jew => jew.JewelryName.ToLower().Equals(jewelry.JewelryName.ToLower())).Result;
			if (jewelryHasDuplicatedName != null)
			{
				throw new Exception("Jewelry name is duplicated");
			}

			//check category name is valid
			Category category = _categoryRepo.FistOrDefault(c => c.CategoryName.Equals(jewelry.Category.CategoryName)).Result;
			if (category == null)
			{
				if (String.IsNullOrEmpty(jewelry.Category.CategoryName))
				{
					throw new Exception("Category is required");
				}
				jewelry.Category.CategoryName = Util.CapitalizeFirstLetterOfSentence(Regex.Replace(jewelry.Category.CategoryName.Trim(), @"\s+", " "));
				Category savedCategory = _categoryService.AddCategory(jewelry.Category);
				if (savedCategory != null)
				{
					category = savedCategory;
				}
			}
			jewelry.CategoryId = category.CategoryId;
			jewelry.Category = category;

			//check quantity to set sale status
			if (jewelry.Quantity <= 0)
			{
				jewelry.StatusSale = StatusSale.OUT_OF_STOCK;
			}
			else
			{
				jewelry.StatusSale = StatusSale.IN_STOCK;
			}


			//TODO: check promotion id is valid
			//TODO: check material id is valid

			//Add jewelry
			try
			{
				bool success = _jewelryRepo.InsertAsync(jewelry).Result;
			}
			catch (Exception ex)
			{
				throw new Exception("Error when adding jewelry", ex);
			}
			Jewelry jewelrySaved = _jewelryRepo.FistOrDefault(jew => jew.JewelryName.Equals(jewelry.JewelryName)).Result;
			List<JewelryMaterial> listJewelryMaterial = jewelry.JewelryMaterials.ToList();
			return jewelrySaved;
		}

		public async Task DeleteJewelryAsync(int id)
		{
			Jewelry jewelry = _jewelryRepo.GetByIdAsync(id).Result;
			if(jewelry == null)
			{
				throw new Exception("Jewelry is not found");
			}
			try
			{
				await _jewelryRepo.DeleteAsync(jewelry);
			}
			catch (Exception ex)
			{
				throw new Exception("Error when deleting jewelry", ex);
			}
		}

		public List<Jewelry> GetJewelries()
		{
			List<Jewelry> listJewelry = _jewelryRepo.GetAllAsync().Result.ToList();
			return listJewelry;
		}

		public Jewelry GetJewelryById(int id)
		{
			Jewelry jewelry = _jewelryRepo.GetByIdAsync(id).Result;
			return jewelry;
		}

		public List<Jewelry> SearchFilterJewelries(JewelryFilter jewelryFilter)
		{
			IQueryable<Jewelry> query = _jewelryRepo.GetAllAsync().Result.OrderBy(j => j.JewelryId);

			if (!string.IsNullOrEmpty(jewelryFilter.SearchKeyword))
			{
				Console.WriteLine(jewelryFilter.SearchKeyword);
				string keyword = jewelryFilter.SearchKeyword.ToLower();
				query = query.Where(j => j.JewelryName.ToLower().Contains(keyword));
			}

			if (jewelryFilter.CategoryFilterOptions != null && jewelryFilter.CategoryFilterOptions.Count > 0)
			{
				query = query.Where(j => jewelryFilter.CategoryFilterOptions.Contains(j.Category.CategoryName));
			}

			//TODO MATERIAL
			//if(jewelryFilter.MaterialFilterOptions != null && jewelryFilter.MaterialFilterOptions.Count > 0)
			//{
			//    query = query.Where(j => j.JewelryMaterials.Any(jm => jewelryFilter.MaterialFilterOptions.Contains(jm.Material.MaterialName)));
			//}

			List<Jewelry> listPagination = query.ToList();
			if (jewelryFilter.PageNumber.HasValue && jewelryFilter.PageSize.HasValue)
			{
				listPagination = new PaginatedList<Jewelry>(query.ToList(), query.Count(), jewelryFilter.PageNumber.Value, jewelryFilter.PageSize.Value).ToList();
				//query = query.Skip((jewelryFilter.PageNumber.Value - 1) * jewelryFilter.PageSize.Value).Take(jewelryFilter.PageSize.Value);
			}

			return listPagination;
		}

		public async Task<Jewelry> UpdateJewelryAsync(Jewelry jewelry)
		{
			using (var scope = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled))
			{
				try
				{
					// Check if id is valid
					Jewelry jewelryToUpdate = await _jewelryRepo.GetByIdAsync(jewelry.JewelryId);
					if (jewelryToUpdate == null)
					{
						throw new Exception("Jewelry id is invalid");
					}

					// Standardize and validate names
					jewelry.JewelryName = Util.CapitalizeFirstLetterOfSentence(Regex.Replace(jewelry.JewelryName, @"\s+", " ")).Trim();
					jewelry.Description = Util.CapitalizeFirstLetterOfSentence(Regex.Replace(jewelry.Description, @"\s+", " ")).Trim();

					// Check duplicate name
					Jewelry jewelryHasDuplicatedName = await _jewelryRepo.FirstOrDefaultAsync(j => j.JewelryName.ToLower() == jewelry.JewelryName.ToLower() && j.JewelryId != jewelry.JewelryId);
					if (jewelryHasDuplicatedName != null)
					{
						throw new Exception("Jewelry name is duplicated");
					}

					// Validate category
					Category category = await _categoryRepo.FirstOrDefaultAsync(c => c.CategoryName.Equals(jewelry.Category.CategoryName));
					if (category == null)
					{
						if (string.IsNullOrEmpty(jewelry.Category.CategoryName))
						{
							throw new Exception("Category is required");
						}
						jewelry.Category.CategoryName = Util.CapitalizeFirstLetterOfSentence(Regex.Replace(jewelry.Category.CategoryName.Trim(), @"\s+", " "));
						category = _categoryService.AddCategory(jewelry.Category);
						if (category == null)
						{
							throw new Exception("Failed to add category");
						}
					}
					jewelry.CategoryId = category.CategoryId;
					jewelry.Category = category;

					// Set sale status based on quantity
					jewelry.StatusSale = jewelry.Quantity <= 0 ? StatusSale.OUT_OF_STOCK : StatusSale.IN_STOCK;

					// Update jewelry materials
					foreach (var material in jewelry.JewelryMaterials)
					{
						material.JewelryId = jewelry.JewelryId;
						material.MaterialId = material.Material.MaterialId;
					}

					// Update jewelry
					bool success = await _jewelryRepo.UpdateByIdAsync(jewelry, jewelry.JewelryId);
					if (!success)
					{
						throw new Exception("Failed to update jewelry");
					}

					// Retrieve updated jewelry to get the latest state
					Jewelry jewelryAfterUpdate = await _jewelryRepo.GetByIdAsync(jewelry.JewelryId);

					// Update or insert jewelry materials
					IQueryable<JewelryMaterial> jewelryMaterialsDB = jewelryAfterUpdate.JewelryMaterials.AsQueryable();

					foreach (var material in jewelry.JewelryMaterials)
					{
						var existingMaterial = jewelryMaterialsDB.FirstOrDefault(jm => jm.MaterialId == material.MaterialId && jm.JewelryId == material.JewelryId);
						if (existingMaterial != null)
						{
							existingMaterial.JewelryWeight = material.JewelryWeight;
							await _jewelryMaterialRepo.UpdateAsync(existingMaterial);
						}
						else
						{
							var newMaterial = new JewelryMaterial
							{
								JewelryId = jewelry.JewelryId,
								MaterialId = material.MaterialId,
								JewelryWeight = material.JewelryWeight
							};
							await _jewelryMaterialRepo.InsertAsync(newMaterial);
						}
					}

					// Delete removed jewelry materials
					foreach (var material in jewelryMaterialsDB)
					{
						if (!jewelry.JewelryMaterials.Any(jm => jm.MaterialId == material.MaterialId && jm.JewelryId == material.JewelryId))
						{
							await _jewelryMaterialRepo.DeleteAsync(material);
						}
					}

					scope.Complete(); // Commit the transaction
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					throw new Exception("Error when updating jewelry", ex);
				}
			}

			// Return updated jewelry
			return await _jewelryRepo.GetByIdAsync(jewelry.JewelryId);
		}

		public double GetJewelrySalePrice(Jewelry jewelry)
		{
			double totalMetalCost = 0;
			double totalStoneCost = 0;
			double laborCost = 0;
			double markupPercentage = 0;

			foreach (var metal in jewelry.JewelryMaterials.Where(jm => jm.Material.IsMetail))
			{
				totalMetalCost += (double)metal.Material.BidPrice * metal.JewelryWeight;
			}

			foreach (var gemstone in jewelry.JewelryMaterials.Where(jm => !jm.Material.IsMetail))
			{
				totalStoneCost += (double)gemstone.Material.MaterialCost * gemstone.JewelryWeight;
			}
			laborCost = (double)jewelry.LaborPrice;
			markupPercentage = jewelry.MarkupPercentage;

			double totalCost = (totalMetalCost + totalStoneCost + laborCost) * (1 + markupPercentage / 100);
			return totalCost;

		}
	}
}
