using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Models;

public partial class VehiclePart
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    public string? SerialNumber { get; set; }

    public DateTime? CreatedAt { get; set; }

    public long? VehicleId { get; set; }

    public long? PartId { get; set; }

    public bool? IsActive { get; set; }

    public virtual Part? Part { get; set; }

    public virtual Vehicle? Vehicle { get; set; }
}
