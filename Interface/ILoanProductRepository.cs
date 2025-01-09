using LoanApplication.Model;

namespace LoanApplication.Interface
{
    public interface ILoanProductRepository
    {
        Task Add(LoanProduct addproduct);
        Task<LoanProduct?> GetSingleLoanProductAsync(string productName);
        Task<List<LoanProduct>> GetLoanProductsDetailAsync();
    }
}
