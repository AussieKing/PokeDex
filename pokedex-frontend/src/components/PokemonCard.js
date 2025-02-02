import React from 'react';

const PokemonCard = ({ pokemon, addToPokedex, deleteFromPokedex }) => {
    const stats = {
        hp: 'N/A',
        attack: 'N/A',
        defense: 'N/A',
        specialAttack: 'N/A',
        specialDefense: 'N/A',
        speed: 'N/A'
    };

    if (pokemon.stats) {
        pokemon.stats.forEach(stat => {
            switch (stat.stat.name) {
                case 'hp':
                    stats.hp = stat.baseStat;
                    break;
                case 'attack':
                    stats.attack = stat.baseStat;
                    break;
                case 'defense':
                    stats.defense = stat.baseStat;
                    break;
                case 'special-attack':
                    stats.specialAttack = stat.baseStat;
                    break;
                case 'special-defense':
                    stats.specialDefense = stat.baseStat;
                    break;
                case 'speed':
                    stats.speed = stat.baseStat;
                    break;
                default:
                    break;
            }
        });
    }

    const totalScore =
        (stats.hp !== 'N/A' ? stats.hp : 0) +
        (stats.attack !== 'N/A' ? stats.attack : 0) +
        (stats.defense !== 'N/A' ? stats.defense : 0) +
        (stats.specialAttack !== 'N/A' ? stats.specialAttack : 0) +
        (stats.specialDefense !== 'N/A' ? stats.specialDefense : 0) +
        (stats.speed !== 'N/A' ? stats.speed : 0);

    return (
        <div className="pokemon-card" id={`pokemon-${pokemon.id}`}>
            <h2>{pokemon.name}</h2>
            <img src={pokemon.imageUrl} alt={pokemon.name} onError={(e) => { e.target.onerror = null; e.target.src = "default-image-url.png" }} />
            <p>Type: {pokemon.types && pokemon.types.map(t => t.type.name).join(', ')}</p>
            <p>HP: {stats.hp}</p>
            <p>Attack: {stats.attack}</p>
            <p>Defense: {stats.defense}</p>
            <p>Special Attack: {stats.specialAttack}</p>
            <p>Special Defense: {stats.specialDefense}</p>
            <p>Speed: {stats.speed}</p>
            <p>Total Score: {totalScore}</p>
            <p>Height: {pokemon.height}</p>
            <p>Weight: {pokemon.weight}</p>
            <div className="pokemon-actions">
                <button onClick={() => addToPokedex(pokemon.id)}>Add to Pokedex</button>
                <button onClick={() => deleteFromPokedex(pokemon.id)}>Delete from Pokedex</button>
            </div>
        </div>
    );
};

export default PokemonCard;
