using RentierApplication.DAL.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentierApplication.ViewModels
{
    public class PaymentCreateViewModel
    {        
        public int Id { get; set; }        
        public int? RealEstateId { get; set; }        
        public decimal MonthlyIncome { get; set; }        
    }
}
