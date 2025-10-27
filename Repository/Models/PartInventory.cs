using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class PartInventory
{
    public long Id { get; set; }

    public long? Quantity { get; set; }

    public long? ManufactureId { get; set; }

    public long? ServiceCenterId { get; set; }

    public long? PartId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? IsActive { get; set; }

    public virtual Part? Part { get; set; }
}
