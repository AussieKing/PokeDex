using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PokedexApp.Models;
using PokedexApp.Services;

namespace PokedexApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PokemonCommandController : ControllerBase
    {
        private readonly IPokemonService _pokemonService;

        public PokemonCommandController(IPokemonService pokemonService)
        {
            _pokemonService = pokemonService;
        }

        [HttpGet("pokedex")]
        public async Task<IActionResult> GetPokemonsInPokedex()
        {
            var pokemons = await _pokemonService.GetPokemonsInPokedexAsync();
            return Ok(pokemons);
        }

        [HttpPost("addById/{id}")]
        public async Task<IActionResult> AddById(int id)
        {
            await _pokemonService.AddPokemonByIdAsync(id);
            return CreatedAtAction(nameof(GetById), "Pokemon", new { id = id }, null);
        }

        [HttpPost("addByName/{name}")]
        public async Task<IActionResult> AddByName(string name)
        {
            await _pokemonService.AddPokemonByNameAsync(name);
            return CreatedAtAction(nameof(GetByName), "Pokemon", new { name = name }, null);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Pokemon pokemon)
        {
            await _pokemonService.UpdatePokemonAsync(pokemon);
            return Ok("The Pokemon has been updated in your Pokedex");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _pokemonService.DeletePokemonAsync(id);
            return Ok("The Pokemon has been deleted from your Pokedex");
        }

        private Task<Pokemon> GetById(int id) => _pokemonService.GetPokemonByIdAsync(id);

        private Task<Pokemon> GetByName(string name) => _pokemonService.GetPokemonByNameAsync(name);
    }
}
