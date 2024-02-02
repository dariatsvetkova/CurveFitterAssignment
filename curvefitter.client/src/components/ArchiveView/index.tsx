import { useContext, useEffect, useState } from 'react';
import Graph from '../Graph';
import Equation from '../Equation';
import { UserContext } from '../../store/userContext';
import { ArchivedCurveType } from '../../models/ArchiveTypes';
import DeleteArchive from './DeleteArchive';

interface ArchiveViewProps {
    archiveToDisplay: ArchivedCurveType['Id'];
    hideArchive: () => void;
}

export default function ArchiveView({
    archiveToDisplay,
    hideArchive,
}: ArchiveViewProps) {
    const [user] = useContext(UserContext);
    const [archive, setArchive] = useState<ArchivedCurveType>()

    useEffect(() => {
        const archiveFromStorage = user?.archives?.find(a => a.Id === archiveToDisplay)
        if (archiveFromStorage) {
            setArchive(archiveFromStorage)
        }
    }, [archiveToDisplay, user])

    return (
        <>
            {archive ? (
                <>
                    <h2>{archive.Name}</h2>

                    <div className="archiveAreaContainer">
                        <button
                            type="button"
                            onClick={hideArchive}
                            className="closeButton"
                            aria-label="Close archive"
                        >
                            X
                        </button>

                        <Equation Equation={archive.Equation} />
                        < Graph
                            userDataPoints={archive.UserDataPoints}
                            fitDataPoints={archive.FitDataPoints}
                        />

                        <p><i>Saved on {archive.Timestamp}</i></p>
                        <DeleteArchive
                            archiveId={archive.Id}
                            hideArchive={hideArchive}
                        />
                    </div>
                </>
            ) : (
                <p className="error">Error: archive not found</p>
            )}
        </>
    )
}
