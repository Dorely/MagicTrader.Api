using MagicTrader.Core.Scryfall;
using System;

namespace MagicTrader.Loader
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            var scry = new ScryfallContext();

            var cards = await scry.GetScryfallCards();

        }
    }
}
