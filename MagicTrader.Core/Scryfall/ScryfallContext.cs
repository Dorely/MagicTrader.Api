using MagicTrader.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MagicTrader.Core.Scryfall
{
    public interface IScryfallContext
    {
        Task<List<MagicSet_Temp>> GetScryfallSets();
        Task<List<MagicCard_Temp>> GetScryfallCards();
    }

    public class ScryfallContext : IScryfallContext
    {
        public async Task<List<MagicCard_Temp>> GetScryfallCards()
        {
            try
            {
                bool hasMore = true;
                var cardUri = "https://api.scryfall.com/cards";
                var cardsList = new List<MagicCard_Temp>();
                using (var client = new HttpClient())
                {
                    while (hasMore && !String.IsNullOrEmpty(cardUri))
                    {
                        Console.WriteLine(cardUri);
                        var response = await client.GetAsync(cardUri);
                        if (response.IsSuccessStatusCode)
                        {
                            var stopwatch = new Stopwatch();
                            stopwatch.Start();

                            var cardsJson = (await response.Content.ReadAsStringAsync());
                            var scryfallCardsList = JsonConvert.DeserializeObject<ScryfallCards>(cardsJson);
                            hasMore = scryfallCardsList.HasMore;
                            cardUri = scryfallCardsList.NextPage;

                            foreach (var scryfallCard in scryfallCardsList.Cards)
                            {
                                int collectorNumber = 0;
                                if (!string.IsNullOrEmpty(scryfallCard.CollectorNumber))
                                {
                                    int.TryParse(scryfallCard.CollectorNumber, out collectorNumber);
                                }

                                float priceUsd = 0;
                                if (!string.IsNullOrEmpty(scryfallCard.Prices.Usd))
                                {
                                    float.TryParse(scryfallCard.Prices.Usd, out priceUsd);
                                }

                                string imageUri = null;
                                if(scryfallCard.ImageUris != null)
                                {
                                    imageUri = scryfallCard.ImageUris.Normal;
                                }

                                string purchaseUri = null;
                                if (scryfallCard.PurchaseUris != null)
                                {
                                    purchaseUri = scryfallCard.PurchaseUris.Tcgplayer;
                                }

                                var card = new MagicCard_Temp()
                                {
                                    ScryfallId = scryfallCard.Id,
                                    SetCode = scryfallCard.Set,
                                    OracleId = scryfallCard.OracleId,
                                    MultiverseId = null,
                                    CardName = scryfallCard.Name,
                                    ApiUri = scryfallCard.Uri,
                                    ImageUri = imageUri,
                                    TypeLine = scryfallCard.TypeLine,
                                    OracleText = scryfallCard.OracleText,
                                    ManaCost = scryfallCard.ManaCost,
                                    CollectorNumber = collectorNumber,
                                    Rarity = scryfallCard.Rarity,
                                    FlavorText = scryfallCard.FlavorText,
                                    Artist = scryfallCard.Artist,
                                    PurchaseUri = purchaseUri,
                                    PriceUsd = priceUsd
                                };
                                cardsList.Add(card);

                            }

                            if (stopwatch.ElapsedMilliseconds < 50)
                            {
                                System.Threading.Thread.Sleep(50);
                            }
                        }
                    }
                }
                return cardsList;
            }
            catch(Exception e)
            {

                throw e;
            }
            
        }

        public async Task<List<MagicSet_Temp>> GetScryfallSets()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync("https://api.scryfall.com/sets");
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var setsJson = (await response.Content.ReadAsStringAsync());
                var scryfallSetsList = JsonConvert.DeserializeObject<ScryfallSets>(setsJson);
                var setsList = new List<MagicSet_Temp>();
                foreach (var scryfallSet in scryfallSetsList.data)
                {
                    var set = new MagicSet_Temp()
                    {
                        SetCode = scryfallSet.code,
                        SetName = scryfallSet.name,
                        ReleaseDate = DateTime.Parse(scryfallSet.released_at),
                        ApiUri = scryfallSet.scryfall_uri,
                        IconSvgUri = scryfallSet.icon_svg_uri
                    };

                    setsList.Add(set);
                }
                return setsList;
                
            }
        }
    }
}
