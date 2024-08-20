'use client';

import React from 'react';
import styles from './Analytics.module.css';
import RadarChart from '../../Components/Charts/RadarChart';
import PieChart from '../../Components/Charts/PieChart';

const AnalyticsPage: React.FC = () => {
    return (
        <main className={styles.container}>
            <div className={styles.sidebar}>
                <div className={styles.listContainer}>
                    <h3>My Game Library</h3>
                    <ul>
                        <li>X games owned</li>
                        <li>X games wish listed</li>
                    </ul>
                </div>
                <div className={styles.listContainer}>
                    <h3>Progress Tracker</h3>
                    <ul>
                        <li>X% in progress</li>
                        <li>X% completed</li>
                    </ul>
                </div>
            </div>
            <div className={styles.chartContainer}>
                <div className={styles.radarChartContainer}>
                    <RadarChart />
                </div>
                <div className={styles.pieChartContainer}>
                    <PieChart />
                </div>
            </div>
        </main>
    );
};

export default AnalyticsPage;
