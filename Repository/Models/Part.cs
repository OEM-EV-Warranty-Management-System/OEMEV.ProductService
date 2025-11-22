using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Models;

public partial class Part
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    public string? Name { get; set; }

    public string? Type { get; set; }

    public int? WarrantyMonths { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<PartInventory> PartInventories { get; set; } = new List<PartInventory>();

    public virtual ICollection<VehiclePart> VehicleParts { get; set; } = new List<VehiclePart>();
}
