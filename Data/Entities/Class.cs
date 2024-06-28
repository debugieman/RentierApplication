using static RentierApplication.Data.Entities.Payments;

namespace RentierApplication.Data.Entities
{
    
        public class Transaction
        {
            public int Id { get; set; }
            public DateTime Date { get; set; }
            public decimal Amount { get; set; }
            public string Description { get; set; }
            public TransactionType Type { get; set; }
            public int FinanceId { get; set; }
            public Finance Finance { get; set; }
        }

    public enum TransactionType
    {
        OneTimeExpense,
        OneTimeIncome
    }
}
