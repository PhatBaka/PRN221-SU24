using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DTOs
{
    public class GemDTO
    {
		public string? CertificateCode { get; set; }

		public string? Description { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public decimal SellPrice { get; set; }

        public decimal BuyPrice { get; set; }

        public IFormFile? MaterialImageFile { get; set; }

        public IFormFile? CertificateImageFile { get; set; }

        public bool IsMetal { get; set; }

        public decimal Weight { get; set; }

        public decimal Purity { get; set; }

        public string? Clarity { get; set; }

        public string? Color { get; set; }

        public string? Cut { get; set; }

        public string? Shape { get; set; }

        public string? MaterialStatus { get; set; }

		public string? GemType { get; set; }
	}

    public class GetGemDTO : GemDTO
    {
        public GetGemDTO()
        {
            Jewelries = new HashSet<GetJewelryDTO>();
        }

        public Guid MaterialId { get; set; }

        public byte[]? MaterialImageData { get; set; }

        public byte[]? CertificateImageData { get; set; }

        public virtual ICollection<GetJewelryDTO> Jewelries { get; set; }
    }
}
