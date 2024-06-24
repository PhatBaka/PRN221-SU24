using BusinessObjects;
using BusinessObjects.Enums;
using BusinessObjects.FilterModels;
using Castle.Core.Internal;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Services.Impls
{
    public class JewelryService : IJewelryService
    {
        private IGenericRepository<Jewelry> _jewelryRepo;
        private ICategoryService _categoryService;
        
        public JewelryService(IServiceProvider service)
        {
            _jewelryRepo = service.GetRequiredService<IGenericRepository<Jewelry>>();
            _categoryService = service.GetRequiredService<ICategoryService>();
        }

        public Jewelry AddJewelry(Jewelry jewelry)
        {
            //check duplicate name
            String formattedName = Util.CapitalizeFirstLetterOfSentence(Regex.Replace(jewelry.JewelryName, @"\s+", " "));
            jewelry.JewelryName = formattedName;
            
            Jewelry jewelryHasDuplicatedName = _jewelryRepo.FistOrDefault(jew => jew.JewelryName.ToLower().Equals(jewelry.JewelryName.ToLower())).Result;
            if (jewelryHasDuplicatedName != null)
            {
                throw new Exception("Jewelry name is duplicated");
            }

            //check category name is valid
            Category category = _categoryService.GetCategoryByName(jewelry.Category.CategoryName);
            if(category == null)
            {
                if(String.IsNullOrEmpty(jewelry.Category.CategoryName))
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
            catch(Exception ex)
            {
                throw new Exception("Error when adding jewelry", ex);
            }
            Jewelry jewelrySaved = _jewelryRepo.FistOrDefault(jew => jew.JewelryName.Equals(jewelry.JewelryName)).Result;
            return jewelrySaved; 
        }

        public void DeleteJewelry(int id)
        {
            throw new NotImplementedException();
        }

        public List<Jewelry> GetJewelries()
        {
           List<Jewelry> listJewelry = _jewelryRepo.GetAllAsync().Result.ToList();
            return listJewelry;
        }

        public Jewelry GetJewelryById(int id)
        {
            Jewelry jewelry =_jewelryRepo.GetByIdAsync(id).Result;
            return jewelry;
        }

        public List<Jewelry> SearchFilterJewelries(JewelryFilter jewelryFilter)
        {
            IQueryable<Jewelry> query = _jewelryRepo.GetAllAsync().Result.OrderBy(j => j.JewelryId);

            if (!string.IsNullOrEmpty(jewelryFilter.SearchKeyword))
            {
                string keyword = jewelryFilter.SearchKeyword.ToLower();
                query = query.Where(j => j.JewelryName.ToLower().Contains(keyword) || j.Description.ToLower().Contains(keyword));
            }

            if(jewelryFilter.CategoryFilterOptions != null && jewelryFilter.CategoryFilterOptions.Count > 0)
            {
                query = query.Where(j => jewelryFilter.CategoryFilterOptions.Contains(j.Category.CategoryName));
            }

            //TODO MATERIAL
            //if(jewelryFilter.MaterialFilterOptions != null && jewelryFilter.MaterialFilterOptions.Count > 0)
            //{
            //    query = query.Where(j => j.JewelryMaterials.Any(jm => jewelryFilter.MaterialFilterOptions.Contains(jm.Material.MaterialName)));
            //}

            List<Jewelry> listPagination = new List<Jewelry>();
            if(jewelryFilter.PageNumber.HasValue && jewelryFilter.PageSize.HasValue)
            {
                listPagination = new PaginatedList<Jewelry>(query.ToList(), query.Count(), jewelryFilter.PageNumber.Value, jewelryFilter.PageSize.Value).ToList();
                //query = query.Skip((jewelryFilter.PageNumber.Value - 1) * jewelryFilter.PageSize.Value).Take(jewelryFilter.PageSize.Value);
            }

            return listPagination;
        }

        public Jewelry UpdateJewelry(Jewelry jewelry)
        {
            //check if id is valid
            Jewelry jewelryToUpdate = _jewelryRepo.GetByIdAsync(jewelry.JewelryId).Result;
            if(jewelryToUpdate == null)
            {
                throw new Exception("Jewelry id is invalid");
            }

            //check duplicate name
            String formattedName = Util.CapitalizeFirstLetterOfSentence(Regex.Replace(jewelry.JewelryName, @"\s+", " "));
            jewelry.JewelryName = formattedName;

            Jewelry jewelryHasDuplicatedName = _jewelryRepo.FistOrDefault(jew => jew.JewelryName.ToLower().Equals(jewelry.JewelryName.ToLower())).Result;
            if (jewelryHasDuplicatedName != null && jewelryHasDuplicatedName.JewelryId != jewelry.JewelryId)
            {
                throw new Exception("Jewelry name is duplicated");
            }

            //check category name is valid
            Category category = _categoryService.GetCategoryByName(jewelry.Category.CategoryName);
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
                bool success = _jewelryRepo.UpdateByIdAsync(jewelry, jewelry.JewelryId).Result;
                if (!success)
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error when update jewelry");
            }
            Jewelry jewelrySaved = _jewelryRepo.GetByIdAsync(jewelry.JewelryId).Result;
            return jewelrySaved;
        }

        public byte[] FormatJewelryImageDataString(string imageData)
        {
            throw new NotImplementedException();
        }

        public string GetJewelryImageString(byte[] imageData)
        {
               throw new NotImplementedException();
        }
    }
}
