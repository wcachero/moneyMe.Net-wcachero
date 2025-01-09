using LoanApplication.Model;
using WebApplication2.Model;

namespace LoanApplication.Interface
{
    public interface ILoanProductService
    {
     //   Task<List<LoanProduct>> GetLoanProductDetailAsync();
        Task<LoanProduct?> GetSingleLoanProductDetailAsync(string productName);
        Task Add(LoanProduct loanProduct);
        Task<bool> ProductExistsAsync(string productName);
        Task<List<LoanProduct>> GetLoanProductsDetailAsync();
    }
}
