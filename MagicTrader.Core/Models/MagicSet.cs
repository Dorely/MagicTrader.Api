using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MagicTrader.Core.Models
{
    [Table("MagicSets")]
    public class MagicSet
    {
        [Key]
        public string SetCode { get; set; }
        public string SetName { get; set; }
        public string ApiUri { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string IconSvgUri { get; set; }
    }

    [Table("MagicSets_TEMP")]
    public class MagicSet_Temp
    {
        [Key]
        public string SetCode { get; set; }
        public string SetName { get; set; }
        public string ApiUri { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string IconSvgUri { get; set; }
    }

    public class ScryfallSet
    {
        public string code { get; set; }
        public string name { get; set; }
        public string uri { get; set; }
        public string scryfall_uri { get; set; }
        public string search_uri { get; set; }
        public string released_at { get; set; }
        public string set_type { get; set; }
        public int card_count { get; set; }
        public bool digital { get; set; }
        public bool foil_only { get; set; }
        public string icon_svg_uri { get; set; }
    }

    public class ScryfallSets
    {
        public bool has_more { get; set; }
	    public List<ScryfallSet> data { get; set; }
    }
}
