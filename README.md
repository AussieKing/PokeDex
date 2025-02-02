# Pokedex App

The Pokedex App is a web application that allows users to search for Pokémon, add them to a personal Pokedex, and even simulate battles between selected Pokémon. The app uses a .NET backend and a React frontend to provide a seamless user experience. The backend now uses SQLite for data storage, making it easier to run on macOS and other platforms without additional database setup.

---

## Table of Contents

- [Features](#features)
- [Installation and Usage](#installation-and-usage)
  - [Backend](#backend)
  - [Frontend](#frontend)
- [Technologies Used](#technologies-used)
- [Screenshots](#screenshots)
- [Live URL and Repository](#live-url-and-repository)
- [Credits](#credits)
- [Contributing](#contributing)
- [License](#license)

## Features

- **Search Pokémon:** Search for Pokémon by ID or name using the PokeAPI.
- **Add to Pokedex:** Add your favorite Pokémon to your personal Pokedex.
- **Delete from Pokedex:** Remove Pokémon from your Pokedex.
- **Battle Arena:** Select two Pokémon and simulate a battle with animations and random attack/defense mechanisms.

---

## Installation and Usage

### Backend

1. **Clone the repository:**
```bash
git clone https://github.com/AussieKing/PokedexApp
cd PokedexApp
```
2. **Navigate to the backend directory:**
```
cd PokedexApp.Api
```
3. **Restore dependencies and build the project:**
```
dotnet restore
dotnet build
```
4. **Apply Database Migrations (SQLite):**
```
dotnet ef database update
```
45 **Run the application:**
```
dotnet run
```
The backend should now be running on http://localhost:5037.

### Frontend
1. **Navigate to the frontend directory:**
```
cd pokedex-frontend
```
2. **Install dependencies:**
```
npm install
```
3. **Run the application:**
```
npm start
```
The frontend should now be running on http://localhost:3000.

## Technologies Used
### Backend:
***.NET Core***

***Entity Framework Core***
						
***Microsoft SQL Server***
						
***ASP.NET Core MVC***

***PokeAPI (for fetching Pokemon data)***

### Frontend:
***React***

***Axios***

***CSS for styling***

## Screenshots
### Fetch 152 Pokemons:
<img width="310" alt="fetch_152_pokemon" src="https://github.com/user-attachments/assets/f133af7b-bb70-477f-ac52-c67fe720a6b7">

### Fetch Pokedex:
<img width="494" alt="fetch_pokedex" src="https://github.com/user-attachments/assets/370c8b39-897d-46cc-9070-b8a75226ba79">

![SQL-Pokedex](https://github.com/AussieKing/PokedexApp/assets/126050763/abd2fbd1-aeb6-4ad1-9d3a-03acf626bfa1)

### Add Pokeomon to Pokedex:
<img width="238" alt="pokemon_to_pokedex" src="https://github.com/user-attachments/assets/4c8eea5c-4296-4852-b3fd-c1df1f60fd73">

### Delete Pokemon from Pokedex:
<img width="473" alt="pokemon_deleted_pokedex" src="https://github.com/user-attachments/assets/18ddb2a7-ca01-4086-ad3f-170645d89565">

### Battle Pokemons:
<img width="466" alt="battle" src="https://github.com/user-attachments/assets/eb89696b-01cf-4297-92fd-2d6c9b3c1ad0">

### Battle Result:
<img width="466" alt="battle_results" src="https://github.com/user-attachments/assets/99049794-c941-4f21-adaf-494ba479e1e5">


## Live URL and Repository
The repository can be accessed at the following URL: [Pokedex App](https://github.com/AussieKing/PokedexApp)

## Credits
This project was created by [AussieKing](https://github.com/AussieKing), thanks to the [PokeAPI](https://pokeapi.co/) for providing the Pokemon data.

## Contributing
Please fork the repository and create a pull request with your changes. For major changes, please open an issue first to discuss what you would like to change.

## License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
