namespace LoanApplication.Model
{
    public class ErrorResponse
    {
        public Dictionary<string, List<string>> Errors { get; set; } = new Dictionary<string, List<string>>();
    }
}
