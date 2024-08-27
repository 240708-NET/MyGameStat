'use client';

import React, { useEffect, useState } from 'react';
import { PieChart, Pie, Legend, Cell } from 'recharts';

const COLORS = ['#0088FE', '#00C49F', '#FFBB28', '#FF8042', '#FF4561', '#66CCFF'];

interface CustomPieChartProps {
    data: { [key: string]: number };
}

const CustomPieChart: React.FC<CustomPieChartProps> = ({ data }) => {
    const [chartData, setChartData] = useState<any[]>([]);

    useEffect(() => {
        // Prepare data for the PieChart
        const formattedData = Object.keys(data).map((platform, index) => ({
            name: platform,
            value: data[platform],
            color: COLORS[index % COLORS.length],
        }));

        setChartData(formattedData);
    }, [data]); // Re-run this effect whenever the `data` prop changes

    return (
        <PieChart
            width={310}  // Adjust to the full width of the container
            height={310}  // Adjust height to fit better with the container
        >
            <Pie
                data={chartData}
                dataKey="value"
                nameKey="name"
                cx="50%"
                cy="50%"
                innerRadius="40%"  // Adjust inner and outer radius for better sizing
                outerRadius="60%"
                label
            >
                {chartData.map((entry, index) => (
                    <Cell key={`cell-${index}`} fill={entry.color} />
                ))}
            </Pie>
            <Legend />
        </PieChart>
    );
};

export default CustomPieChart;
