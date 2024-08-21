'use client';

import React, { useState } from 'react';
import styles from './Collection.module.css';

const CollectionPage: React.FC = () => {
    const [games, setGames] = useState<any[]>([]);
    const [newGame, setNewGame] = useState({
        title: '',
        status: 'Owned',
        genre: 'Action',
        releaseDate: '',
        developer: '',
        publisher: '',
        platform: 'Console',
        manufacturer: ''
    });

    const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
        setNewGame({ ...newGame, [e.target.name]: e.target.value });
    };

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();
        setGames([...games, newGame]);
        setNewGame({
            title: '',
            status: 'Owned',
            genre: 'Action',
            releaseDate: '',
            developer: '',
            publisher: '',
            platform: 'Console',
            manufacturer: ''
        });
    };

    const handleDelete = (index: number) => {
        const updatedGames = games.filter((_, i) => i !== index);
        setGames(updatedGames);
    };

    return (
        <main className={styles.container}>
            <div className={styles.formContainer}>
                <h2>Add New Game</h2>
                <form onSubmit={handleSubmit} className={styles.form}>
                    <input 
                        type="text" 
                        name="title" 
                        placeholder="Game Title" 
                        value={newGame.title} 
                        onChange={handleChange} 
                        required 
                    />
                    <select name="status" value={newGame.status} onChange={handleChange}>
                        <option value="Owned">Owned</option>
                        <option value="Wishlist">Wishlist</option>
                        <option value="Playing">Playing</option>
                        <option value="Completed">Completed</option>
                    </select>
                    <select name="genre" value={newGame.genre} onChange={handleChange}>
                        <option value="Action">Action</option>
                        <option value="Adventure">Adventure</option>
                        <option value="RPG">RPG</option>
                        <option value="Simulation">Simulation</option>
                        <option value="Strategy">Strategy</option>
                        <option value="Sports">Sports</option>
                    </select>
                    <input 
                        type="date" 
                        name="releaseDate" 
                        value={newGame.releaseDate} 
                        onChange={handleChange} 
                        required 
                    />
                    <input 
                        type="text" 
                        name="developer" 
                        placeholder="Developer" 
                        value={newGame.developer} 
                        onChange={handleChange} 
                        required 
                    />
                    <input 
                        type="text" 
                        name="publisher" 
                        placeholder="Publisher" 
                        value={newGame.publisher} 
                        onChange={handleChange} 
                        required 
                    />
                    <select name="platform" value={newGame.platform} onChange={handleChange}>
                        <option value="Console">Console</option>
                        <option value="PC">PC</option>
                        <option value="Mobile">Mobile</option>
                    </select>
                    <input 
                        type="text" 
                        name="manufacturer" 
                        placeholder="Manufacturer" 
                        value={newGame.manufacturer} 
                        onChange={handleChange} 
                        required 
                    />
                    <button type="submit">Add Game</button>
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
                            <p>Platform: {game.platform}</p>
                            <p>Manufacturer: {game.manufacturer}</p>
                            <button onClick={() => handleDelete(index)}>Delete</button>
                        </div>
                    ))}
                </div>
            </div>
        </main>
    );
};

export default CollectionPage;
