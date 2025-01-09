using WebApplication2.Model;

namespace LoanApplication.Interface
{
    public interface IBlackListedEmaiDomainService
    {
        Task<List<BlackListedEmailDomain>> GetBlacklistedEmailDomain();
        BlackListedEmailDomain GetById(Guid id);
        void Add(BlackListedEmailDomain blacklistedEmailDomain);
        Task<bool> Exists(string emailDomain);
    }
}
