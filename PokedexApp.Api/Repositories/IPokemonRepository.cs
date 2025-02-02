using System.Collections.Generic;
using System.Threading.Tasks;
using PokedexApp.Models;

namespace PokedexApp.Repositories
{
    public interface IPokemonRepository
    {
        Task<Pokemon> GetPokemonByIdAsync(int id);
        Task<Pokemon> GetPokemonByNameAsync(string name);
        Task<IEnumerable<Pokemon>> GetPokemonsAsync();
        Task<IEnumerable<Pokemon>> GetPokemonsInPokedexAsync();
        Task AddPokemonAsync(Pokemon pokemon);
        Task UpdatePokemonAsync(Pokemon pokemon);
        Task DeletePokemonAsync(int id);
    }
}
