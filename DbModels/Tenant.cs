using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RentierApplication.DbModels;

[Index("RealEstateId", Name = "IX_Tenants_RealEstateID")]
public partial class Tenant
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string Email { get; set; } = null!;

    [Column(TypeName = "decimal(18, 2)")]
    public decimal MoneyObligation { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Surety { get; set; }

    [Column("RealEstateID")]
    public int RealEstateId { get; set; }

    [ForeignKey("RealEstateId")]
    [InverseProperty("TenantsNavigation")]
    public virtual RealEstate RealEstate { get; set; } = null!;

    [InverseProperty("Tenants")]
    public virtual ICollection<RealEstate> RealEstates { get; } = new List<RealEstate>();
}
