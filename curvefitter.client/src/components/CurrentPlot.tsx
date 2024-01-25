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
    userDataPoints: DataPointType[];
    fitDataPoints?: DataPointType[];
}

export default function CurrentPlot({
    userDataPoints,
    fitDataPoints = [],
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
                    dataKey="X"
                    name="X"
                />
                <XAxis
                    xAxisId="fitPoints"
                    type="number"
                    dataKey="X"
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
                    dataKey="Y"
                    stroke="#101c3e"
                    type="monotone"
                    dot={false}
                />"
                <Scatter
                    name="User Points"
                    xAxisId="userPoints"
                    data={userDataPoints}
                    dataKey="Y"
                    fill="#1da1b2"
                />
                <Legend />
            </ComposedChart>
        </ResponsiveContainer>
    );
}
