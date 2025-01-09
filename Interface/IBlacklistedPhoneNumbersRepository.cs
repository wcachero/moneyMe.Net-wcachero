using System;
using System.Collections.Generic;
using WebApplication2.Model;

namespace WebApplication2.Repositories
{
    public interface IBlacklistedPhoneNumbersRepository
    {
        Task<List<BlacklistedPhoneNumbers>> GetBlacklistedPhoneNumbers();
        BlacklistedPhoneNumbers GetById(Guid id);
        void Add(BlacklistedPhoneNumbers blacklistedPhoneNumber);
        Task<bool> Exists(string phoneNumber);
    }
}
