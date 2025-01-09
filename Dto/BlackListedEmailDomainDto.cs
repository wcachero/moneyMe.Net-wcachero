namespace LoanApplication.Dto
{
    public class BlackListedEmailDomainDto
    {
        public string DomainName { get; set; } = "";
        public bool IsActive { get; set; }
    }
}
