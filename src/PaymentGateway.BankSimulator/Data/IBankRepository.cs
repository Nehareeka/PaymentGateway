using PaymentGateway.BankSimulator.Models;

namespace PaymentGateway.BankSimulator.Data
{
    public interface IBankRepository
    {
        CardData GetCardData(string cardNumber);
    }
}
