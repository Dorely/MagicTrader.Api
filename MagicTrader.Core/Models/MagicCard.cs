using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MagicTrader.Core.Models
{
    [Table("MagicCards")]
    public class MagicCard
    {
        [Key]
        public Guid ScryfallId { get; set; }
        public string SetCode { get; set; }
        public Guid OracleId { get; set; }
        public int? MultiverseId { get; set; }
        public string CardName { get; set; }
        public string ApiUri { get; set; }
        public string ImageUri { get; set; }
        public string TypeLine { get; set; }
        public string OracleText { get; set; }
        public string ManaCost { get; set; }
        public int? CollectorNumber { get; set; }
        public string Rarity { get; set; }
        public string FlavorText { get; set; }
        public string Artist { get; set; }
        public string PurchaseUri { get; set; }
        public float? PriceUsd { get; set; }
    }

    [Table("MagicCards_TEMP")]
    public class MagicCard_Temp
    {
        [Key]
        public Guid ScryfallId { get; set; }
        public string SetCode { get; set; }
        public Guid OracleId { get; set; }
        public int? MultiverseId { get; set; }
        public string CardName { get; set; }
        public string ApiUri { get; set; }
        public string ImageUri { get; set; }
        public string TypeLine { get; set; }
        public string OracleText { get; set; }
        public string ManaCost { get; set; }
        public int? CollectorNumber { get; set; }
        public string Rarity { get; set; }
        public string FlavorText { get; set; }
        public string Artist { get; set; }
        public string PurchaseUri { get; set; }
        public float? PriceUsd { get; set; }
    }

    public class ScryfallCards
    {
        [JsonProperty("total_cards")]
        public int TotalCards { get; set; }

        [JsonProperty("has_more")]
        public bool HasMore { get; set; }

        [JsonProperty("next_page")]
        public string NextPage { get; set; }

        [JsonProperty("data")]
        public List<ScryfallCard> Cards { get; set; }
    }

    public class ScryfallCard
    {
        [JsonProperty("object")]
        public string Object { get; set; }

        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("oracle_id")]
        public Guid OracleId { get; set; }

        [JsonProperty("multiverse_ids")]
        public object[] MultiverseIds { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("lang")]
        public string Lang { get; set; }

        [JsonProperty("released_at")]
        public DateTimeOffset ReleasedAt { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("scryfall_uri")]
        public string ScryfallUri { get; set; }

        [JsonProperty("layout")]
        public string Layout { get; set; }

        [JsonProperty("highres_image")]
        public bool HighresImage { get; set; }

        [JsonProperty("image_uris")]
        public ImageUris ImageUris { get; set; }

        [JsonProperty("mana_cost")]
        public string ManaCost { get; set; }

        [JsonProperty("cmc")]
        public long Cmc { get; set; }

        [JsonProperty("type_line")]
        public string TypeLine { get; set; }

        [JsonProperty("oracle_text")]
        public string OracleText { get; set; }

        [JsonProperty("colors")]
        public object[] Colors { get; set; }

        [JsonProperty("color_identity")]
        public object[] ColorIdentity { get; set; }

        [JsonProperty("legalities")]
        public Legalities Legalities { get; set; }

        [JsonProperty("games")]
        public string[] Games { get; set; }

        [JsonProperty("reserved")]
        public bool Reserved { get; set; }

        [JsonProperty("foil")]
        public bool Foil { get; set; }

        [JsonProperty("nonfoil")]
        public bool Nonfoil { get; set; }

        [JsonProperty("oversized")]
        public bool Oversized { get; set; }

        [JsonProperty("promo")]
        public bool Promo { get; set; }

        [JsonProperty("reprint")]
        public bool Reprint { get; set; }

        [JsonProperty("set")]
        public string Set { get; set; }

        [JsonProperty("set_name")]
        public string SetName { get; set; }

        [JsonProperty("set_uri")]
        public string SetUri { get; set; }

        [JsonProperty("set_search_uri")]
        public string SetSearchUri { get; set; }

        [JsonProperty("scryfall_set_uri")]
        public string ScryfallSetUri { get; set; }

        [JsonProperty("rulings_uri")]
        public string RulingsUri { get; set; }

        [JsonProperty("prints_search_uri")]
        public string PrintsSearchUri { get; set; }

        [JsonProperty("collector_number")]
        public string CollectorNumber { get; set; }

        [JsonProperty("digital")]
        public bool Digital { get; set; }

        [JsonProperty("rarity")]
        public string Rarity { get; set; }

        [JsonProperty("flavor_text")]
        public string FlavorText { get; set; }

        [JsonProperty("illustration_id")]
        public Guid IllustrationId { get; set; }

        [JsonProperty("artist")]
        public string Artist { get; set; }

        [JsonProperty("border_color")]
        public string BorderColor { get; set; }

        [JsonProperty("frame")]
        public string Frame { get; set; }

        [JsonProperty("full_art")]
        public bool FullArt { get; set; }

        [JsonProperty("story_spotlight")]
        public bool StorySpotlight { get; set; }

        [JsonProperty("usd")]
        public string Usd { get; set; }

        [JsonProperty("eur")]
        public string Eur { get; set; }

        [JsonProperty("prices")]
        public Prices Prices { get; set; }

        [JsonProperty("related_uris")]
        public RelatedUris RelatedUris { get; set; }

        [JsonProperty("purchase_uris")]
        public PurchaseUris PurchaseUris { get; set; }
    }

    public partial class ImageUris
    {
        [JsonProperty("small")]
        public string Small { get; set; }

        [JsonProperty("normal")]
        public string Normal { get; set; }

        [JsonProperty("large")]
        public string Large { get; set; }

        [JsonProperty("png")]
        public string Png { get; set; }

        [JsonProperty("art_crop")]
        public string ArtCrop { get; set; }

        [JsonProperty("border_crop")]
        public string BorderCrop { get; set; }
    }

    public partial class Legalities
    {
        [JsonProperty("standard")]
        public string Standard { get; set; }

        [JsonProperty("future")]
        public string Future { get; set; }

        [JsonProperty("frontier")]
        public string Frontier { get; set; }

        [JsonProperty("modern")]
        public string Modern { get; set; }

        [JsonProperty("legacy")]
        public string Legacy { get; set; }

        [JsonProperty("pauper")]
        public string Pauper { get; set; }

        [JsonProperty("vintage")]
        public string Vintage { get; set; }

        [JsonProperty("penny")]
        public string Penny { get; set; }

        [JsonProperty("commander")]
        public string Commander { get; set; }

        [JsonProperty("duel")]
        public string Duel { get; set; }

        [JsonProperty("oldschool")]
        public string Oldschool { get; set; }
    }

    public partial class Prices
    {
        [JsonProperty("usd")]
        public string Usd { get; set; }

        [JsonProperty("usd_foil")]
        public string UsdFoil { get; set; }

        [JsonProperty("eur")]
        public string Eur { get; set; }

        [JsonProperty("tix")]
        public object Tix { get; set; }
    }

    public partial class PurchaseUris
    {
        [JsonProperty("tcgplayer")]
        public string Tcgplayer { get; set; }

        [JsonProperty("cardmarket")]
        public string Cardmarket { get; set; }

        [JsonProperty("cardhoarder")]
        public string Cardhoarder { get; set; }
    }

    public partial class RelatedUris
    {
        [JsonProperty("tcgplayer_decks")]
        public string TcgplayerDecks { get; set; }

        [JsonProperty("edhrec")]
        public string Edhrec { get; set; }

        [JsonProperty("mtgtop8")]
        public string Mtgtop8 { get; set; }
    }
}
