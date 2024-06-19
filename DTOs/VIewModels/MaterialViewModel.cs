namespace DTOs
{
    public class MaterialViewModel
    {
        public int MaterialId { get; set; }

        public string? MaterialName { get; set; }

        public bool IsMetal { get; set; }

        public decimal MaterialCost { get; set; }

        public float MaterialWeight { get; set; }

        public string? UnitType { get; set; }

        public int MaterialQuantity { get; set; }

        public byte[]? MaterialImage { get; set; }

        public float MetalWeight { get; set; }

        public int NumberOfGem { get; set; }
    }
}
