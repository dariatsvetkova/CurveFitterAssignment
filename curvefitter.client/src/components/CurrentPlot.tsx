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
} from 'recharts';

const data = [
    { x: 100, userY: 200, z: 200 },
    { x: 110, fitY: 180, z: 200 },
    { x: 120, fitY: 200, z: 260 },
    { x: 140, fitY: 250, z: 280 },
    { x: 150, fitY: 400, z: 500 },
    { x: 170, userY: 300, z: 400 },
];

export default function CurrentPlot() {
    return (
        <ResponsiveContainer width="100%" height={400}>
            <ComposedChart
                data={data}
                margin={{
                    top: 20,
                    right: 20,
                    bottom: 20,
                    left: 20,
                }}
            >
                <CartesianGrid />
                <XAxis type="number" dataKey="x" name="stature" unit="cm" />
                <YAxis type="number" dataKey="fitY" name="weight" unit="kg" />
                <Tooltip cursor={{ strokeDasharray: '3 3' }} />
                <Scatter name="User Points" dataKey="userY" fill="#8884d8" />
                <Line name="Approximated Curve" dataKey="fitY" stroke="#82ca9d" />"
            </ComposedChart>
        </ResponsiveContainer>
    );
}
