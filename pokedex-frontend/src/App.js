import React, { useState, useEffect } from 'react';
import axios from 'axios';
import PokemonCard from './components/PokemonCard';
import BattleArena from './components/BattleArena';
import './App.css';

// 🔹 Set API Base URL dynamically (Supports Docker & Localhost)
const API_BASE_URL = process.env.REACT_APP_API_URL || "http://localhost:5037/api"; 

const App = () => {
    const [pokemons, setPokemons] = useState([]);
    const [searchId, setSearchId] = useState('');
    const [searchName, setSearchName] = useState('');
    const [errorMessage, setErrorMessage] = useState('');

    useEffect(() => {
        fetchPokemons();
    }, []);

    const fetchPokemons = async () => {
        try {
            const response = await axios.get(`${API_BASE_URL}/pokemon`);
            setPokemons(response.data);
            setErrorMessage('');
        } catch (error) {
            console.error('Error fetching pokemons:', error);
            setErrorMessage('Failed to fetch pokemons. Please try again later.');
        }
    };

    const fetchPokemonById = async () => {
        try {
            const response = await axios.get(`${API_BASE_URL}/pokemon/${searchId}`);
            setPokemons([response.data]);
            setErrorMessage('');
        } catch (error) {
            console.error('Error fetching pokemon by ID:', error);
            setErrorMessage('Pokemon with the specified ID not found.');
        }
    };

    const fetchPokemonByName = async () => {
        try {
            const response = await axios.get(`${API_BASE_URL}/pokemon/name/${searchName}`);
            setPokemons([response.data]);
            setErrorMessage('');
        } catch (error) {
            console.error('Error fetching pokemon by name:', error);
            if (error.response && error.response.status === 404) {
                setErrorMessage(error.response.data);
            } else {
                setErrorMessage('Failed to fetch pokemon by name. Please check details again.');
            }
        }
    };

    const fetchFromPokedex = async () => {
        try {
            const response = await axios.get(`${API_BASE_URL}/pokemoncommand/pokedex`);
            setPokemons(response.data);
        } catch (error) {
            console.error('Error fetching pokemons from Pokedex:', error);
            alert('Failed to fetch Pokemons from Pokedex. Please try again.');
        }
    };

    const addToPokedex = async (id) => {
        try {
            await axios.post(`${API_BASE_URL}/pokemoncommand/addById/${id}`);
            alert('Pokemon added to Pokedex successfully!');
        } catch (error) {
            console.error('Error adding to Pokedex:', error);
            alert('Failed to add Pokemon to Pokedex. Please check details again.');
        }
    };

    const deleteFromPokedex = async (id) => {
        try {
            await axios.delete(`${API_BASE_URL}/pokemoncommand/${id}`);
            alert('Pokemon deleted successfully!');
            setPokemons(pokemons.filter(pokemon => pokemon.id !== id));
        } catch (error) {
            console.error('Error deleting from Pokedex:', error);
            alert('Failed to delete Pokemon. Please try again.');
        }
    };

    return (
        <div className="container">
            <div className="background-box">
                <h1>Pokedex</h1>
            </div>
            <div className="controls">
                <button onClick={fetchPokemons}>See All Pokemon</button>
                <button onClick={fetchFromPokedex}>My Pokedex</button>
                <input
                    type="text"
                    placeholder="Search by ID"
                    value={searchId}
                    onChange={(e) => setSearchId(e.target.value)}
                />
                <button onClick={fetchPokemonById}>Search by ID</button>
                <input
                    type="text"
                    placeholder="Search by Name"
                    value={searchName}
                    onChange={(e) => setSearchName(e.target.value)}
                />
                <button onClick={fetchPokemonByName}>Search by Name</button>
            </div>
            {errorMessage && <div className="error-message">{errorMessage}</div>}
            <div className="pokemon-list">
                {pokemons.map(pokemon => (
                    <PokemonCard
                        key={pokemon.id}
                        pokemon={pokemon}
                        addToPokedex={addToPokedex}
                        deleteFromPokedex={deleteFromPokedex}
                    />
                ))}
            </div>
            <BattleArena pokemons={pokemons} />
        </div>
    );
};

export default App;