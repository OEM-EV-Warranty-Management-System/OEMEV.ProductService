namespace Service.DTOs
{
    public class PartDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int WarrantyMonths { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CreatePartDto
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int WarrantyMonths { get; set; }
    }

    public class UpdatePartDto
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int WarrantyMonths { get; set; }
    }
}