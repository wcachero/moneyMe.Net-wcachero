using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication2.Repositories
{
    public class CustomerDetailRepository : ICustomerDetailRepository
    {
        private readonly MoneyMeDbContext _context;

        public CustomerDetailRepository(MoneyMeDbContext context)
        {
            _context = context;
        }

        public async Task<List<CustomerDetail>> GetCustomerDetailAsync()
        {
            return await _context.CustomerDetail.ToListAsync();
        }

        public async Task<CustomerDetail?> GetSingleCustomerDetailAsync(Guid id)
        {
            return await _context.CustomerDetail.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task Add(CustomerDetail cusDetail)
        {
             await _context.CustomerDetail.AddAsync(cusDetail);
            await _context.SaveChangesAsync();

        }
    }
}