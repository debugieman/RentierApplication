using Microsoft.AspNetCore.Mvc.Rendering;
using RentierApplication.DAL.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentierApplication.ViewModels
{
    public class TransactionsCreateViewModel
    {
        
        public string RealEstateName { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }

        public DateTime DateOfTransaction { get; set; }

        public int PaymentId { get; set; }
        
        public TransactionType Type { get; set; }

        public List<SelectListItem> TransactionTypes { get; set; }

        public TransactionsCreateViewModel()
        {
            TransactionTypes = new List<SelectListItem>
         {
            new SelectListItem { Value =  "5", Text = "One-Time Expense" },
            new SelectListItem { Value = "10", Text = "One-Time Income" }
         };
        }



        public enum TransactionType
        {
            OneTimeExpense = 5,
            OneTimeIncome = 10,
        }
    }
}


