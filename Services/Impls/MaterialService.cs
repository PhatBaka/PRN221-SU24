using BusinessObjects;
using DataAccessObjects;
using Repositories;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Impls
{
    public class MaterialService : IMaterialService
    {
        private readonly IGenericRepository<Material> _materialRepository;

        public MaterialService(IGenericRepository<Material> materialRepository)
        {
            _materialRepository = materialRepository;
        }

        public Material AddMaterial(Material material)
        {
            try
            {
                if (_materialRepository.InsertAsync(material).Result)
                {
                    Material materialSaved = _materialRepository.FistOrDefault(mat => mat.MaterialName.Equals(material.MaterialName)).Result;
                    return materialSaved;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteMaterial(int id)
        {
            throw new NotImplementedException();
        }

        public Material GetMaterialById(int id)
        {
            throw new NotImplementedException();
        }

        public Material GetMaterialByName(string name)
        {
            throw new NotImplementedException();
        }

        public List<Material> GetMaterials()
        {
            List<Material> listMaterial = _materialRepository.GetAllAsync().Result.ToList();
            return listMaterial;
        }

        public void UpdateMaterial(Material material)
        {
            throw new NotImplementedException();
        }
    }
}
