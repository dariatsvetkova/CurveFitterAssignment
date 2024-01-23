import React, { useEffect, useState } from 'react';
import { CurrentPlot, UserInputs } from '.';
import { CurveFitType, DataPointType, ServerResponseType } from '../models/CurveTypes';
import { curveFitOptions, defaultServerResponse } from '../utility/constants';

export default function PlotView() {
    // User input state

    const [fit, setFit] = useState<CurveFitType>(curveFitOptions[0]);

    const handleFitChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
        setFit(curveFitOptions[parseInt(e.target.value)]);
    }

    const [dataPoints, setDataPoints] = useState<DataPointType[]>([]);

    const handleDataPointChange = (
        e: React.ChangeEvent<HTMLInputElement>,
        index: number
    ) => {
        const dataPointType = e.target.name.split('-')[0] as 'x' | 'y';
        const newDataPoints = [...dataPoints];

        newDataPoints[index][dataPointType] = parseInt(e.target.value);
        setDataPoints(newDataPoints);
    }

    // Server request state

    const [response, setResponse] = useState<ServerResponseType>(defaultServerResponse);

    console.log('data from server: ', response);

    useEffect(() => {
        setResponse({ ...response, loading: true });

        fetch(
            'https://localhost:7228/api/curvefit',
            /*?points = ${ encodeURIComponent(points) } & type=${ encodeURIComponent(curveType) }*/
            {
                method: "GET",
                headers: {
                    Accept: "application/json; charset=utf-8",
                    Origin: 'https://localhost:5173'
                },
                cache: "default",
            }

        )
            .then((res) => {
                if (res.ok) {
                    return res.json()
                } else {
                    throw new Error('Server request failed')
                }
            })
            .then((data) => setResponse({
                data, 
                loading: false,
                error: false,
            }))
            .catch((err) => {
                console.error(err)
                setResponse({
                    data: undefined,
                    loading: false,
                    error: true,
                })
            });
    }, [])

    return (
        <div>
            <UserInputs
                fit={fit}
                dataPoints={dataPoints}
                handleFitChange={handleFitChange}
                handleDataPointChange={handleDataPointChange}
            />
            {response.error ? (
                <p>Something went wrong. Please try again later.</p>
            ) : (
                <>
                    {
                        response.loading ? (
                            <p>Loading...</p>
                        ) : (
                            <CurrentPlot
                                userDataPoints={response.data?.UserDataPoints}
                                fitDataPoints={response.data?.FitDataPoints}
                            />
                        )
                    }
                </>
            )}
        </div>
    )
}
