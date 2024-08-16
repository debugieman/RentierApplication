using System.ComponentModel.DataAnnotations;

namespace RentierApplication.ViewModels
{
    public class RealEstateEditViewModel
    {
        [MaxLength(20)]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}