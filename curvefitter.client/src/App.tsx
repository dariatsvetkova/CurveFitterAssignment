import React from 'react';
import './App.css';
import { Layout, PlotView, UserPlots } from './components';

export default function App() {
    return (
        <Layout>
            <PlotView />
            <UserPlots />
        </Layout>
    )
}
