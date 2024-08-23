'use client';
import React, { useEffect, useState } from 'react';
import { PieChart, Pie, Legend, Cell } from "recharts";

const COLORS = ['#0088FE', '#00C49F', '#FFBB28', '#FF8042', '#FF4561', '#66CCFF'];

const CustomPieChart: React.FC = () => {
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

        // Prepare data for the PieChart
        const data = Object.keys(genreCounts).map((genre, index) => ({
            name: genre,
            value: genreCounts[genre],
            color: COLORS[index % COLORS.length]  // Assign a color from the COLORS array
        }));

        setChartData(data);
    }, []);

    return (
        <PieChart width={730} height={250}>
            <Pie 
                data={chartData} 
                dataKey="value" 
                nameKey="name" 
                cx="50%" 
                cy="50%" 
                innerRadius={60} 
                outerRadius={80} 
                label
            >
                {chartData.map((entry, index) => (
                    <Cell key={`cell-${index}`} fill={entry.color} />
                ))}
            </Pie>
            <Legend/>
        </PieChart>
    );
};

export default CustomPieChart;
