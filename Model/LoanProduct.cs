namespace LoanApplication.Model
{
    public class LoanProduct
    {
        public Guid Id { get; set; } = new Guid();
        public string ProductName { get; set; } = "";
        public decimal InterestRate { get; set; }
        public decimal MinTerm { get; set; }
        public decimal MaxTerm { get; set; }
        public int InitialMonthNoInterest { get; set; }
        public decimal ProcessFee { get; set; }
        public DateTimeOffset DateAdded { get; set; } = DateTimeOffset.Now;

    }
}
