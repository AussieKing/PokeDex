using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Caching.Memory;
using PokedexApp.Models;
using PokedexApp.Repositories;

namespace PokedexApp.Services
{
    public class PokemonService : IPokemonService
    {
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IMemoryCache _cache;
        private static readonly string CacheKeyPrefix = "Pokemon_";

        public PokemonService(IPokemonRepository pokemonRepository, IMemoryCache cache)
        {
            _pokemonRepository = pokemonRepository;
            _cache = cache;
        }

        public async Task<Pokemon> GetPokemonByIdAsync(int id)
        {
            var cacheKey = $"{CacheKeyPrefix}{id}";
            if (!_cache.TryGetValue(cacheKey, out Pokemon pokemon))
            {
                pokemon = await _pokemonRepository.GetPokemonByIdAsync(id);
                if (pokemon != null)
                {
                    SetCache(cacheKey, pokemon);
                }
            }
            return pokemon;
        }

        public async Task<Pokemon> GetPokemonByNameAsync(string name)
        {
            var cacheKey = $"{CacheKeyPrefix}{name}";
            if (!_cache.TryGetValue(cacheKey, out Pokemon pokemon))
            {
                pokemon = await _pokemonRepository.GetPokemonByNameAsync(name);
                if (pokemon != null)
                {
                    SetCache(cacheKey, pokemon);
                }
            }
            return pokemon;
        }

        public async Task<IEnumerable<Pokemon>> GetPokemonsInPokedexAsync()
        {
            var cacheKey = $"{CacheKeyPrefix}Pokedex";
            if (!_cache.TryGetValue(cacheKey, out IEnumerable<Pokemon> pokemons))
            {
                pokemons = await _pokemonRepository.GetPokemonsInPokedexAsync();
                if (pokemons != null)
                {
                    SetCache(cacheKey, pokemons);
                }
            }
            return pokemons;
        }

        public async Task<IEnumerable<Pokemon>> GetPokemonsAsync()
        {
            var cacheKey = $"{CacheKeyPrefix}All";
            if (!_cache.TryGetValue(cacheKey, out IEnumerable<Pokemon> pokemons))
            {
                pokemons = await _pokemonRepository.GetPokemonsAsync();
                if (pokemons != null)
                {
                    SetCache(cacheKey, pokemons);
                }
            }
            return pokemons;
        }

        public async Task AddPokemonAsync(Pokemon pokemon)
        {
            await _pokemonRepository.AddPokemonAsync(pokemon);
            var cacheKey = $"{CacheKeyPrefix}{pokemon.Id}";
            SetCache(cacheKey, pokemon);
            RemoveCache($"{CacheKeyPrefix}All");
            RemoveCache($"{CacheKeyPrefix}Pokedex");
        }

        public async Task AddPokemonByIdAsync(int id)
        {
            var cacheKey = $"{CacheKeyPrefix}{id}";
            if (!_cache.TryGetValue(cacheKey, out Pokemon pokemon))
            {
                pokemon = await _pokemonRepository.GetPokemonByIdAsync(id);
                if (pokemon != null)
                {
                    await _pokemonRepository.AddPokemonAsync(pokemon);
                    SetCache(cacheKey, pokemon);
                    RemoveCache($"{CacheKeyPrefix}All");
                    RemoveCache($"{CacheKeyPrefix}Pokedex");
                }
            }
        }

        public async Task AddPokemonByNameAsync(string name)
        {
            var cacheKey = $"{CacheKeyPrefix}{name}";
            if (!_cache.TryGetValue(cacheKey, out Pokemon pokemon))
            {
                pokemon = await _pokemonRepository.GetPokemonByNameAsync(name);
                if (pokemon != null)
                {
                    await _pokemonRepository.AddPokemonAsync(pokemon);
                    SetCache(cacheKey, pokemon);
                    RemoveCache($"{CacheKeyPrefix}All");
                    RemoveCache($"{CacheKeyPrefix}Pokedex");
                }
            }
        }

        public async Task DeletePokemonAsync(int id)
        {
            await _pokemonRepository.DeletePokemonAsync(id);
            var cacheKey = $"{CacheKeyPrefix}{id}";
            _cache.Remove(cacheKey);
            RemoveCache($"{CacheKeyPrefix}All");
            RemoveCache($"{CacheKeyPrefix}Pokedex");
        }

        public async Task UpdatePokemonAsync(Pokemon pokemon)
        {
            await _pokemonRepository.UpdatePokemonAsync(pokemon);
            var cacheKey = $"{CacheKeyPrefix}{pokemon.Id}";
            SetCache(cacheKey, pokemon);
            RemoveCache($"{CacheKeyPrefix}All");
            RemoveCache($"{CacheKeyPrefix}Pokedex");
        }

        private void SetCache<T>(string cacheKey, T value)
        {
            var cacheOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(
                TimeSpan.FromMinutes(30)
            );
            _cache.Set(cacheKey, value, cacheOptions);
        }

        private void RemoveCache(string cacheKey)
        {
            _cache.Remove(cacheKey);
        }
    }
}
