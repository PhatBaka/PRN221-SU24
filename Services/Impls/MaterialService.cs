using AutoMapper;
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
        private readonly IGenericRepository<JewelryMaterial> _jewelryMaterialRepository;
        private readonly IGenericRepository<Material> _materialRepository;
        private readonly IMapper _mapper;

        public MaterialService(IGenericRepository<Material> materialRepository,
                                IGenericRepository<JewelryMaterial> jewelryMaterialRepository,
                                IMapper mapper)
        {
            _mapper = mapper;
            _materialRepository = materialRepository;
            _jewelryMaterialRepository = jewelryMaterialRepository;
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

        public List<Material> GetGemsNotInJewelry()
        {
            var gem = _materialRepository.GetAllAsync().Result.Where(x => !x.IsMetail).ToList();
            var jewelryMaterials = _jewelryMaterialRepository.GetAllAsync().Result;

            // Create a set of gem IDs already in jewelry materials
            var jewelryMaterialGemIds = new HashSet<int>(jewelryMaterials.Select(jm => jm.MaterialId));

            // Exclude gems that are already in jewelry materials
            var filteredGem = gem.Where(g => !jewelryMaterialGemIds.Contains(g.MaterialId)).ToList();

            return filteredGem;
        }

        public Material GetMaterialById(int id)
        {
            var material = _materialRepository.GetByIdAsync(id).Result;
            return material;
        }

        public Material GetMaterialByName(string name)
        {
            return _materialRepository.GetAllAsync().Result.ToList().FirstOrDefault(x => x.MaterialName.Equals(name));
        }

        public List<Material> GetMaterials()
        {
            List<Material> listMaterial = _materialRepository.GetAllAsync().Result.ToList();
            return listMaterial;
        }

        public void UpdateMaterial(Material material)
        {
            var existedMaterial = _materialRepository.GetByIdAsync(material.MaterialId).Result;
            if (existedMaterial == null)
            {
                return;
            }
            _mapper.Map(material, existedMaterial);
            _materialRepository.UpdateByIdAsync(existedMaterial, material.MaterialId);
        }
    }
}
