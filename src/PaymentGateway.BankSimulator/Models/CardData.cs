namespace PaymentGateway.BankSimulator.Models
{
    public class CardData
    {
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public string CardType { get; set; }
        public int ResponseCode { get; set; }
        public string Description { get; set; }
    }
}
