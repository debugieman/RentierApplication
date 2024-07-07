using RentierApplication.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentierApplication.ViewModels
{
    public class TenantDeleteViewModel
    {        
        public int ID { get; set; }
        
        public string Name { get; set; }
        public string Surname { get; set; }

        public string Email { get; set; }

        public decimal MoneyObligation { get; set; }
        public decimal Surety { get; set; }
       
             
    }
}
