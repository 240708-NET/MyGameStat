'use client';

import React, { useState, useEffect } from 'react';
import styles from './Collection.module.css';

interface Game {
    id?: number; 
    title: string;
    status: string;
    genre: string;
    releaseDate: string;
    developer: string;
    publisher: string;
    platformName: string;
    platformManufacturer: string;
}

const CollectionPage: React.FC = () => {
    const [games, setGames] = useState<Game[]>([]);
    const [newGame, setNewGame] = useState<Game>({
        title: '',
        status: 'Owned',
        genre: 'Action',
        releaseDate: '',
        developer: '',
        publisher: '',
        platformName: 'Console',
        platformManufacturer: ''
    });
    const [editIndex, setEditIndex] = useState<number | null>(null);

    // Fetch all games
    const fetchGames = async () => {
        const token = sessionStorage.getItem('token');
        const tokenType = sessionStorage.getItem('tokenType') || 'Bearer';

        if (!token) {
            sessionStorage.clear();
            console.error('No token found');
            return;
        }

        try {
            const response = await fetch(`https://localhost:7094/api/user/games`, {
                method: 'GET',
                headers: {
                    'Authorization': `${tokenType} ${token}`,
                    'Content-Type': 'application/json'
                }
            });

            if (response.ok) {
                const data = await response.json();
                setGames(data);
            } else {
                sessionStorage.clear();
                console.error('Failed to fetch games');
            }
        } catch (error) {
            console.error('An error occurred while fetching games:', error);
        }
    };

    useEffect(() => {
        fetchGames();
    }, []);

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();

        const token = sessionStorage.getItem('token');
        const tokenType = sessionStorage.getItem('tokenType') || 'Bearer';

        if (!token) {
            console.error('No token found');
            return;
        }

        const gameData = { 
            title: newGame.title,
            status: newGame.status,
            genre: newGame.genre,
            releaseDate: newGame.releaseDate,
            developer: newGame.developer,
            publisher: newGame.publisher,
            platformName: newGame.platformName,
            platformManufacturer: newGame.platformManufacturer,
        };

        try {
            let response;

            if (editIndex !== null && games[editIndex]?.id) {
                const gameId = games[editIndex].id;
                console.log('Attempting to update game with ID:', gameId);
                response = await fetch(`https://localhost:7094/api/user/games/${gameId}`, {
                    method: 'PUT',
                    headers: {
                        'Authorization': `${tokenType} ${token}`,
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(gameData)
                });
            } else {
                console.log('Attempting to add new game with data:', gameData);
                response = await fetch(`https://localhost:7094/api/user/games`, {
                    method: 'POST',
                    headers: {
                        'Authorization': `${tokenType} ${token}`,
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(gameData)
                });
            }

            console.log('Response status:', response.status);

            if (response.ok) {
                // Fetch the updated collection after successful add/update
                await fetchGames();
                setNewGame({
                    title: '',
                    status: 'Owned',
                    genre: 'Action',
                    releaseDate: '',
                    developer: '',
                    publisher: '',
                    platformName: '',
                    platformManufacturer: ''
                });
                setEditIndex(null);
            } else {
                const errorText = await response.text();
                console.error('Failed to add/update game. Server responded with:', errorText);
            }
        } catch (error) {
            console.error('An error occurred while adding/updating the game:', error);
        }
    };

    // Handle deleting a game
    const handleDelete = async (index: number) => {
        const gameId = games[index].id;

        if (!gameId) {
            console.error('No game ID found');
            return;
        }

        const token = sessionStorage.getItem('token');
        const tokenType = sessionStorage.getItem('tokenType') || 'Bearer';

        if (!token) {
            console.error('No token found');
            return;
        }
        try {
            const response = await fetch(`https://localhost:7094/api/user/games/${gameId}`, {
                method: 'DELETE',
                headers: {
                    'Authorization': `${tokenType} ${token}`,
                    'Content-Type': 'application/json'
                }
            });

            if (response.ok) {
                const updatedGames = games.filter((_, i) => i !== index);
                setGames(updatedGames);
            } else {
                const errorText = await response.text();
                console.error('Failed to delete game. Server responded with:', errorText);
            }
        } catch (error) {
            console.error('An error occurred while deleting the game:', error);
        }
    };

    // Handle editing a game
    const handleEdit = (index: number) => {
        setEditIndex(index);
        setNewGame(games[index]);
    };

    // Handle input change
    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
        const { name, value } = e.target;
        setNewGame(prevState => ({ ...prevState, [name]: value }));
    };

    return (
        <main className={styles.container}>
            <div className={styles.formContainer}>
                <h2>{editIndex !== null ? 'Edit Game' : 'Add New Game'}</h2>
                <form onSubmit={handleSubmit} className={styles.form}>
                    <label htmlFor="title">Game Title:</label>
                    <input
                        type="text"
                        name="title"
                        placeholder="Game Title"
                        value={newGame.title}
                        onChange={handleInputChange}
                        required
                    />
                    <label htmlFor="status">Status:</label>
                    <select name="status" value={newGame.status} onChange={handleInputChange}>
                        <option value="Owned">Owned</option>
                        <option value="Wishlist">Wishlist</option>
                        <option value="Playing">Playing</option>
                        <option value="Completed">Completed</option>
                    </select>
                    <label htmlFor="genre">Genre:</label>
                    <select name="genre" value={newGame.genre} onChange={handleInputChange}>
                        <option value="Action">Action</option>
                        <option value="Adventure">Adventure</option>
                        <option value="RPG">RPG</option>
                        <option value="Simulation">Simulation</option>
                        <option value="Strategy">Strategy</option>
                        <option value="Sports">Sports</option>
                    </select>
                    <label htmlFor="releaseDate">Release Date:</label>
                    <input
                        type="date"
                        name="releaseDate"
                        value={newGame.releaseDate}
                        onChange={handleInputChange}
                        required
                    />
                    <label htmlFor="developer">Developer:</label>
                    <input
                        type="text"
                        name="developer"
                        placeholder="Developer"
                        value={newGame.developer}
                        onChange={handleInputChange}
                        required
                    />
                    <label htmlFor="publisher">Publisher:</label>
                    <input
                        type="text"
                        name="publisher"
                        placeholder="Publisher"
                        value={newGame.publisher}
                        onChange={handleInputChange}
                        required
                    />
                    <label htmlFor="platform">Platform:</label>
                    <select name="platformName" value={newGame.platformName} onChange={handleInputChange}>
                        <option value="Console">Console</option>
                        <option value="PC">PC</option>
                        <option value="Mobile">Mobile</option>
                    </select>
                    <label htmlFor="platformManufacturer">Manufacturer:</label>
                    <input
                        type="text"
                        name="platformManufacturer"
                        placeholder="Manufacturer"
                        value={newGame.platformManufacturer}
                        onChange={handleInputChange}
                        required
                    />
                    <button type="submit" className={editIndex !== null ? styles.updateButton : styles.addButton}>
                        {editIndex !== null ? 'Update Game' : 'Add Game'}
                    </button>
                </form>
            </div>
            <div className={styles.collectionContainer}>
                <h2>Your Collection</h2>
                <div className={styles.cards}>
                    {games.map((game, index) => (
                        <div key={index} className={styles.card}>
                            <h3>{game.title}</h3>
                            <p>Status: {game.status}</p>
                            <p>Genre: {game.genre}</p>
                            <p>Release Date: {game.releaseDate}</p>
                            <p>Developer: {game.developer}</p>
                            <p>Publisher: {game.publisher}</p>
                            <p>Platform: {game.platformName}</p>
                            <p>Manufacturer: {game.platformManufacturer}</p>
                            <div className={styles.buttonGroup}>
                                <button onClick={() => handleEdit(index)} className={styles.editButton}>
                                    Edit
                                </button>
                                <button onClick={() => handleDelete(index)} className={styles.deleteButton}>
                                    Delete
                                </button>
                            </div>
                        </div>
                    ))}
                </div>
            </div>
        </main>
    );
};

export default CollectionPage;
