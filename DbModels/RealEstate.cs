using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RentierApplication.DbModels;

[Index("TenantsId", Name = "IX_RealEstates_TenantsID")]
[Index("UserId", Name = "IX_RealEstates_UserId")]
public partial class RealEstate
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string UserId { get; set; } = null!;

    [Column("TenantsID")]
    public int? TenantsId { get; set; }

    [ForeignKey("TenantsId")]
    [InverseProperty("RealEstates")]
    public virtual Tenant? Tenants { get; set; }

    [InverseProperty("RealEstate")]
    public virtual ICollection<Tenant> TenantsNavigation { get; } = new List<Tenant>();

    [ForeignKey("UserId")]
    [InverseProperty("RealEstates")]
    public virtual AspNetUser User { get; set; } = null!;
}
