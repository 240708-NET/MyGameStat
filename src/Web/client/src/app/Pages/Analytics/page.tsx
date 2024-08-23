'use client';

import React, { useEffect, useState } from 'react';
import styles from './Analytics.module.css';
import CustomRadarChart from '../../Components/Charts/RadarChart';
import CustomPieChart from '../../Components/Charts/PieChart';

const AnalyticsPage: React.FC = () => {
    const [ownedGames, setOwnedGames] = useState<number>(0);
    // const [wishlistedGames, setWishlistedGames] = useState<number>(0);
    const [inProgressGames, setInProgressGames] = useState<number>(0);
    const [completedGames, setCompletedGames] = useState<number>(0);

    useEffect(() => {
        const loggedInUser = localStorage.getItem('loggedInUser');
        const savedGames = JSON.parse(localStorage.getItem('userGames') || '[]');

        // Filter games to show only those belonging to the logged-in user
        const userGames = savedGames.filter((game: any) => game.user === loggedInUser);

        // Calculate game statistics
        const ownedCount = userGames.length;
        // const wishlistedCount = userGames.filter((game: any) => game.status === 'Wishlist').length;
        const inProgressCount = userGames.filter((game: any) => game.status === 'Playing').length;
        const completedCount = userGames.filter((game: any) => game.status === 'Completed').length;

        setOwnedGames(ownedCount);
        // setWishlistedGames(wishlistedCount);
        setInProgressGames(inProgressCount);
        setCompletedGames(completedCount);
    }, []);

    return (
        <main className={styles.container}>
            <div className={styles.sidebar}>
                <div className={styles.listContainer}>
                    <h3>My Game Library</h3>
                    <ul>
                        <li>{ownedGames} games owned</li>
                        {/* <li>{wishlistedGames} games wish listed</li> */}
                    </ul>
                </div>
                <div className={styles.listContainer}>
                    <h3>Progress Tracker</h3>
                    <ul>
                        <li>{(inProgressGames / ownedGames * 100).toFixed(2)}% in progress</li>
                        <li>{(completedGames / ownedGames * 100).toFixed(2)}% completed</li>
                    </ul>
                </div>
            </div>
            <div className={styles.chartContainer}>
                <div className={styles.radarChartContainer}>
                    <CustomRadarChart />
                </div>
                <div className={styles.pieChartContainer}>
                    <CustomPieChart />
                </div>
            </div>
        </main>
    );
};

export default AnalyticsPage;
