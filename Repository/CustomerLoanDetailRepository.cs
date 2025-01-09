using LoanApplication.Interface;
using LoanApplication.Model;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Model;

namespace LoanApplication.Repository
{
    public class CustomerLoanDetailRepository : ICustomerLoanDetailRepository
    {
        private readonly MoneyMeDbContext _MMcontext;

        public CustomerLoanDetailRepository(MoneyMeDbContext MMcontext)
        {
            _MMcontext = MMcontext;
        }

        public async Task<List<CustomerLoanDetails>> GetCustomerLoanDetailAsync()
        {
            return await _MMcontext.CustomerLoanDetails.ToListAsync();
        }

        public async Task Update(CustomerLoanDetails customerLoanDetails)
        {
          
            _MMcontext.CustomerLoanDetails.Update(customerLoanDetails);
            await _MMcontext.SaveChangesAsync();
        }

        public async Task Add(CustomerLoanDetails cusLoanDetail)
        {
            try
            {
                await _MMcontext.CustomerLoanDetails.AddAsync(cusLoanDetail);
                await _MMcontext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., log it)
                throw new Exception("An error occurred while saving the customer loan detail.", ex);
            }
        }

        public async Task<CustomerLoanDetails?> GetSingleCustomerLoanDetailAsync(Guid id)
        {

            return await _MMcontext.CustomerLoanDetails
                            .Include(cld => cld.Customer)
        .Include(cld => cld.Product)
        .FirstOrDefaultAsync(cld => cld.Id == id || cld.CustomerId == id);

            ;
        }
    }
}