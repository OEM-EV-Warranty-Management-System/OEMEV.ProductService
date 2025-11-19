namespace Service.DTOs
{
    public class VehicleDto
    {
        public long Id { get; set; }
        public string Vin { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public DateOnly WarrantyStart { get; set; }
        public DateOnly WarrantyEnd { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid CustomerId { get; set; }
        public long ManufactureId { get; set; }
        public List<VehiclePartDto> VehicleParts { get; set; } = new();
    }

    public class CreateVehicleDto
    {
        public string Vin { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public DateOnly WarrantyStart { get; set; }
        public DateOnly WarrantyEnd { get; set; }
        public string Status { get; set; }
        public Guid CustomerId { get; set; }
        public long ManufactureId { get; set; }
    }

    public class UpdateVehicleDto
    {
        public string Model { get; set; }
        public string Status { get; set; }
    }
}