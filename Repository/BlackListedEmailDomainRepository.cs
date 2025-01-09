using LoanApplication.Interface;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Model;

namespace LoanApplication.Repository
{
    public class BlackListedEmailDomainRepository : IBlackListedEmailDomainRepository
    {
        private readonly MoneyMeDbContext _context;

        public BlackListedEmailDomainRepository(MoneyMeDbContext context)
        {
            _context = context;
        }

        public BlackListedEmailDomain GetById(Guid id)
        {
            return _context.BlackListedEmailDomain.FirstOrDefault(b => b.Id == id);
        }

        public async Task<List<BlackListedEmailDomain>> GetBlacklistedEmailDomain()
        {
            return await _context.BlackListedEmailDomain.ToListAsync();
        }

        public void Add(BlackListedEmailDomain blackListedEmailDomain)
        {
            _context.Add(blackListedEmailDomain);
            _context.SaveChanges();

        }

        public async Task<bool> Exists(string domainName)
        {
            var emailDomain = _context.BlackListedEmailDomain.FirstOrDefault(b => b.DomainName == domainName);
            if (emailDomain == null)
            {
                return false;
            }
            return true;

        }
    }
}
