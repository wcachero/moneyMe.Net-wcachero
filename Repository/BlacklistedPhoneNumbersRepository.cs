using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication2.Data;
using WebApplication2.Model;

namespace WebApplication2.Repositories
{
    public class BlacklistedPhoneNumbersRepository : IBlacklistedPhoneNumbersRepository
    {
        private readonly MoneyMeDbContext _context;

        public BlacklistedPhoneNumbersRepository(MoneyMeDbContext context)
        {
            _context = context;
        }
    
        public BlacklistedPhoneNumbers GetById(Guid id)
        {
            return _context.BlacklistedPhoneNumbers.FirstOrDefault(b => b.Id == id);
        }

        public async Task<List<BlacklistedPhoneNumbers>> GetBlacklistedPhoneNumbers()
        {
            return await _context.BlacklistedPhoneNumbers.ToListAsync();
        }

        public void Add(BlacklistedPhoneNumbers blacklistedPhoneNumber)
        {
            _context.Add(blacklistedPhoneNumber);
            _context.SaveChanges();

        }

        public async Task<bool> Exists(string phoneNumber)
        {
            var blacklistedPhoneNumber = _context.BlacklistedPhoneNumbers.FirstOrDefault(b => b.PhoneNumber == phoneNumber);
            if (blacklistedPhoneNumber == null)
            {
                return  false;
            }
            return  true;

        }
    }
}
