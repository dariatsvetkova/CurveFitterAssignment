import { useContext, useEffect, useState } from "react";
import { ArchivedCurveType } from "../../models/ArchiveTypes";
import { UserContext } from "../../store/userContext";
import { getArchives } from "../../dbUtility/getArchives";
import ArchiveCard from "./ArchiveCard";

interface ArchiveListProps {
    showArchive: (archiveId: ArchivedCurveType['Id']) => void;
    archiveToDisplay: ArchivedCurveType['Id'] | null;
}

export default function ArchiveList({
    showArchive,
    archiveToDisplay,
}: ArchiveListProps) {
    const [user, setUser] = useContext(UserContext);
    const [loading, setLoading] = useState(false);

    useEffect(() => {
        if (user.id > 0) {
            setLoading(true);

            getArchives(user.id)
                .then((data: ArchivedCurveType[]) => {
                    data.length > 0 && setUser({
                        ...user, 
                        archives: data
                    })
                })
                .catch((err: string) => {
                    console.error(err);
                })
                .finally(() => setLoading(false))
        }
    }, [])

    return (
        <div className="archiveListContainer">
            <h2>My Archive</h2>
            {loading ? (
                <p>Loading...</p> 
            ) : (
                <ul className="archiveList">
                    {user.archives?.sort((a, b) => Date.parse(b.Timestamp) - Date.parse(a.Timestamp))
                        .map((archive) => (
                        <ArchiveCard
                            key={archive.Id}
                            archive={archive}
                            showArchive={showArchive}
                            isActive={archiveToDisplay === archive.Id}
                        />
                    ))}
                </ul>
            )}
        </div>
    )
}
