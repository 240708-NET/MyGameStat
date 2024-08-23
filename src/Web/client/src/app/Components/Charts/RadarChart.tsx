'use client';
import React, { useEffect, useState } from 'react';
import { RadarChart, Radar, PolarGrid, PolarAngleAxis, PolarRadiusAxis, Legend } from "recharts";

const CustomRadarChart: React.FC = () => {
    const [chartData, setChartData] = useState<any[]>([]);

    useEffect(() => {
        const loggedInUser = localStorage.getItem('loggedInUser');
        const savedGames = JSON.parse(localStorage.getItem('userGames') || '[]');

        // Filter games to show only those belonging to the logged-in user
        const userGames = savedGames.filter((game: any) => game.user === loggedInUser);

        // Calculate the distribution of games by genre (or any other property)
        const genreCounts = userGames.reduce((acc: any, game: any) => {
            acc[game.genre] = (acc[game.genre] || 0) + 1;
            return acc;
        }, {});

        // Prepare data for the RadarChart
        const data = Object.keys(genreCounts).map((genre) => ({
            subject: genre,
            A: genreCounts[genre],
            fullMark: Math.max(...Object.values(genreCounts) as number[]),
        }));

        setChartData(data);
    }, []);

    return (
        <RadarChart outerRadius={140} width={730} height={400} data={chartData}>
            <PolarGrid />
            <PolarAngleAxis dataKey="subject" />
            <PolarRadiusAxis angle={30} domain={[0, Math.max(...chartData.map(d => d.A))]} tick={false} />
            <Radar name="You" dataKey="A" stroke="#171D25" fill="#1A9FFF" fillOpacity={0.8} />
            <Legend />
        </RadarChart>
    );
};

export default CustomRadarChart;
