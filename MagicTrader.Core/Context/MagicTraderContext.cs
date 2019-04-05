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

        public DbSet<MagicSet> MagicSets { get; set; }
        public DbSet<MagicCard> MagicCards { get; set; }
        public DbSet<MagicSet_Temp> MagicSets_Temp { get; set; }
        public DbSet<MagicCard_Temp> MagicCards_Temp { get; set; }

    }
}
