namespace LoanApplication.Dto
{
    public class CustomerLoanDetaiDto
    {
        public Guid Id { get; set; }
        public int Term { get; set; }
        public decimal FinanceAmount { get; set; }
    }
}
