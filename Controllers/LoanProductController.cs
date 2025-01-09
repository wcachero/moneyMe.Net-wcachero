using LoanApplication.Interface;
using LoanApplication.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanProductController : ControllerBase
    {
        private readonly ILoanProductService _loanProductService;

        public LoanProductController(ILoanProductService loanProductService)
        {
            _loanProductService = loanProductService;
        }

        [HttpPost]
        public async Task<ActionResult> AddLoanProduct(LoanProduct loanProduct)
        {
            loanProduct.Id = Guid.NewGuid();
            await _loanProductService.Add(loanProduct);
            return CreatedAtAction(nameof(AddLoanProduct), new { id = loanProduct.Id }, loanProduct);
        }

        [HttpGet]
        public async Task<ActionResult<List<LoanProduct>>> GetLoanProducts()
        {
            var loanProducts = await _loanProductService.GetLoanProductsDetailAsync();
            return Ok(loanProducts);
        }

        [HttpGet]
        [Route("product/{ProductName}")]
        public async Task<ActionResult<LoanProduct>> GetSingleLoanProduct(string productName)
        {
            var loanProducts = await _loanProductService.GetSingleLoanProductDetailAsync(productName);
            return Ok(loanProducts);
        }
    }
}
