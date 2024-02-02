import { ArchivedCurveType } from "../../models/ArchiveTypes";
import Equation from "../Equation";

interface ArchiveCardProps {
    archive: ArchivedCurveType;
    showArchive: (archiveInd: number) => void;
    isActive: boolean;
}

export default function ArchiveCard({
    archive,
    showArchive,
    isActive,
}: ArchiveCardProps) {
    const date: string = new Date(archive.Timestamp).toLocaleDateString()

    return (
        <li className={`archiveCard${isActive ? " activeCard" : ""}`}>
            <p><b>{archive.Name}</b></p>
            <p><i>Saved on {date}</i></p>
            <Equation Equation={archive.Equation} />
            {!isActive && (
                <button
                    type="button"
                    onClick={() => showArchive(archive.Id)}
                >
                    Show
                </button>
            )}
        </li>
    )
}
