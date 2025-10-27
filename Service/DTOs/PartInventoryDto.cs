using Service.DTOs;

namespace Service.DTOs
{
    public class PartInventoryDto
    {
        public long Id { get; set; }
        public int Quantity { get; set; }
        public long ManufactureId { get; set; }
        public long ServiceCenterId { get; set; }
        public long PartId { get; set; }
        public DateTime CreatedAt { get; set; }
        public PartDto Part { get; set; }
    }

    public class CreatePartInventoryDto
    {
        public int Quantity { get; set; }
        public long ManufactureId { get; set; }
        public long ServiceCenterId { get; set; }
        public long PartId { get; set; }
    }

    public class UpdatePartInventoryDto
    {
        public int Quantity { get; set; }
    }
}