using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MagicTrader.Core.Context;
using MagicTrader.Core.Scryfall;
using Microsoft.Extensions.Hosting;

namespace MagicTrader.Core.DataLoad
{
    public class DataRefresher
    {
        private readonly IMagicSetContext _setContext;
        private readonly IMagicCardContext _cardContext;
        private readonly IScryfallContext _scryfallContext;
        const int msWaitTime = 100;
        private static Semaphore semaphore = new Semaphore(10, 10);
        private string _connectString;

        public DataRefresher(IMagicSetContext setContext, IMagicCardContext cardContext, IScryfallContext scryfallContext)
        {
            _setContext = setContext;
            _cardContext = cardContext;
            _scryfallContext = scryfallContext;
        }

        public async Task LoadFromScryfall(string connectString)
        {
            _connectString = connectString;
            await RefreshSets();
            await RefreshCards();

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
            await _cardContext.ResetTempTable();
            var pagenums = await _scryfallContext.GetScryfallPages();

            List<Task> tasklist = new List<Task>();
            for(int i = 1; i <= pagenums; i++)
            {
                var cardtask = RefreshCardPage(i);
                tasklist.Add(cardtask);
                Thread.Sleep(msWaitTime);
            }

            foreach(var task in tasklist)
            {
                task.Wait();
            }

            await _cardContext.MergeCards();
        }

        private async Task RefreshCardPage(int pagenum)
        {
            try
            {
                semaphore.WaitOne();
                Console.WriteLine($"{DateTime.Now}: Page {pagenum} starting");
                var cards = await _scryfallContext.GetScryfallCardPage(pagenum);

                var cardContext = new MagicCardContext(_connectString);

                await cardContext.InsertCardPage(cards);
                Console.WriteLine($"{DateTime.Now}: Page {pagenum} ending");
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw;
            }
            finally
            {
                semaphore.Release();
            }

        }
    }

}
