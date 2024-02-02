import React, { useContext, useState } from 'react';
import { CurveFitType, CurveType } from '../../models/CurveTypes';
import { UserContext } from '../../store/userContext';
import { createUser } from '../../dbUtility/createUser';
import { createArchive } from '../../dbUtility/createArchive';
import { ArchivePostParamsType } from '../../models/ArchiveTypes';
import SavePlotForm from './SavePlotForm';

interface SavePlotProps {
    data: CurveType;
    fitType: CurveFitType['value'];
}

export default function SavePlot({ data, fitType }: SavePlotProps) {
    const [user, setUser] = useContext(UserContext);

    const [showForm, setShowForm] = useState(false);
    const [submitting, setSubmitting] = useState(false);

    const [saved, setSaved] = useState(false);
    const [message, setMessage] = useState('');

    const handleSavePlot = (
        e: React.FormEvent<HTMLFormElement>,
        input: string,
    ) => {
        e.preventDefault();

        if (message.length > 0) setMessage('')
        setSubmitting(true)

        const { id } = user

        // Create a new user if there is no valid id saved in local storage
        if (id == 0) {
            createUser()
                .then((newUser) => {
                    if (newUser?.id) {
                        setUser({ id: newUser.id });
                    }
                    else throw new Error('No user id returned from server')
                })
                .catch((err) => {
                    console.error(err)
                    setSubmitting(false)
                    return setMessage('Error creating user. Please try again later')
                });
        }

        // Save the plot to user archives
        const params: ArchivePostParamsType = {
            Name: input,
            UserId: id,
            FitType: fitType,
            ...data,
        }
        createArchive(params)
            .then((data) => {
                if (data?.Id) {
                    setShowForm(false)
                    setSaved(true)
                    setMessage('Plot saved successfully')

                    const newArchives = user.archives ? [...user.archives, data] : [data]
                    setUser({ ...user, archives: newArchives })
                }
                else throw new Error('No plot id returned from server')
            })
            .catch((err) => {
                console.error(err);
                setMessage('Something went wrong. Please try again later.')
            })
            .finally(() => setSubmitting(false));
    }


    return (
        <>
            {showForm && (
                <SavePlotForm
                    handleSavePlot={handleSavePlot}
                    hideForm={() => setShowForm(false)}
                    submitting={submitting}
                />
            )}
            {!showForm && !saved && (
                <button
                    type="button"
                    onClick={() => setShowForm(true)}
                >
                    Save to My Archieve
                </button>
            )}
            {message.length > 0 && (
                <p className={!saved ? "error" : ""}>{message}</p>
            )}
        </>
    )
}