using System.ComponentModel.DataAnnotations.Schema;

namespace PokedexApp.Models
{
    public class PokemonStat
    {
        public int Id { get; set; }
        public int BaseStat { get; set; }
        public int Effort { get; set; }
        public int StatId { get; set; }

        [ForeignKey("StatId")]
        public StatInfo Stat { get; set; }
    }
}
