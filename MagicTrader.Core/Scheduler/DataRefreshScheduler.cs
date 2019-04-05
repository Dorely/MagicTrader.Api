using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MagicTrader.Core.Context;
using MagicTrader.Core.Scryfall;
using Microsoft.Extensions.Hosting;

namespace MagicTrader.Core.Scheduler
{
    public class DataRefreshScheduler : BackgroundService
    {
        private readonly IMagicSetContext _setContext;
        private readonly IMagicCardContext _cardContext;
        private readonly IScryfallContext _scryfallContext;
        const int refreshTimeMs = 24 * 60 * 60 * 1000;
        public DataRefreshScheduler(IMagicSetContext setContext, IMagicCardContext cardContext, IScryfallContext scryfallContext)
        {
            _setContext = setContext;
            _cardContext = cardContext;
            _scryfallContext = scryfallContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine("Refresh Task Beginning");
                await RefreshSets();
                await RefreshCards();
                Console.WriteLine("Refresh Task Finished");
                await Task.Delay(refreshTimeMs, stoppingToken);
            }
        }

        private async Task RefreshSets()
        {
            //sets
            await _setContext.ResetTempTable();
            var sets = await _scryfallContext.GetScryfallSets();
            await _setContext.InsertSetPage(sets);
            await _setContext.MergeSets();
        }

        private async Task RefreshCards()
        {
            //cards
            await _cardContext.ResetTempTable();
            //TODO split this up to something I can multithread
            var cards = await _scryfallContext.GetScryfallCards();
            await _cardContext.InsertCardPage(cards);
            await _cardContext.MergeCards();
        }
    }

}
