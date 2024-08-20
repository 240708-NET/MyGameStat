'use client';
import { RadarChart, Radar, PolarGrid, PolarAngleAxis, PolarRadiusAxis, Legend  } from "recharts";

const radarChart: React.FC = () => {

    const dummyData = [
        {
          "subject": "Action",
          "A": 6,
          "fullMark": 6
        },
        {
          "subject": "Adventure",
          "A": 3,
          "fullMark": 6
        },
        {
          "subject": "RPG",
          "A": 4,
          "fullMark": 6
        },
        {
          "subject": "Simulation",
          "A": 1,
          "fullMark": 6
        },
        {
          "subject": "Strategy",
          "A": 5,
          "fullMark": 6
        },
        {
          "subject": "Sports",
          "A": 2,
          "fullMark": 6
        }
      ]

    return (
        <RadarChart outerRadius={140} width={730} height={400} data={dummyData}>
            <PolarGrid />
            <PolarAngleAxis dataKey="subject" />
            <PolarRadiusAxis angle={30} domain={[0, 6]} />
            <Radar name="You" dataKey="A" stroke="#171D25" fill="#1A9FFF" fillOpacity={0.8} />
        </RadarChart>
    );
};

export default radarChart;