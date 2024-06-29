using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentierApplication.Data.Entities
{
    public class Tenant
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }
        public string Surname { get; set; }

        public string Email { get; set; }

        public decimal MoneyObligation { get; set; }
        public decimal Surety { get; set; }

        [ForeignKey("RealEstate")]
        public int RealEstateID { get; set; }
        
        public virtual RealEstate RealEstateTenant { get; set; }                   
    }
}
