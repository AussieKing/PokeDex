using System.Collections.Generic;
using System.Threading.Tasks;
using PokedexApp.Models;

namespace PokedexApp.Services
{
    public interface IPokemonService
    {
        Task<Pokemon> GetPokemonByIdAsync(int id);
        Task<Pokemon> GetPokemonByNameAsync(string name);
        Task<IEnumerable<Pokemon>> GetPokemonsInPokedexAsync();
        Task AddPokemonAsync(Pokemon pokemon);
        Task DeletePokemonAsync(int id);
        Task UpdatePokemonAsync(Pokemon pokemon);
        Task<IEnumerable<Pokemon>> GetPokemonsAsync();
        Task AddPokemonByIdAsync(int id);
        Task AddPokemonByNameAsync(string name);
    }
}
