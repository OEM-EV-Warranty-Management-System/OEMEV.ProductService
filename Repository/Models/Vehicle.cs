using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class Vehicle
{
    public long Id { get; set; }

    public string Vin { get; set; } = null!;

    public string? Model { get; set; }

    public long? Year { get; set; }

    public DateOnly? WarrantyStart { get; set; }

    public DateOnly? WarrantyEnd { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public Guid? CustomerId { get; set; }

    public long? ManufactureId { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<VehiclePart> VehicleParts { get; set; } = new List<VehiclePart>();
}
