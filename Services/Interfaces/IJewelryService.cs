using BusinessObjects;
using BusinessObjects.FilterModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IJewelryService
    {
        List<Jewelry> GetJewelries();
        List<Jewelry> SearchFilterJewelries(JewelryFilter jewelryFilter);
        Jewelry GetJewelryById(int id);
		Jewelry AddJewelry(Jewelry jewelry);
        Task<Jewelry> UpdateJewelry(Jewelry jewelry);
        void DeleteJewelry(int id);
        byte[] FormatJewelryImageDataString(string imageData);
        string GetJewelryImageString(byte[] imageData);
        double GetJewelrySalePrice(Jewelry jewelry);
    }
}
