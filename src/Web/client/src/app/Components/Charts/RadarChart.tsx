'use client';

import React, { useEffect, useState } from 'react';
import { RadarChart, Radar, PolarGrid, PolarAngleAxis, PolarRadiusAxis, Legend } from 'recharts';

const CustomRadarChart: React.FC<{ data: { [key: string]: number } }> = ({ data }) => {
    const [chartData, setChartData] = useState<any[]>([]);

    useEffect(() => {
        // Prepare data for the RadarChart
        const formattedData = Object.keys(data).map((genre) => ({
            subject: genre,
            A: data[genre],
            fullMark: Math.max(...Object.values(data)),
        }));

        setChartData(formattedData);
    }, [data]); // Re-run this effect whenever the `data` prop changes

    return (
        <RadarChart
            outerRadius="60%" /* Adjusting the size to be more responsive */
            width={350}
            height={350}  /* Fixed height */
            data={chartData}
        >
            <PolarGrid />
            <PolarAngleAxis dataKey="subject" />
            <PolarRadiusAxis angle={30} domain={[0, Math.max(...chartData.map(d => d.A))]} tick={false} />
            <Radar name="You" dataKey="A" stroke="#171D25" fill="#1A9FFF" fillOpacity={0.8} />
            {/* <Legend /> */}
        </RadarChart>
    );
};

export default CustomRadarChart;
