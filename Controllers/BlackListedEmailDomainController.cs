using LoanApplication.Dto;
using LoanApplication.Interface;
using LoanApplication.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Dtos;
using WebApplication2.Model;
using WebApplication2.Services;

namespace LoanApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlackListedEmailDomainController : ControllerBase
    {
        private readonly IBlackListedEmaiDomainService _blackListedEmaiDomainService;

        public BlackListedEmailDomainController(IBlackListedEmaiDomainService blackListedEmaiDomainService)
        {
            _blackListedEmaiDomainService = blackListedEmaiDomainService;
        }


        [HttpGet]
        public async Task<ActionResult<List<BlackListedEmailDomain>>> GetBlacklistedEmailDomain()
        {
            var blacklistedPhoneNumbers = await _blackListedEmaiDomainService.GetBlacklistedEmailDomain();
            return Ok(blacklistedPhoneNumbers);
        }

   
        [HttpPost]
        public async Task<ActionResult<BlackListedEmailDomainDto>> PostBlacklistedEmailDomain(BlackListedEmailDomainDto blackListedEmailDomainDto)
        {
            string domainName = blackListedEmailDomainDto.DomainName.Contains('@')
               ? blackListedEmailDomainDto.DomainName.Split('@')[1]
               : blackListedEmailDomainDto.DomainName;

            if (await _blackListedEmaiDomainService.Exists(domainName))
            {
                var errorResponse = new ErrorResponse
                {
                    Errors = new Dictionary<string, List<string>>
                        {
                            { "email", new List<string> { "Email domain name already exists in the blacklist." } }
                         }
                };
                return BadRequest(errorResponse);
            }

            var blackListedEmailDomain = new BlackListedEmailDomain()
            {
                DomainName = domainName,
                isActive = blackListedEmailDomainDto.IsActive
            };

            _blackListedEmaiDomainService.Add(blackListedEmailDomain);

            return CreatedAtAction(nameof(GetBlacklistedEmailDomain), new { id = blackListedEmailDomain.Id }, blackListedEmailDomainDto);
        }
    }
}

