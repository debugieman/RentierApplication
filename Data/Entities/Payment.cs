using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentierApplication.Data.Entities
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("RealEstate")]
        public int? RealEstateId { get; set; }      
        
        public virtual RealEstate RealEstate { get; set; }        
        public decimal MonthlyIncome { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
