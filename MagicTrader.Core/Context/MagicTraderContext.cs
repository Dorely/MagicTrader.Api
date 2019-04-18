using MagicTrader.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicTrader.Core.Context
{
    public class MagicTraderContext : DbContext
    {
        public MagicTraderContext()
        {
        }

        public MagicTraderContext(DbContextOptions<MagicTraderContext> options) : base(options)
        {
        }

        private string _connectString;
        public MagicTraderContext(string connectString) : base()
        {
            _connectString = connectString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectString);
            }
        }

        public DbSet<MagicSet> MagicSets { get; set; }
        public DbSet<MagicCard> MagicCards { get; set; }
        public DbSet<MagicSet_Temp> MagicSets_Temp { get; set; }
        public DbSet<MagicCard_Temp> MagicCards_Temp { get; set; }

    }
}
