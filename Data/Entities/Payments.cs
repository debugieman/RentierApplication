namespace RentierApplication.Data.Entities
{
    public class Payments
    {
        public class Finance
        {
            public int Id { get; set; }
            public int RealEstateId { get; set; }
            public RealEstate RealEstate { get; set; }
            public decimal MonthlyIncome { get; set; }
            public List<Transaction> Transactions { get; set; }
        }


    }
}
