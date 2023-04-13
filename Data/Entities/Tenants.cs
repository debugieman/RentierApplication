using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentierApplication.Data.Entities
{
    public class Tenants
    {


        [System.ComponentModel.DataAnnotations.Key]
        public int ID { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }
        public string Surname { get; set; }

        public string Email { get; set; }


        public decimal MoneyObligation { get; set; }
        public decimal Surety { get; set; }

        public int RealEstateID { get; set; }

        [ForeignKey(nameof(RealEstateID))]
        public RealEstate RealEstateTenant { get; set; }

        
    }





}
