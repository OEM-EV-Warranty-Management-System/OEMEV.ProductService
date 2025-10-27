using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class VehiclePart
{
    public long Id { get; set; }

    public string? SerialNumber { get; set; }

    public DateTime? CreatedAt { get; set; }

    public long? VehicleId { get; set; }

    public long? PartId { get; set; }

    public bool? IsActive { get; set; }

    public virtual Part? Part { get; set; }

    public virtual Vehicle? Vehicle { get; set; }
}
