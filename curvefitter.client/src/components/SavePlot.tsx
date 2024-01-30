import React, { useContext, useState } from 'react';
import { CurveType } from '../models/CurveTypes';
import { UserContext } from '../store/userContext';
import { createUser } from '../dbUtility/createUser';

interface SavePlotProps {
    data: CurveType;
}

export default function SavePlot({ data }: SavePlotProps) {
    const [user, setUser] = useContext(UserContext);
    const [message, setMessage] = useState('');

    const handleSavePlot = (data: CurveType) => {
        if (message.length > 0) setMessage('');
        const { id } = user;

        // Create a new user if there is no id saved in local storage
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
                    return setMessage('Error creating user. Please try again later')
                });
        }

        // Save the plot to user archives
    }

    return (
        <>
            <button
                type="button"
                onClick={() => handleSavePlot(data)}
            >
                Save to My Plots
            </button>
            {message.length > 0 && <p className="error">{message}</p>}
        </>
    )
}