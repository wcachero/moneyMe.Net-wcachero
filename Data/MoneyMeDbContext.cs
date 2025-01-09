using LoanApplication.Model;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Model;

namespace WebApplication2.Data
{
    public class MoneyMeDbContext : DbContext
    {
        public MoneyMeDbContext(DbContextOptions<MoneyMeDbContext> options) : base(options)
        {
            CustomerDetail = Set<CustomerDetail>();
            BlackListedEmailDomain = Set<BlackListedEmailDomain>();
            BlacklistedPhoneNumbers = Set<BlacklistedPhoneNumbers>();
            LoanProduct = Set<LoanProduct>();
            CustomerLoanDetails = Set<CustomerLoanDetails>();
        }

        public DbSet<CustomerDetail> CustomerDetail { get; set; }
        public DbSet<BlackListedEmailDomain> BlackListedEmailDomain { get; set; }
        public DbSet<BlacklistedPhoneNumbers> BlacklistedPhoneNumbers { get; set; }
        public DbSet<LoanProduct> LoanProduct { get; set; }
        public DbSet<CustomerLoanDetails> CustomerLoanDetails { get; set; }
    }
}
