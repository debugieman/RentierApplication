using System.Transactions;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RentierApplication.ViewModels
{
    public class TransactionsViewModel
    {
        public int PaymentId { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}