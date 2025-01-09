using System.Collections.Generic;
using WebApplication2.Model;

namespace WebApplication2.Services
{
    public interface IBlacklistedPhoneNumbersService
    {
        Task<List<BlacklistedPhoneNumbers>> GetBlacklistedPhoneNumbers();
        BlacklistedPhoneNumbers GetById(Guid id);
        void Add(BlacklistedPhoneNumbers blacklistedPhoneNumber); 
        Task<bool> Exists(string phoneNumber);
    }
}
