﻿using BusinessObjects;
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
        Task<Jewelry> UpdateJewelryAsync(Jewelry jewelry);
        Task DeleteJewelryAsync(int id);
        double GetJewelrySalePrice(Jewelry jewelry);
    }
}
