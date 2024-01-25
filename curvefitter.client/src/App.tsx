import React from 'react';
import './App.css';
import { Layout, PlotView } from './components';

export default function App() {
    return (
        <Layout>
            <PlotView />
            {/* TBD: add UI for displaying archived plots
                <UserPlots />
             */}
        </Layout>
    )
}
