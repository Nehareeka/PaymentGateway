using PaymentGateway.BankSimulator.Models;

namespace PaymentGateway.BankSimulator.Data
{
    public class BankRepository : IBankRepository
    {
        private readonly CardDbContext _dbContext;

        public BankRepository(CardDbContext dbContext)
        {
            _dbContext = dbContext;
            SeedData();
        }

        private void SeedData()
        {
            if (!_dbContext.CardData.Any())
            {
                _dbContext.CardData.Add(new CardData { CardNumber = "4024007103573027", CardType = "Visa", Description = "Invalid transaction", ResponseCode = 20012 });
                _dbContext.CardData.Add(new CardData { CardNumber = "4544249167673670", CardType = "Visa", Description = "Insufficient funds", ResponseCode = 20051 });
                _dbContext.CardData.Add(new CardData { CardNumber = "5279988405398834", CardType = "MasterCard", Description = "Restricted card", ResponseCode = 20062 });
                _dbContext.CardData.Add(new CardData { CardNumber = "5328090869100177", CardType = "MasterCard", Description = "Security violation", ResponseCode = 20063 });
                _dbContext.CardData.Add(new CardData { CardNumber = "4095254802642505", CardType = "Visa", Description = "Timeout", ResponseCode = 20068 });
                _dbContext.CardData.Add(new CardData { CardNumber = "4897453568485113", CardType = "Visa", Description = "Suspected fraud", ResponseCode = 20059 });
                _dbContext.CardData.Add(new CardData { CardNumber = "5420951756276171", CardType = "MasterCard", Description = "Activity amount limit exceeded", ResponseCode = 20061 });
                _dbContext.SaveChanges();
            }
        }

        public CardData GetCardData(string cardNumber)
        {
            return _dbContext.CardData.FirstOrDefault(card => card.CardNumber == cardNumber);
        }
    }
}
