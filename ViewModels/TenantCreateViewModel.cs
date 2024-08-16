namespace RentierApplication.ViewModels
{
    public class TenantCreateViewModel
    {
        public int ID { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }

        public string Email { get; set; }

        public decimal MoneyObligation { get; set; }
        public decimal Surety { get; set; }

        public int RealEstateID { get; set; }
    }
}