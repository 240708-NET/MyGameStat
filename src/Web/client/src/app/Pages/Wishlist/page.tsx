'use client';

import React, { useState, useEffect } from 'react';
import axios from 'axios';
import WishlistSlider from '../../Components/WishlistSlider/WishlistSlider';
import styles from './Wishlist.module.css'; 

const WishlistPage: React.FC = () => {
    const [wishlistItems, setWishlistItems] = useState<any[]>([]);
    const [loading, setLoading] = useState(true); 

    useEffect(() => {
        const fetchWishlist = async () => {
            const API_KEY = ''; 
            try {
                const response = await axios.get('https://api.rawg.io/api/games', {
                    params: {
                        key: API_KEY,
                    },
                });
                setWishlistItems(response.data.results);
                setLoading(false); 
            } catch (error) {
                console.error('Error fetching wishlist:', error);
                setLoading(false); 
            }
        };

        fetchWishlist(); 
    }, []);

    return (   
            <main>
                <div className={styles.Title}>
                    <h1>Wishlist</h1>
                </div>
                <div className={styles.Container}>
                    {loading ? (
                        <p>Loading...</p>
                    ) : (
                        <WishlistSlider wishlistItems={wishlistItems} /> 
                    )}
                </div>
            </main>
    );
};

export default WishlistPage;


