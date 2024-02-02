import { useState } from 'react';
import './App.css';
import { ArchiveList, ArchiveView, Layout, PlotView } from './components';
import { ArchivedCurveType } from './models/ArchiveTypes';

export default function App() {
    const [archiveToDisplay, setArchiveToDisplay] =
        useState<ArchivedCurveType['Id'] | null>(null)

    const showArchive = (archiveId: ArchivedCurveType['Id']) => {
        setArchiveToDisplay(archiveId)
    }

    const hideArchive = () => {
        setArchiveToDisplay(null)
    }

    return (
        <Layout>
            {archiveToDisplay !== null ? (
                <ArchiveView
                    archiveToDisplay={archiveToDisplay}
                    hideArchive={hideArchive}
                />
            ) : (
                <PlotView />
            )}
            <ArchiveList
                showArchive={showArchive}
                archiveToDisplay={archiveToDisplay}
            />
        </Layout>
    )
}
