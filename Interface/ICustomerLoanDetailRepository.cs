using LoanApplication.Model;
using WebApplication2.Model;

namespace LoanApplication.Interface
{
    public interface ICustomerLoanDetailRepository
    {
        Task<List<CustomerLoanDetails>> GetCustomerLoanDetailAsync();
        Task Add(CustomerLoanDetails cusDetail);
        Task<CustomerLoanDetails?> GetSingleCustomerLoanDetailAsync(Guid id);
        Task Update(CustomerLoanDetails customerLoanDetails);

    }
}
