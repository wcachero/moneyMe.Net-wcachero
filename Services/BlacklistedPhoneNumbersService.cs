using System;
using System.Collections.Generic;
using WebApplication2.Model;
using WebApplication2.Repositories;

namespace WebApplication2.Services
{
    public class BlacklistedPhoneNumbersService : IBlacklistedPhoneNumbersService
    {
        private readonly IBlacklistedPhoneNumbersRepository _blacklistedPhoneNumbersRepository;

        public BlacklistedPhoneNumbersService(IBlacklistedPhoneNumbersRepository blacklistedPhoneNumbersRepository)
        {
            _blacklistedPhoneNumbersRepository = blacklistedPhoneNumbersRepository;
        }

        public async Task<List<BlacklistedPhoneNumbers>> GetBlacklistedPhoneNumbers()
        {
            return await _blacklistedPhoneNumbersRepository.GetBlacklistedPhoneNumbers();
        }

        public BlacklistedPhoneNumbers GetById(Guid id)
        {
            return _blacklistedPhoneNumbersRepository.GetById(id);
        }

        public void Add(BlacklistedPhoneNumbers blacklistedPhoneNumber)
        {
            _blacklistedPhoneNumbersRepository.Add(blacklistedPhoneNumber);
        }

        public async Task<bool> Exists(string phoneNumber)
        {
            return await _blacklistedPhoneNumbersRepository.Exists(phoneNumber);
        }
    }
}
