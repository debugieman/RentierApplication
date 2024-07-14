using RentierApplication.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentierApplication.ViewModels
{
    public class RealEstateCreateViewModel
    {
        public int ID { get; set; }
        
        public string Name { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }

    }
}
