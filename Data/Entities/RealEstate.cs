using MessagePack;
using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Evaluation;
using RentierApplication.Data.Migrations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentierApplication.Data.Entities
{
    public class RealEstate
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int ID { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }   
                     
        public virtual ICollection<Tenants> Tenants { get; set; }

    }
}
