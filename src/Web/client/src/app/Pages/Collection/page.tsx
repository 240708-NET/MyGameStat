'use client';

import React, { useState , useEffect} from 'react';
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
    const [editIndex, setEditIndex] = useState<number | null>(null);

    useEffect(() => {
        const loggedInUser = localStorage.getItem('loggedInUser');
        const savedGames = JSON.parse(localStorage.getItem('userGames') || '[]');

        // Filter games to show only those belonging to the logged-in user
        const userGames = savedGames.filter((game: any) => game.user === loggedInUser);
        setGames(userGames);
    }, []);

    const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
        setNewGame({ ...newGame, [e.target.name]: e.target.value });
    };

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();

        const loggedInUser = localStorage.getItem('loggedInUser');

        if (editIndex !== null) {
            const updatedGames = [...games];
            updatedGames[editIndex] = newGame;
            setGames(updatedGames);
            setEditIndex(null);
        } else {
            setGames([...games, newGame]);
        }
        localStorage.setItem('userGames', JSON.stringify([...games, { ...newGame, user: loggedInUser }]));
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
        localStorage.setItem('userGames', JSON.stringify(updatedGames));
    };

    const handleEdit = (index: number) => {
        setEditIndex(index);
        setNewGame(games[index]);
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
                        onChange={handleChange}
                        required
                    />
                    <label htmlFor="status">Status:</label>
                    <select name="status" value={newGame.status} onChange={handleChange}>
                        {/* <option value="Owned">Owned</option> */}
                        {/* <option value="Wishlist">Wishlist</option> */}
                        <option value="Playing">Playing</option>
                        <option value="Completed">Completed</option>
                    </select>
                    <label htmlFor="genre">Genre:</label>
                    <select name="genre" value={newGame.genre} onChange={handleChange}>
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
                        onChange={handleChange}
                        required
                    />
                    <label htmlFor="developer">Developer:</label>
                    <input
                        type="text"
                        name="developer"
                        placeholder="Developer"
                        value={newGame.developer}
                        onChange={handleChange}
                        required
                    />
                    <label htmlFor="publisher">Publisher:</label>
                    <input
                        type="text"
                        name="publisher"
                        placeholder="Publisher"
                        value={newGame.publisher}
                        onChange={handleChange}
                        required
                    />
                    <label htmlFor="platform">Platform:</label>
                    <select name="platform" value={newGame.platform} onChange={handleChange}>
                        <option value="Console">Console</option>
                        <option value="PC">PC</option>
                        <option value="Mobile">Mobile</option>
                    </select>
                    <label htmlFor="manufacturer">Manufacturer:</label>
                    <input
                        type="text"
                        name="manufacturer"
                        placeholder="Manufacturer"
                        value={newGame.manufacturer}
                        onChange={handleChange}
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
                            <p>Platform: {game.platform}</p>
                            <p>Manufacturer: {game.manufacturer}</p>
                            <div className={styles.cardButtons}>
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
