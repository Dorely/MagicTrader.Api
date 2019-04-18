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
        Task<int> GetScryfallPages();
        Task<List<MagicCard_Temp>> GetScryfallCardPage(int pagenum);
    }

    public class ScryfallContext : IScryfallContext
    {
        private const string scryfallCardUri = "https://api.scryfall.com/cards?page=";
        private readonly HttpClient _client;

        public ScryfallContext(HttpClient client)
        {
            _client = client;
        }

        public async Task<int> GetScryfallPages()
        {
            var uri = $"{scryfallCardUri}1";
            var response = await _client.GetAsync(uri);
            var cardsJson = (await response.Content.ReadAsStringAsync());
            var scryfallCardsList = JsonConvert.DeserializeObject<ScryfallCards>(cardsJson);

            var totalCards = scryfallCardsList.TotalCards;
            var pagenumber = (totalCards / 175) + 1;
            return pagenumber;
        }


        public async Task<List<MagicCard_Temp>> GetScryfallCardPage(int pagenum)
        {
            var uri = $"{scryfallCardUri}{pagenum}";
            var response = await _client.GetAsync(uri);
            var byteArray = response.Content.ReadAsByteArrayAsync().Result;
            var cardsJson = Encoding.UTF8.GetString(byteArray, 0, byteArray.Length);
            var scryfallCardsList = JsonConvert.DeserializeObject<ScryfallCards>(cardsJson);
            var cardsList = new List<MagicCard_Temp>();
            foreach (var scryfallCard in scryfallCardsList.Cards)
            {
                int collectorNumber = 0;
                if (!string.IsNullOrEmpty(scryfallCard.CollectorNumber))
                {
                    int.TryParse(scryfallCard.CollectorNumber, out collectorNumber);
                }

                decimal? priceUsd = 0;
                if (scryfallCard.Usd != null)
                {
                    priceUsd = scryfallCard.Usd;
                }
                else if (scryfallCard.Prices != null && scryfallCard.Prices.Usd != null)
                {
                    priceUsd = scryfallCard.Prices.Usd;
                }

                decimal? priceUsdFoil = 0;
                if (scryfallCard.Prices != null && scryfallCard.Prices.UsdFoil != null)
                {
                    priceUsdFoil = scryfallCard.Prices.UsdFoil;
                }

                string imageUri = null;
                if (scryfallCard.ImageUris != null)
                {
                    imageUri = scryfallCard.ImageUris.Normal;
                }

                string imageUri2 = null;
                string oracleText = scryfallCard.OracleText;
                string artist = scryfallCard.Artist;
                string flavorText = scryfallCard.FlavorText;
                string manaCost = scryfallCard.ManaCost;
                string typeLine = scryfallCard.TypeLine;
                if (scryfallCard.CardFaces != null && scryfallCard.CardFaces.Length > 1)
                {
                    if(scryfallCard.CardFaces[0].ImageUris != null)
                    {
                        imageUri = scryfallCard.CardFaces[0].ImageUris.Normal;
                    }
                    if (scryfallCard.CardFaces[1].ImageUris != null)
                    {
                        imageUri2 = scryfallCard.CardFaces[1].ImageUris.Normal;
                    }
                    oracleText = scryfallCard.CardFaces[0].OracleText + " // " + scryfallCard.CardFaces[1].OracleText;
                    artist = scryfallCard.CardFaces[0].Artist + " // " + scryfallCard.CardFaces[1].Artist;
                    flavorText = scryfallCard.CardFaces[0].FlavorText + " // " + scryfallCard.CardFaces[1].FlavorText;
                    manaCost = scryfallCard.CardFaces[0].ManaCost + " // " + scryfallCard.CardFaces[1].ManaCost;
                    typeLine = scryfallCard.CardFaces[0].TypeLine + " // " + scryfallCard.CardFaces[1].TypeLine;
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
                    ImageUri2 = imageUri2,
                    TypeLine = typeLine,
                    OracleText = oracleText,
                    ManaCost = manaCost,
                    CollectorNumber = collectorNumber,
                    Rarity = scryfallCard.Rarity,
                    FlavorText = flavorText,
                    Artist = artist,
                    PurchaseUri = purchaseUri,
                    PriceUsd = priceUsd,
                    PriceUsdFoil = priceUsdFoil,
                    Language = scryfallCard.Lang
                };
                cardsList.Add(card);
            }
            return cardsList;
            
        }


        public async Task<List<MagicSet_Temp>> GetScryfallSets()
        {
            var response = await _client.GetAsync("https://api.scryfall.com/sets");
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
