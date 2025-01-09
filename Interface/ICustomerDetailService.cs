using WebApplication2.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using LoanApplication.Model;

namespace WebApplication2.Services
{
    public interface ICustomerDetailService
    {
        Task<List<CustomerDetail>> GetCustomerDetailAsync();
        Task<CustomerDetail?> GetSingleCustomerDetailAsync(Guid id);
        Task Add(CustomerDetail customerDetail);
        Task AddCustomerLoanDetail(CustomerLoanDetails customerLoanDetails);
        Task<LoanProduct> GetProductDetailsAsync(string productName, CustomerDetail customerDetail);
        Task UpdateCustomerLoanDetailAsync(CustomerLoanDetails customerLoanDetails);
    }
}
