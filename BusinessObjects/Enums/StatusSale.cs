using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Enums
{
    public enum StatusSale
    {
        IN_STOCK, 
        OUT_OF_STOCK, 
        DISCONTINUED 
    }

    public static class StatusSaleExtension
    {
        public static string GetDisplayName(this StatusSale statusSale)
        {
            switch (statusSale)
            {
                case StatusSale.IN_STOCK:
                    return "In stock";
                case StatusSale.OUT_OF_STOCK:
                    return "Out of stock";
                case StatusSale.DISCONTINUED:
                    return "Discontinued";
                default:
                    throw new ArgumentOutOfRangeException(nameof(statusSale), statusSale, null);
            }
        }
    }
}
