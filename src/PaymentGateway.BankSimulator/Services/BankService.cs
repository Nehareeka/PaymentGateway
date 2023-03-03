using PaymentGateway.BankSimulator.Data;
using PaymentGateway.BankSimulator.Models;

namespace PaymentGateway.BankSimulator.Services
{
    public class BankService : IBankService
    {
        private readonly IBankRepository _bankRepo;

        public BankService(IBankRepository bankRepository)
        {
            this._bankRepo = bankRepository;
        }

        public BankProcessResponse ProcessPayment(string cardNumber)
        {
            string paymentId = Guid.NewGuid().ToString();
            var responseCode = _bankRepo.GetCardData(cardNumber);

            var message = $"Payment with Id {paymentId} Created";
            if (responseCode == null)
            {
                //transaction succeeded
                return new BankProcessResponse(paymentId, GetPaymentStatusName(PaymentStatus.Paid), "Payment Success");
            }
            return new BankProcessResponse(paymentId, GetPaymentStatus(responseCode.ResponseCode), responseCode.Description);
        }

        private string GetPaymentStatus(int responseCode)
        {
            PaymentStatus status;
            switch (responseCode)
            {
                case 20012:
                case 20051:
                case 20062:
                case 20063:
                case 20059:
                case 20061: status = PaymentStatus.Declined;
                    break;
                case 20068: status = PaymentStatus.Expired;
                    break;
                default: status = PaymentStatus.Pending;
                    break;
            }
            return GetPaymentStatusName(status);
        }

        private string GetPaymentStatusName(PaymentStatus paymentStatus){
            return Enum.GetName(typeof(PaymentStatus), paymentStatus);
        }
    }
}
