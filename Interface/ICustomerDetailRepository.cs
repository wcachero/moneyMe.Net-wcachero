using WebApplication2.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication2.Repositories
{
    public interface ICustomerDetailRepository
    {
        Task<List<CustomerDetail>> GetCustomerDetailAsync();
        Task Add(CustomerDetail cusDetail);
        Task<CustomerDetail?> GetSingleCustomerDetailAsync(Guid id);
    }
}
