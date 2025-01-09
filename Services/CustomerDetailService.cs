using LoanApplication.Interface;
using LoanApplication.Model;
using WebApplication2.Model;
using WebApplication2.Repositories;

namespace WebApplication2.Services
{
    /// <summary>
    /// Developed by Wilson Cachero
    /// </summary>
    public class CustomerDetailService : ICustomerDetailService
    {
        private readonly ICustomerDetailRepository _customerdetailRepository;
        private readonly ILoanProductService _loanProductService;
        private readonly ILoanProductRepository _loanProductRepository;
        private readonly ICustomerLoanDetailRepository _customerLoanDetailRepository;
        private string customerLoanDetailMethod;

        public CustomerDetailService(
            ICustomerDetailRepository customerdetailRepository,
            ILoanProductService loanProductService,
            ILoanProductRepository loanProductRepository,
            ICustomerLoanDetailRepository customerLoanDetailRepository)
        {
            _customerdetailRepository = customerdetailRepository;
            _loanProductService = loanProductService;
            _loanProductRepository = loanProductRepository;
            _customerLoanDetailRepository = customerLoanDetailRepository;
        }

        public async Task<List<CustomerDetail>> GetCustomerDetailAsync()
        {
            return await _customerdetailRepository.GetCustomerDetailAsync();
        }

        public async Task<CustomerDetail?> GetSingleCustomerDetailAsync(Guid id)
        {
            return await _customerdetailRepository.GetSingleCustomerDetailAsync(id);
        }

        public async Task Add(CustomerDetail customerDetail)
        {
                await _customerdetailRepository.Add(customerDetail);
                await GetProductDetailsAsync(customerDetail.Product,customerDetail);  
        }

        public async Task AddCustomerLoanDetail(CustomerLoanDetails customerLoanDetails)
        {
            await _customerLoanDetailRepository.Add(customerLoanDetails);
        }

        public async Task<LoanProduct?> GetProductDetailsAsync(string productName, CustomerDetail customerDetail)
        {
            try
            {
                var productExists = await _loanProductService.ProductExistsAsync(productName);
                if (!productExists)
                {
                    throw new InvalidOperationException("Product does not exist.");
                }
                var productDetails = await _loanProductRepository.GetSingleLoanProductAsync(productName);
                customerLoanDetailMethod = "AddCustomerLoanDetail";
                await ComputeLoanAsync(customerDetail, productDetails);     
            }
            catch (Exception ex)
            { 
            }
            return await _loanProductRepository.GetSingleLoanProductAsync(productName);

        }

        #region "Update Customer Loan Detail"

        public async Task UpdateCustomerLoanDetailAsync(CustomerLoanDetails customerLoanDetails)
        {
            customerLoanDetailMethod = "UpdateCustomerLoanDetailAsync";
            var existingCustomerLoanDetail = await _customerLoanDetailRepository.GetSingleCustomerLoanDetailAsync(customerLoanDetails.Id);
            if (existingCustomerLoanDetail == null)
            {
                throw new KeyNotFoundException("Customer loan detail not found.");
            }
       
             await UpdateComputeLoanAsync(customerLoanDetails);
        }

        #endregion


        #region "Loan Computation"
        private async Task UpdateComputeLoanAsync(CustomerLoanDetails customerLoanDetails)

        {
            var existingCustomerLoanDetail = await _customerLoanDetailRepository.GetSingleCustomerLoanDetailAsync(customerLoanDetails.Id);
           var customerDetail = await _customerdetailRepository.GetSingleCustomerDetailAsync(existingCustomerLoanDetail.CustomerId);
            var productDetails = await _loanProductRepository.GetSingleLoanProductAsync(customerDetail.Product);
            existingCustomerLoanDetail.ProductId = productDetails.Id;
            existingCustomerLoanDetail.Product = productDetails;
            existingCustomerLoanDetail.FinanceAmount = customerLoanDetails.FinanceAmount;
            existingCustomerLoanDetail.Term = customerLoanDetails.Term;
            existingCustomerLoanDetail.DateUpdated = DateTimeOffset.Now;
 
            switch (existingCustomerLoanDetail.Product.ProductName)
            {
                case "ProductA":
                    // Custom logic for ProductA
                    existingCustomerLoanDetail.Repayment = CalculateRepayment(customerLoanDetails.FinanceAmount, productDetails.InterestRate, customerLoanDetails.Term);
                    existingCustomerLoanDetail.TotalRepayment = CalculateTotalRepayment(customerLoanDetails.FinanceAmount, productDetails.InterestRate, customerLoanDetails.Term);
                    existingCustomerLoanDetail.MonthlyAmortization = CalculateMonthlyAmortization(customerLoanDetails.FinanceAmount, productDetails.InterestRate, customerLoanDetails.Term);
                    break;
                case "ProductB":
                    // Custom logic for ProductB with 2 months no interest
                    existingCustomerLoanDetail.Repayment = CalculateRepaymentWithNoInterest(customerLoanDetails.FinanceAmount, productDetails.InterestRate, customerLoanDetails.Term, productDetails.InitialMonthNoInterest);
                   existingCustomerLoanDetail.TotalRepayment = CalculateTotalRepaymentWithNoInterest(customerLoanDetails.FinanceAmount, productDetails.InterestRate, customerLoanDetails.Term, productDetails.InitialMonthNoInterest);
                   existingCustomerLoanDetail.MonthlyAmortization = CalculateMonthlyAmortizationWithNoInterest(customerLoanDetails.FinanceAmount, productDetails.InterestRate, customerLoanDetails.Term, productDetails.InitialMonthNoInterest);
                    break;

                case "ProductC":
                    // Custom logic for ProductC
                    existingCustomerLoanDetail.Repayment = CalculateRepayment(customerLoanDetails.FinanceAmount, productDetails.InterestRate, customerLoanDetails.Term);
                    existingCustomerLoanDetail.TotalRepayment = CalculateTotalRepayment(customerLoanDetails.FinanceAmount, productDetails.InterestRate, customerLoanDetails.Term);
                    existingCustomerLoanDetail.MonthlyAmortization = CalculateMonthlyAmortization(customerLoanDetails.FinanceAmount, productDetails.InterestRate, customerLoanDetails.Term);
                    break;
                default:
                    // Default logic
                    existingCustomerLoanDetail.Repayment = CalculateRepayment(customerLoanDetails.FinanceAmount, productDetails.InterestRate, customerLoanDetails.Term);
                    existingCustomerLoanDetail.TotalRepayment = CalculateTotalRepayment(customerLoanDetails.FinanceAmount, productDetails.InterestRate, customerLoanDetails.Term);
                    existingCustomerLoanDetail.MonthlyAmortization = CalculateMonthlyAmortization(customerLoanDetails.FinanceAmount, productDetails.InterestRate, customerLoanDetails.Term);
                    break;
            }
            await _customerLoanDetailRepository.Update(existingCustomerLoanDetail);
        }

        #endregion

        #region "Loan Computation"
        private async Task ComputeLoanAsync(CustomerDetail customerDetail, LoanProduct productDetails)
        {
                var customerLoanDetail = new CustomerLoanDetails()
                {
                    Id = Guid.NewGuid(),
                    CustomerId = customerDetail.Id,
                    Customer = customerDetail,
                    Product = productDetails,
                    ProductId = productDetails.Id,
                    Term = customerDetail.Term,
                    FinanceAmount = customerDetail.RequiredAmount,
                    Repayment = CalculateRepayment(customerDetail.RequiredAmount, productDetails.InterestRate, customerDetail.Term),
                    TotalRepayment = CalculateTotalRepayment(customerDetail.RequiredAmount, productDetails.InterestRate, customerDetail.Term),
                    MonthlyAmortization = CalculateMonthlyAmortization(customerDetail.RequiredAmount, productDetails.InterestRate, customerDetail.Term)
                };

                switch (customerDetail.Product)
                {
                    case "ProductA":
                        // Custom logic for ProductA
                        customerLoanDetail.Repayment = CalculateRepayment(customerDetail.RequiredAmount, productDetails.InterestRate, customerDetail.Term);
                        customerLoanDetail.TotalRepayment = CalculateTotalRepayment(customerDetail.RequiredAmount, productDetails.InterestRate, customerDetail.Term);
                        customerLoanDetail.MonthlyAmortization = CalculateMonthlyAmortization(customerDetail.RequiredAmount, productDetails.InterestRate, customerDetail.Term);
                        break;
                    case "ProductB":
                        // Custom logic for ProductB with 2 months no interest
                        customerLoanDetail.Repayment = CalculateRepaymentWithNoInterest(customerDetail.RequiredAmount, productDetails.InterestRate, customerDetail.Term, productDetails.InitialMonthNoInterest);
                        customerLoanDetail.TotalRepayment = CalculateTotalRepaymentWithNoInterest(customerDetail.RequiredAmount, productDetails.InterestRate, customerDetail.Term, productDetails.InitialMonthNoInterest);
                        customerLoanDetail.MonthlyAmortization = CalculateMonthlyAmortizationWithNoInterest(customerDetail.RequiredAmount, productDetails.InterestRate, customerDetail.Term, productDetails.InitialMonthNoInterest);
                        break;

                    case "ProductC":
                    // Custom logic for ProductC
                    customerLoanDetail.Repayment = CalculateRepayment(customerDetail.RequiredAmount, productDetails.InterestRate, customerDetail.Term);
                    customerLoanDetail.TotalRepayment = CalculateTotalRepayment(customerDetail.RequiredAmount, productDetails.InterestRate, customerDetail.Term);
                    customerLoanDetail.MonthlyAmortization = CalculateMonthlyAmortization(customerDetail.RequiredAmount, productDetails.InterestRate, customerDetail.Term);
                    break;
                default:
                        // Default logic
                        customerLoanDetail.Repayment = CalculateRepayment(customerDetail.RequiredAmount, productDetails.InterestRate, customerDetail.Term);
                        customerLoanDetail.TotalRepayment = CalculateTotalRepayment(customerDetail.RequiredAmount, productDetails.InterestRate, customerDetail.Term);
                        customerLoanDetail.MonthlyAmortization = CalculateMonthlyAmortization(customerDetail.RequiredAmount, productDetails.InterestRate, customerDetail.Term);
                        break;
                }
                await AddCustomerLoanDetail(customerLoanDetail);
        }

        #endregion


        #region "Loan Computation Methods"
        private decimal CalculateRepayment(decimal requiredAmount, decimal interestRate, int term)
        {
            if (interestRate == 0)
            {
                return requiredAmount / term;
            }
            return (requiredAmount * (1 + interestRate / 100)) / term;
        }

        private decimal CalculateTotalRepayment(decimal requiredAmount, decimal interestRate, int term)
        {
            if (interestRate == 0)
            {
                return requiredAmount;
            }
            return requiredAmount * (1 + interestRate / 100);
        }

        private decimal CalculateRepaymentWithNoInterest(decimal requiredAmount, decimal interestRate, int term, int initialMonthNoInterest)
        {
            if (interestRate == 0)
            {
                return requiredAmount / term;
            }
            // Calculate repayment with initial months having no interest
            var interestTerm = term - initialMonthNoInterest;
            var interestAmount = requiredAmount * (interestRate / 100) * interestTerm;
            return (requiredAmount + interestAmount) / term;
        }

        private decimal CalculateTotalRepaymentWithNoInterest(decimal requiredAmount, decimal interestRate, int term, int initialMonthNoInterest)
        {
            if (interestRate == 0)
            {
                return requiredAmount;
            }
            // Calculate total repayment with initial months having no interest
            var interestTerm = term - initialMonthNoInterest;
            var interestAmount = requiredAmount * (interestRate / 100) * interestTerm;
            return requiredAmount + interestAmount;
        }

        private decimal CalculateMonthlyAmortization(decimal requiredAmount, decimal interestRate, int term)
        {
            if (interestRate == 0)
            {
                return requiredAmount / term;
            }
            // Calculate monthly amortization
            var monthlyInterestRate = interestRate / 12 / 100;
            return requiredAmount * monthlyInterestRate / (1 - (decimal)Math.Pow((double)(1 + monthlyInterestRate), -term));
        }

        private decimal CalculateMonthlyAmortizationWithNoInterest(decimal requiredAmount, decimal interestRate, int term, int initialMonthNoInterest)
        {
            if (interestRate == 0)
            {
                return requiredAmount / term;
            }
            // Calculate monthly amortization with initial months having no interest
            var interestTerm = term - initialMonthNoInterest;
            var monthlyInterestRate = interestRate / 12 / 100;
            var interestAmount = requiredAmount * monthlyInterestRate / (1 - (decimal)Math.Pow((double)(1 + monthlyInterestRate), -interestTerm));
            return (requiredAmount / term) + interestAmount;
        }
    }
    #endregion
}