using LoanApplication.Interface;
using LoanApplication.Model;
using System.Threading.Tasks;
using WebApplication2.Repositories;

namespace LoanApplication.Services
{
    public class LoanProductService : ILoanProductService
    {
        private readonly ILoanProductRepository _loanProductRepository;

        public LoanProductService(ILoanProductRepository loanProductRepository)
        {
            _loanProductRepository = loanProductRepository;
        }

        public async Task<LoanProduct?> GetSingleLoanProductDetailAsync(string productName)
        {
            return await _loanProductRepository.GetSingleLoanProductAsync(productName);
        }
        public async Task<List<LoanProduct>> GetLoanProductsDetailAsync()
        {
            return await _loanProductRepository.GetLoanProductsDetailAsync();
        }

        public async Task Add(LoanProduct loanProduct)
        {
            await _loanProductRepository.Add(loanProduct);
        }

        public async Task<bool> ProductExistsAsync(string productName)
        {
            var product = await _loanProductRepository.GetSingleLoanProductAsync(productName);
            return product != null;
        }
    }
}