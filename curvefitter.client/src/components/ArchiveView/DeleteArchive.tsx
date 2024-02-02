import { useContext, useRef, useState } from 'react';
import { UserContext } from '../../store/userContext';
import { ArchiveDeleteParamsType, ArchivedCurveType } from '../../models/ArchiveTypes';
import { defaultServerResponse } from '../../utility/constants';
import { deleteArchive } from '../../dbUtility/deleteArchive';

interface DeleteArchiveProps {
    archiveId: ArchivedCurveType['Id'];
    hideArchive: () => void;
}

export default function DeleteArchive({
    archiveId,
    hideArchive,
}: DeleteArchiveProps) {
    const [user, setUser] = useContext(UserContext);
    const [response, setResponse] = useState(defaultServerResponse)

    const modalRef = useRef<HTMLDialogElement>(null)

    const handleDelete = (
        archiveId: ArchiveDeleteParamsType['ArchiveId'],
        userId: ArchiveDeleteParamsType['UserId'],
    ) => {
        modalRef.current && modalRef.current.close()

        setResponse({
            loading: true,
            error: false,
            message: '',
        })

        const params: ArchiveDeleteParamsType = {
            UserId: userId,
            ArchiveId: archiveId,
        }

        deleteArchive(params)
            .then(() => {
                setResponse({
                    loading: false,
                    error: false,
                    message: 'Plot deleted successfully',
                })

                const updatedArchives = user.archives?.filter(a => a.Id !== archiveId)
                setUser({ ...user, archives: updatedArchives })

                setTimeout(() => {
                    hideArchive()
                }, 1500)
            })
            .catch((err: string) => {
                console.error(err);
                setResponse({
                    loading: false,
                    error: true,
                    message: 'Failed to delete plot',
                })
            })
    }

    return (
        <>
            <button
                type="button"
                onClick={() => modalRef.current && modalRef.current.showModal()}
                disabled={response.loading}
            >
                Remove from My Archieves
            </button>
            {response.error && response.message && response.message.length > 0 && (
                <p className="error">{response.message}</p>
            )}

            <dialog ref={modalRef}>
                <p>Delete the plot permanently?</p>

                <div className="formButtonContainer">
                    <button
                        type="button"
                        autoFocus
                        onClick={() => modalRef.current && modalRef.current.close()}
                    >
                        Close
                    </button>

                    <button
                        type="button"
                        onClick={() => handleDelete(archiveId, user.id)}
                    >
                        Ok
                    </button>
                </div>
            </dialog>
        </>
    )
}