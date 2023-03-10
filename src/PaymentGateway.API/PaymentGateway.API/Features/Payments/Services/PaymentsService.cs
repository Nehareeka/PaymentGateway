using PaymentGateway.API.Payments.Data;
using PaymentGateway.API.Models;
using System.Text.Json;
using PaymentGateway.API.Exceptions;

namespace PaymentGateway.API.Services
{
    public class PaymentsService : IPaymentsService
    {
        private readonly IPaymentRepository paymentRepo;
        private readonly HttpClient httpClient;
        private readonly ILogger<PaymentsService> logger;

        public PaymentsService(IPaymentRepository paymentRepo, IHttpClientFactory httpClientFactory, ILogger<PaymentsService> logger)
        {
            this.paymentRepo = paymentRepo;
            this.httpClient = httpClientFactory.CreateClient("BankSimulator");
            this.logger = logger;
        }

        /// <summary>
        /// Fetch payment details from database
        /// </summary>
        /// <param name="id">Payment Id</param>
        /// <returns>Details of the payment or null if no such payment exists</returns>
        public async Task<PaymentDetails> GetPaymentDetailsAsync(string id)
        {
            try
            {
                logger?.LogInformation("Fetching Payments");
                PaymentDetails responsePayment = await paymentRepo.GetPayment(id);
                if (responsePayment == null)
                {
                    return null;
                }
                return responsePayment;
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                throw new CardDetailsException($"Could not fetch details for payment Id : {id}", ex);

            }
        }

        /// <summary>
        /// Send request to bank to process payment
        /// </summary>
        /// <param name="request">Card payment request details</param>
        /// <returns>Success or Failure response</returns>
        /// <exception cref="CardDetailsException"></exception>
        public async Task<ProcessPaymentResponse> ProcessPaymentAsync(ProcessPaymentRequest request)
        {
            try
            {
                logger?.LogInformation("Processing Payment");
                var response = await httpClient.PostAsJsonAsync("/bank/process", request);

                var content = await response.Content.ReadAsByteArrayAsync();
                var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                var result = JsonSerializer.Deserialize<ProcessPaymentResponse>(content, options);

                //store in db
                var paymentDetails = new PaymentDetails()
                {
                    Amount = request.Amount,
                    Currency = request.Currency,
                    MerchantId = request.MerchantId,
                    PaymentTimeStamp = DateTime.Now,
                    Status = result.Status,
                    Id = result.PaymentId,
                    MaskedCardNumber = request.CardNumber.Substring(request.CardNumber.Length-4, 4),
                    CustomerName = request.CustomerName,
                    StatusMessage = result.Message
                };
                await paymentRepo.AddPayment(paymentDetails);

                if (!response.IsSuccessStatusCode)
                {
                    throw new CardDetailsException(result.Message);
                }

                if(result.Status != "Paid")
                {
                    throw new CardDetailsException($"PaymentId {result.PaymentId} {result.Status}: {result.Message}");
                }

                return result;
            }
            catch(CardDetailsException ex)
            {
                logger?.LogError(ex.ToString());
                throw ex;
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                throw new CardDetailsException("Payment Failed. Please select another payment method or try again later.", ex);
            }
        }
    }
}
