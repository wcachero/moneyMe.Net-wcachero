using LoanApplication.Interface;
using LoanApplication.Model;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;

namespace WebApplication2.Repositories
{
    public class LoanProductRepository : ILoanProductRepository
    {
        private readonly MoneyMeDbContext _context;

        public LoanProductRepository(MoneyMeDbContext context)
        {
            _context = context;
        }

        public async Task Add(LoanProduct loanProduct)
        {
            await _context.LoanProduct.AddAsync(loanProduct);
            await _context.SaveChangesAsync();
        }

        public async Task<LoanProduct?> GetSingleLoanProductAsync(string productName)
        {

            return await _context.LoanProduct.FirstOrDefaultAsync(p => p.ProductName == productName);
        }
        public async Task<List<LoanProduct>> GetLoanProductsDetailAsync()
        {
            return await _context.LoanProduct.ToListAsync();
        }
    }
}