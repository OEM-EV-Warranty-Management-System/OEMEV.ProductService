using Service.DTOs;

namespace Service.DTOs
{
    public class VehiclePartDto
    {
        public long Id { get; set; }
        public string SerialNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public long VehicleId { get; set; }
        public long PartId { get; set; }
        public VehicleDto Vehicle { get; set; }
        public PartDto Part { get; set; }
    }

    public class CreateVehiclePartDto
    {
        public string SerialNumber { get; set; }
        public long VehicleId { get; set; }
        public long PartId { get; set; }
    }

    public class UpdateVehiclePartDto
    {
        public string SerialNumber { get; set; }
    }
}