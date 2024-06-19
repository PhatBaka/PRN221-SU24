using BusinessObjects;
using DataAccessObjects.Impls;
using DataAccessObjects.Interfaces;
using DTOs;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Impls
{
    public class JewelryRepository : BaseRepository<Jewelry>, IJewelryRepository
    {
        private readonly IJewelryMaterialDAO jewelryMaterialDAO;

        public JewelryRepository(IJewelryMaterialDAO jewelryMaterialDAO)
        {
            this.jewelryMaterialDAO = jewelryMaterialDAO;
        }

        public async Task<bool> AddMaterialIntoJewelry(Jewelry jewelry, IList<MaterialViewModel> materials)
        {
            try
            {
                float metalWeight = 0;
                int numberOfGem = 0;
                foreach (var material in materials)
                {
                    if (material.IsMetal)
                    {
                        metalWeight = material.MetalWeight;
                    }
                    else
                    {
                        numberOfGem = material.NumberOfGem;
                    }
                    JewelryMaterial jewelryMaterial = new JewelryMaterial()
                    {
                        JewelryId = jewelry.JewelryId,
                        MaterialId = material.MaterialId,
                        MetalWeight = metalWeight,
                        NumberOfGem = numberOfGem
                    };
                    if (!await BaseDAO<JewelryMaterial>.Instance.CreateAsync(jewelryMaterial))
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
