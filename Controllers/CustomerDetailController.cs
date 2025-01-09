using Microsoft.AspNetCore.Mvc;
using WebApplication2.Model;
using WebApplication2.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication2.Dtos;
using LoanApplication.Interface;
using LoanApplication.Dto;
using LoanApplication.Model;

namespace WebApplication2.Controllers
{/// <summary>
/// Developed by: Wilson Cachero
/// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerDetailController : ControllerBase
    {
        private readonly ICustomerDetailService _cusDetailService;
        private readonly IBlacklistedPhoneNumbersService _blacklistedPhoneNumbersService;
        private readonly IBlackListedEmaiDomainService _blackListedEmaiDomain;
        private readonly ICustomerLoanDetailRepository _customerLoanDetailRepository;

        public CustomerDetailController(
            ICustomerDetailService cusDetailService,
            IBlacklistedPhoneNumbersService blacklistedPhoneNumbersService,
            IBlackListedEmaiDomainService blackListedEmaiDomainService,
            ICustomerLoanDetailRepository customerLoanDetailRepository

            )
        {
            _cusDetailService = cusDetailService;
            _blacklistedPhoneNumbersService = blacklistedPhoneNumbersService;
            _blackListedEmaiDomain = blackListedEmaiDomainService;
            _customerLoanDetailRepository = customerLoanDetailRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<CustomerDetail>>> GetCustomerDetail()
        {
            var customerDetails = await _cusDetailService.GetCustomerDetailAsync();
            return Ok(customerDetails);
        }

        [HttpGet]
        [Route("single/{id}")]
        public async Task<ActionResult<CustomerDetail>> GetSingleCustomerDetail(Guid id)
        {
            var customerDetail = await _cusDetailService.GetSingleCustomerDetailAsync(id);
            return Ok(customerDetail);
        }
   
        [HttpGet]
        [Route("loanDetail/{id}")]
        public async Task<ActionResult<CustomerLoanDetails>> GetSingleCustomerLoanDetail(Guid id)
        {
            var customerDetail = await _customerLoanDetailRepository.GetSingleCustomerLoanDetailAsync(id);
            return Ok(customerDetail);
        }

        [HttpPost]
        public async Task<ActionResult> CreateQoute(CustomerDetailDto customerDetailDto)
        {
                var customerDetail = new CustomerDetail
            {
                RequiredAmount = customerDetailDto.RequiredAmount,
                Term = customerDetailDto.Term,
                Title = customerDetailDto.Title,
                FirstName = customerDetailDto.FirstName,
                LastName = customerDetailDto.LastName,
                DateofBirth = customerDetailDto.DateofBirth,
                Mobile = customerDetailDto.Mobile,
                Email = customerDetailDto.Email,
                Product = customerDetailDto.Product,
                DateAdded = DateTimeOffset.Now
            };

           await _cusDetailService.Add(customerDetail);

            return CreatedAtAction(nameof(GetCustomerDetail), new { id = customerDetail.Id }, customerDetail);
        }
        [HttpPut]
        [Route("loanDetail")]
        public async Task<ActionResult> UpdateCustomerLoanDetail(CustomerLoanDetaiDto customerLoanDetail)
        {
            var customerLoanDetails = await _customerLoanDetailRepository.GetSingleCustomerLoanDetailAsync(customerLoanDetail.Id);
            CustomerLoanDetails newcustomerLoanDetails = new CustomerLoanDetails
            {
                Id = customerLoanDetail.Id,
                Term = customerLoanDetail.Term,
                FinanceAmount = customerLoanDetail.FinanceAmount

            };

            await _cusDetailService.UpdateCustomerLoanDetailAsync(newcustomerLoanDetails);
            return Ok( await _customerLoanDetailRepository.GetSingleCustomerLoanDetailAsync(customerLoanDetail.Id));
         
        }
    }

}


