using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PokedexApp.Models
{
    public class PokemonType
    {
        public int Id { get; set; }
        public int Slot { get; set; }
        public int PokemonId { get; set; }
        public int TypeId { get; set; }

        [ForeignKey("TypeId")]
        public TypeInfo Type { get; set; }
    }
}
