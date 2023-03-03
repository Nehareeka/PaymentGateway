using PaymentGateway.BankSimulator.Models;

namespace PaymentGateway.BankSimulator.Services
{
    public interface IBankService
    {
        BankProcessResponse ProcessPayment(string cardNumber);
    }
}
