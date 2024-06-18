using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.FilterModels
{
    public class JewelryFilter
    {
        public string? SearchKeyword { get; set; }
        
        public HashSet<string>? CategoryFilterOptions { get; set; }
       
        public HashSet<string>? MaterialNameFilterOptions { get; set; }
        
        [Range(1, int.MaxValue, ErrorMessage = "Value page number for {0} must be from {1}")]
        public int? PageNumber { get; set; }

        [Range(5, 20, ErrorMessage = "Value page size for {0} must be from {1} to {2}")]
        public int? PageSize { get; set; }
        
        public string? SortBy { get; set; }
        
        public string? SortDirection { get; set; }
       
    }
}
