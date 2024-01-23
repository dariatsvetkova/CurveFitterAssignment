import React from 'react';
import {
    Scatter,
    XAxis,
    YAxis,
    CartesianGrid,
    Tooltip,
    ResponsiveContainer,
    ComposedChart,
    Line,
    Legend,
} from 'recharts';
import { DataPointType } from '../models/CurveTypes';

interface CurrentPlotProps {
    userDataPoints?: DataPointType[];
    fitDataPoints?: DataPointType[];
}

const mockUserDataPoints = [
    { x: 150, y: 400 },
    { x: 110, y: 180 },
];

const mockFitDataPoints = [
    { x: 100, y: 200 },
    { x: 120, y: 200 },
    { x: 140, y: 250 },
    { x: 170, y: 300 },
];

export default function CurrentPlot({
    userDataPoints = mockUserDataPoints,
    fitDataPoints = mockFitDataPoints,
}: CurrentPlotProps) {
    return (
        <ResponsiveContainer width="100%" height={400}>
            <ComposedChart
                margin={{
                    top: 20,
                    right: 20,
                    bottom: 20,
                    left: 20,
                }}
            >
                <CartesianGrid />
                <XAxis
                    xAxisId="userPoints"
                    type="number"
                    dataKey="x"
                    name="X"
                />
                <XAxis
                    xAxisId="fitPoints"
                    type="number"
                    dataKey="x"
                    hide={true}
                />
                <YAxis
                    type="number"
                    name="Y"
                />
                <Tooltip cursor={{ strokeDasharray: '3 3' }} />
                <Line
                    name="Approximated Curve"
                    xAxisId="fitPoints"
                    data={fitDataPoints}
                    dataKey="y"
                    stroke="#101c3e"
                    type="monotone"
                    dot={false}
                />"
                <Scatter
                    name="User Points"
                    xAxisId="userPoints"
                    data={userDataPoints}
                    dataKey="y"
                    fill="#1da1b2"
                />
                <Legend />
            </ComposedChart>
        </ResponsiveContainer>
    );
}
