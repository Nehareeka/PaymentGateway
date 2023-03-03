using Microsoft.EntityFrameworkCore;
using PaymentGateway.BankSimulator.Models;

namespace PaymentGateway.BankSimulator.Data
{
    public class CardDbContext : DbContext
    {
        public DbSet<CardData> CardData { get; set; }
        public CardDbContext(DbContextOptions options) : base(options) { }
    }
}
