using WebApplication2.Model;

namespace LoanApplication.Model
{
    public class CustomerLoanDetails
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        // Foreign key for CustomerDetail
        public Guid CustomerId { get; set; }
        public CustomerDetail Customer { get; set; }

        // Foreign key for LoanProduct
        public Guid ProductId { get; set; }
        public LoanProduct Product { get; set; }

        public decimal Repayment { get; set; }
        public decimal MonthlyAmortization { get; set; }
        public decimal TotalRepayment { get; set; }
        public int Term { get; set; }
        public decimal FinanceAmount { get; set; }
        public DateTimeOffset DateSubmitted { get; set; } = DateTimeOffset.Now;
        public DateTimeOffset DateUpdated { get; set; } = DateTimeOffset.Now;
    }
}
