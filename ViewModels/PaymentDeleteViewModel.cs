using RentierApplication.DAL.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentierApplication.ViewModels
{
    public class PaymentDeleteViewModel
    {        
        public int Id { get; set; }        
        public int? RealEstateId { get; set; }        
        public decimal MonthlyIncome { get; set; }        
    }
}
