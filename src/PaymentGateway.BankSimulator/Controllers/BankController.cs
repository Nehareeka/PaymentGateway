using Microsoft.AspNetCore.Mvc;
using PaymentGateway.BankSimulator.Models;
using PaymentGateway.BankSimulator.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PaymentGateway.BankSimulator.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        private readonly IBankService _bankService;

        public BankController(IBankService bankService)
        {
            _bankService = bankService;
        }

        /// <summary>
        /// Validate and process the payment request
        /// </summary>
        /// <param name="request">Payment details</param>
        /// <returns>Status of the transaction</returns>
        [HttpPost("Process")]
        public BankProcessResponse Process(BankProcessRequest request)
        {
            var response = _bankService.ProcessPayment(request.CardNumber);
            return response;
        }
    }
}
