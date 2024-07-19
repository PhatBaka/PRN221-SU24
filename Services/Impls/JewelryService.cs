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
			if (jewelry.Quantity == 0 && jewelry.StatusSale != StatusSale.OUT_OF_STOCK)
			{
				throw new Exception("Jewelry with quantity 0 must have StatusSale is " + StatusSale.OUT_OF_STOCK);
			}
			if(jewelry.Quantity > 0 && jewelry.StatusSale != StatusSale.IN_STOCK)
			{
				throw new Exception("Jewelry with quantity greater than 0 must have StatusSale is " + StatusSale.IN_STOCK);
			}

			//TODO: check promotion id is valid
			//TODO: check material id is valid
			ValidateMaterialBeforCreate(jewelry, out bool successValidate, out string messageError);
			if (!successValidate)
			{
				throw new Exception(messageError);
			}
			//Add jewelry
			try
			{
				bool success = _jewelryRepo.InsertAsync(jewelry).Result;
			}
			catch (Exception ex)
			{
				throw new Exception("Error when adding jewelry. Error Message: " + ex.Message , ex);
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
			
					// Check if id is valid
					Jewelry jewelryToUpdate = await _jewelryRepo.GetByIdAsync(jewelry.JewelryId);
					if (jewelryToUpdate == null)
					{
						throw new Exception("Jewelry id is invalid");
					}

					// Standardize and validate names
					jewelry.JewelryName = Util.CapitalizeFirstLetterOfSentence(Regex.Replace(jewelry.JewelryName, @"\s+", " ")).Trim();
					jewelry.Description = Util.CapitalizeFirstLetterOfSentence(Regex.Replace(jewelry.Description, @"\s+", " ")).Trim();


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

					//check quantity to set sale status
					if (jewelry.Quantity == 0 && jewelry.StatusSale != StatusSale.OUT_OF_STOCK)
					{
						throw new Exception("Jewelry with quantity 0 must have StatusSale is " + StatusSale.OUT_OF_STOCK);
					}
					if (jewelry.Quantity > 0 && jewelry.StatusSale != StatusSale.IN_STOCK)
					{
						throw new Exception("Jewelry with quantity greater than 0 must have StatusSale is " + StatusSale.IN_STOCK);
					}

					//TODO: check promotion id is valid
					//TODO: check material id is valid
					ValidateMaterialBeforUpdate(jewelryToUpdate, jewelry, out bool successValidate, out string messageError);
					if (!successValidate)
					{
						throw new Exception(messageError);
					}

					// Update jewelry materials
					foreach (var material in jewelry.JewelryMaterials)
					{
						material.JewelryId = jewelry.JewelryId;
						material.MaterialId = material.Material.MaterialId;
					}
					using (var scope = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled))
					{
						try
						{
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
					throw new Exception("Error when updating jewelry. Error Message: " + ex.Message, ex);
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
		
		private void ValidateMaterialBeforCreate(Jewelry jewelry, out bool success, out string messageError)
		{
			success = true;
			messageError = "";
			//validate exist at least one type of material
			List<JewelryMaterial> jewelryMaterials = jewelry.JewelryMaterials.ToList();
			List<JewelryMaterial> metalMaterials = jewelryMaterials.Where(jm => jm.Material.IsMetail).ToList();	
			List<JewelryMaterial> stoneMaterials = jewelryMaterials.Where(jm => !jm.Material.IsMetail).ToList();
			if (metalMaterials.IsNullOrEmpty() && stoneMaterials.IsNullOrEmpty())
			{
				success = false;
				messageError = ("Jewelry must have at least one type of material");
				return;
			}

			//validate duplicate material
			List<Material> jewelryMaterialWithDuplicate = jewelryMaterials
				.GroupBy(jm => jm.Material.MaterialId) // Nhóm theo MaterialId
				.Where(mtKey => mtKey.Count() > 1) // Chỉ giữ lại các nhóm có số lượng lớn hơn 1
				.SelectMany(group => group.Select(jm => jm.Material).Distinct()) // Lấy ra tất cả các vật liệu trong các nhóm này
				.ToList(); // Chuyển kết quả thành danh sách

			if (jewelryMaterialWithDuplicate.Count > 0)
			{
				success = false;
				foreach (var jewelryMaterial in jewelryMaterialWithDuplicate)
				{
					messageError = messageError + "Material " + jewelryMaterial.MaterialName + " is duplicated. \n";
				}
				return;
			}

			//validate unique stoneMaterial
			if (stoneMaterials.Count > 0)
			{
				List<JewelryMaterial> stoneMaterialsHasBeenAssigned = _jewelryRepo.GetAllAsync().Result.SelectMany(j => j.JewelryMaterials).Where(jm => !jm.Material.IsMetail).ToList();
				List<JewelryMaterial> stoneMaterialsAreNotAllowToBeAssigned = stoneMaterials.SelectMany(jm => stoneMaterialsHasBeenAssigned.Where(jm2 => jm2.MaterialId == jm.Material.MaterialId)).ToList();
				if(stoneMaterialsAreNotAllowToBeAssigned.Count > 0)
				{
					success = false;
					foreach(var stoneMaterial in stoneMaterialsAreNotAllowToBeAssigned)
					{
						messageError = messageError + "Stone material " + stoneMaterial.Material.MaterialName + " has been assigned to another jewelry\n";
					}
					return;
				}
							
			}

			//validate quantity of product based on if it contain gemstone
			if(stoneMaterials.Count > 0)
			{
				if(jewelry.Quantity > 1)
				{
					success = false;
					messageError = "Jewelry with gemstone must have quantity equal 1";
					return;
				}
			}
		}

		private void ValidateMaterialBeforUpdate(Jewelry originalJewelry, Jewelry jewelry, out bool success, out string messageError)
		{
			success = true;
			messageError = "";
			//validate exist at least one type of material
			List<JewelryMaterial> jewelryMaterials = jewelry.JewelryMaterials.ToList();
			List<JewelryMaterial> metalMaterials = jewelryMaterials.Where(jm => jm.Material.IsMetail).ToList();
			List<JewelryMaterial> stoneMaterials = jewelryMaterials.Where(jm => !jm.Material.IsMetail).ToList();
			if (metalMaterials.IsNullOrEmpty() && stoneMaterials.IsNullOrEmpty())
			{
				success = false;
				messageError = ("Jewelry must have at least one type of material");
				return;
			}

			List<Material> jewelryMaterialWithDuplicate = jewelryMaterials
			.GroupBy(jm => jm.Material.MaterialId) 
			.Where(mtKey => mtKey.Count() > 1) 
			.SelectMany(group => group.Select(jm => jm.Material).Distinct()) 
			.ToList(); 

			if (jewelryMaterialWithDuplicate.Count > 0)
			{
				success = false;
				foreach (var jewelryMaterial in jewelryMaterialWithDuplicate)
				{
					messageError = messageError + "Material " + jewelryMaterial.MaterialName + " is duplicated. \n";
				}
				return;
			}

			//validate unique stoneMaterial
			if (stoneMaterials.Count > 0)
			{
				List<JewelryMaterial> stoneMaterialsHasBeenAssigned = _jewelryRepo.GetAllAsync().Result.SelectMany(j => j.JewelryMaterials).Where(jm => !jm.Material.IsMetail).ToList();
				List<JewelryMaterial> stoneMaterialsAreNotAllowToBeAssigned = stoneMaterials.SelectMany(jm => stoneMaterialsHasBeenAssigned.Where(jm2 => jm2.MaterialId == jm.Material.MaterialId && jm2.JewelryId != originalJewelry.JewelryId)).ToList();
				if (stoneMaterialsAreNotAllowToBeAssigned.Count > 0)
				{
					success = false;
					foreach (var stoneMaterial in stoneMaterialsAreNotAllowToBeAssigned)
					{
						messageError = messageError + "Stone material " + stoneMaterial.Material.MaterialName + " has been assigned to another jewelry\n";
					}
					return;
				}
				
			}
			//validate quantity of product based on if it contain gemstone
			if (stoneMaterials.Count > 0)
			{
				if (jewelry.Quantity > 1)
				{
					success = false;
					messageError = "Jewelry with gemstone must have quantity equal 1";
					return;
				}
				
			}
		}

	}
}
