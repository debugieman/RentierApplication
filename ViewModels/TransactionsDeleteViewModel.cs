using RentierApplication.DAL.Entities;

namespace RentierApplication.ViewModels
{
    public class TransactionsDeleteViewModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public TransactionType Type { get; set; }
        public int PaymentId { get; set; }
        public virtual Payment Payment { get; set; }
    }
}