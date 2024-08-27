'use client';

import React, { useEffect, useState } from 'react';
import styles from './Analytics.module.css';
import CustomRadarChart from '../../Components/Charts/RadarChart';
import CustomPieChart from '../../Components/Charts/PieChart';
import CustomHistogramChart from '../../Components/Charts/HistogramChart'; 

const AnalyticsPage: React.FC = () => {
    const [ownedGames, setOwnedGames] = useState<number>(0);
    const [inProgressGames, setInProgressGames] = useState<number>(0);
    const [completedGames, setCompletedGames] = useState<number>(0);
    const [genreDistribution, setGenreDistribution] = useState<{ [key: string]: number }>({});
    const [platformDistribution, setPlatformDistribution] = useState<{ [key: string]: number }>({});
    const [wishlistGames, setWishlistGames] = useState<any[]>([]);
    const [releaseDates, setReleaseDates] = useState<{ releaseDate: string }[]>([]);
    
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
    
    useEffect(() => {
        const fetchUserGames = async () => {
            const token = sessionStorage.getItem('token');
            const tokenType = sessionStorage.getItem('tokenType') || 'Bearer';
    
            if (!token) {
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
                    const userGames: Game[] = await response.json();
                    
                    // Calculate game statistics
                    const ownedCount = userGames.length;
                    const inProgressCount = userGames.filter((game) => game.status === 'Playing').length;
                    const completedCount = userGames.filter((game) => game.status === 'Completed').length;
                    const wishlist = userGames.filter((game) => game.status === 'Wishlist');

                      // Extract release dates
                const releaseDates = userGames.map((game) => ({
                    releaseDate: game.releaseDate,
                }));


                    // Calculate genre and platform distributions
                    const genreCount: { [key: string]: number } = {};
                    const platformCount: { [key: string]: number } = {};
    
                    userGames.forEach((game) => {
                        // Genre distribution
                        if (game.genre) {
                            genreCount[game.genre] = (genreCount[game.genre] || 0) + 1;
                        }
                        // Platform distribution
                        if (game.platformName) {
                            platformCount[game.platformName] = (platformCount[game.platformName] || 0) + 1;
                        }
                    });
    
                    setOwnedGames(ownedCount);
                    setInProgressGames(inProgressCount);
                    setCompletedGames(completedCount);
                    setGenreDistribution(genreCount);
                    setPlatformDistribution(platformCount);
                    setWishlistGames(wishlist);
                    setReleaseDates(releaseDates); 
                } else {
                    console.error('Failed to fetch games');
                }
            } catch (error) {
                console.error('An error occurred while fetching games:', error);
            }
        };
    
        fetchUserGames();
    }, []);
    
    return (
        <main className={styles.container}>
        <h2 className={styles.title}>Gaming Statistics</h2>
        <div className={styles.analyticsContent}>
            <div className={styles.statsContainer}>
                <div className={styles.listContainer}>
                    <h3>My Game Library</h3>
                    <ul>
                        <li>{ownedGames} games owned</li>
                    </ul>
                </div>
                <div className={styles.listContainer}>
                    <h3>Progress Tracker</h3>
                    <ul>
                            <li>{(ownedGames > 0 ? ((ownedGames - inProgressGames - completedGames) / ownedGames * 100).toFixed(2) : 0)}% not started</li>
                            <li>{(ownedGames > 0 ? (inProgressGames / ownedGames * 100).toFixed(2) : 0)}% in progress</li>
                            <li>{(ownedGames > 0 ? (completedGames / ownedGames * 100).toFixed(2) : 0)}% completed</li>
                        </ul>
                </div>
                <div className={styles.listContainer}>
                    <h3>Genre Distribution</h3>
                    <ul>
                        {Object.keys(genreDistribution).map((genre) => (
                            <li key={genre}>{genre}: {genreDistribution[genre]}</li>
                        ))}
                    </ul>
                </div>
                <div className={styles.listContainer}>
                    <h3>Platform Distribution</h3>
                    <ul>
                        {Object.keys(platformDistribution).map((platform) => (
                            <li key={platform}>{platform}: {platformDistribution[platform]}</li>
                        ))}
                    </ul>
                </div>
                <div className={styles.listContainer}>
                        <h3>Wishlist Analysis</h3>
                        <ul>
                            <li>{wishlistGames.length} games in wishlist</li>
                        </ul>
                    </div>

            </div>
            <div className={styles.chartsContainer}>
                <div className={styles.chart}>
                    <h3>Genre Distribution</h3>
                    <CustomRadarChart data={genreDistribution}/>
                </div>
                <div className={styles.chart}>
                    <h3>Platform Distribution</h3>
                    <CustomPieChart data={platformDistribution}/>
                </div>
                {/* New histogram chart showing release date distribution */}
                <div className={styles.chart}>
                        <h3>Release Date Distribution</h3>
                        <CustomHistogramChart data={releaseDates}/>
                    </div>
            </div>
        </div>
    </main>
    );
};

export default AnalyticsPage;
