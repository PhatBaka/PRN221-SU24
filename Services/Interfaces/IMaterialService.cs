using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IMaterialService
    {
        List<Material> GetMaterials();
        Material GetMaterialById(int id);
        Material GetMaterialByName(string name);
        Material AddMaterial(Material material);
        void UpdateMaterial(Material material);
        void DeleteMaterial(int id);
    }
}
