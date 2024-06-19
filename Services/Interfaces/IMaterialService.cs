using BusinessObjects;
using DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IMaterialService
    {
        public Task<IList<MaterialDTO>> GetMaterials();
        public Task<MaterialDTO> GetMaterialById(int id);
        public Task<bool> UpdateMaterial(MaterialDTO updateMaterialDTO, IFormFile materialImage);
    }
}
