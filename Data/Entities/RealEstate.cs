using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace RentierApplication.Data.Entities
{
    public class RealEstate
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }

        public virtual ICollection<Tenant> Tenants { get; set; }
        [ForeignKey("Payment")]
        public int? PaymentId { get; set; }
        public virtual Payment Payment { get; set; }
    }
}
