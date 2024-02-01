import React, { useContext, useState } from 'react';
import { CurveFitType, CurveType } from '../models/CurveTypes';
import { UserContext } from '../store/userContext';
import { createUser } from '../dbUtility/createUser';
import { createArchive } from '../dbUtility/createArchive';
import { ArchivePostParamsType } from '../models/ArchiveTypes';

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

    const handleSavePlot = (e: React.FormEvent<HTMLFormElement>) => {
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
            Name: "plot name",
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
                <form
                    onSubmit={(e) => handleSavePlot(e)}
                >
                    <div className="inputContainer">
                        <label htmlFor="name" className="formLabel">
                            Give your plot a distinct name:
                        </label>
                        <input
                            type="text"
                            id="name"
                            name="name"
                            placeholder="Current to Potential Plot"
                            required
                        />
                    </div>

                    <div className="formButtonContainer">
                        <button
                            type="button"
                            onClick={() => setShowForm(false)}
                            disabled={submitting}
                        >
                            Cancel
                        </button>

                        <button
                            type="submit"
                            disabled={submitting}
                        >
                            Save
                        </button>
                    </div>
                </form>
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