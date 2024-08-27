'use client';

import React, { useEffect, useState } from 'react';
import { BarChart, Bar, XAxis, YAxis, CartesianGrid, Tooltip, Legend, ResponsiveContainer } from 'recharts';

interface CustomHistogramChartProps {
    data: { releaseDate: string }[];
}

const CustomHistogramChart: React.FC<CustomHistogramChartProps> = ({ data }) => {
    const [chartData, setChartData] = useState<any[]>([]);

    useEffect(() => {
        console.log("Original data passed to histogram:", data); // Log the original data

        // Process the data to group by release year
        const groupedData = data.reduce((acc: any, game) => {
            const date = new Date(game.releaseDate);
            const year = date.getFullYear(); // Extract year from release date

            if (!isNaN(year)) { // Ensure it's a valid year
                console.log(`Parsed year for releaseDate ${game.releaseDate}: ${year}`);
                acc[year] = (acc[year] || 0) + 1;
            } else {
                console.error(`Invalid date encountered: ${game.releaseDate}`);
            }

            return acc;
        }, {});

        console.log("Grouped data by year:", groupedData); // Log the grouped data

        // Convert the grouped data into the format required for the BarChart
        const formattedData = Object.keys(groupedData).map(year => ({
            name: year,
            count: groupedData[year]
        }));

        console.log("Formatted data for chart:", formattedData); // Log the formatted data

        setChartData(formattedData);
    }, [data]);

    return (
        <ResponsiveContainer width="100%" height={300}>
            <BarChart data={chartData} margin={{ top: 20, right: 30, left: 20, bottom: 5 }}>
                <CartesianGrid strokeDasharray="3 3" />
                <XAxis dataKey="name" />
                <YAxis />
                <Tooltip />
                {/* <Legend /> */}
                <Bar dataKey="count" fill="#3faba2" />
            </BarChart>
        </ResponsiveContainer>
    );
};

export default CustomHistogramChart;
