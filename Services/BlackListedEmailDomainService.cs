using LoanApplication.Interface;
using WebApplication2.Model;
using WebApplication2.Repositories;

namespace LoanApplication.Services
{
    public class BlackListedEmailDomainService : IBlackListedEmaiDomainService
    {
        private readonly IBlackListedEmailDomainRepository _blackListedEmailDomainRepository;

        public BlackListedEmailDomainService(IBlackListedEmailDomainRepository blackListedEmailDomainRepository)
        {
            _blackListedEmailDomainRepository = blackListedEmailDomainRepository;
        }

        public async Task<List<BlackListedEmailDomain>> GetBlacklistedEmailDomain()
        {
            return await _blackListedEmailDomainRepository.GetBlacklistedEmailDomain();
        }

        public BlackListedEmailDomain GetById(Guid id)
        {
            return _blackListedEmailDomainRepository.GetById(id);
        }

        public void Add(BlackListedEmailDomain blackListedEmailDomain)
        {
            _blackListedEmailDomainRepository.Add(blackListedEmailDomain);
        }

        public async Task<bool> Exists(string emailDomain)
        {
            return await _blackListedEmailDomainRepository.Exists(emailDomain);
        }
    }
}
