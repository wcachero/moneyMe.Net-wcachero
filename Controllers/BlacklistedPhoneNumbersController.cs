using Microsoft.AspNetCore.Mvc;
using WebApplication2.Model;
using WebApplication2.Services;
using System.Collections.Generic;
using WebApplication2.Dtos;
using LoanApplication.Model;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlacklistedPhoneNumbersController : ControllerBase
    {
        private readonly IBlacklistedPhoneNumbersService _blackListedPhoneNumber;

        public BlacklistedPhoneNumbersController(IBlacklistedPhoneNumbersService blackListedPhoneNumber)
        {
            _blackListedPhoneNumber = blackListedPhoneNumber;
        }

        [HttpGet]
        public async Task<ActionResult<List<BlacklistedPhoneNumbers>>> GetBlacklistedPhoneNumbers()
        {
            var blacklistedPhoneNumbers = await _blackListedPhoneNumber.GetBlacklistedPhoneNumbers();
            return Ok(blacklistedPhoneNumbers);
        }

        // POST: api/BlacklistedPhoneNumbers
        [HttpPost]
        public async Task<ActionResult<BlacklistedPhoneNumbersDto>> PostBlacklistedPhoneNumbers(BlacklistedPhoneNumbersDto blacklistedPhoneNumberDto)
        {
            if (await _blackListedPhoneNumber.Exists(blacklistedPhoneNumberDto.PhoneNumber))
            {
                var errorResponse = new ErrorResponse
                {
                    Errors = new Dictionary<string, List<string>>
                        {
                            { "PhoneNumber", new List<string> { "Phone number already exists in the blacklist." } }
                         }
                };
                return BadRequest(errorResponse);
            }

            var blacklistedPhoneNumber = new BlacklistedPhoneNumbers
            {
                PhoneNumber = blacklistedPhoneNumberDto.PhoneNumber,
                isActive = blacklistedPhoneNumberDto.IsActive
            };

            _blackListedPhoneNumber.Add(blacklistedPhoneNumber);

            return CreatedAtAction(nameof(GetBlacklistedPhoneNumbers), new { id = blacklistedPhoneNumber.Id }, blacklistedPhoneNumberDto);
        }
    }
}