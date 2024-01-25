import React, { useEffect, useState } from 'react';
import { CurrentPlot, Equation, UserInputs } from '.';
import {
    CurveFitType,
    DataPointType,
    CurveServerResponseType,
    CurveRequestParamsType,
} from '../models/CurveTypes';
import { curveFitOptions, defaultServerResponse } from '../utility/constants';
import { getCurveData } from '../dbUtility/getData';
import { validateInputs } from '../utility/validateInputs';

export default function PlotView() {
    // User input state

    const [fit, setFit] = useState<CurveFitType>(curveFitOptions[0]);

    const handleFitChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
        setFit(curveFitOptions[parseInt(e.target.value) - 1]);
    }

    const [dataPoints, setDataPoints] =
        useState<DataPointType[]>([]);

    useEffect(() => {
        // Populate missing data points with empty values if user changes fit type
        if (dataPoints.length < fit.minDataPoints) {
            const emptyPoints: DataPointType[] = Array
                .from({ length: fit.minDataPoints })
                .map((_, ind) => (dataPoints[ind] || { X: 0, Y: 0 }));

            setDataPoints(emptyPoints);
        }
    }, [fit]);

    const handleDataPointChange = (
        e: React.ChangeEvent<HTMLInputElement>,
        index: number
    ) => {
        const dataPointType = e.target.name.split('-')[0] as 'X' | 'Y';
        const newDataPoints = [...dataPoints];

        newDataPoints[index][dataPointType] = parseInt(e.target.value);
        setDataPoints(newDataPoints);
    }

    const handleDataPointsAmount = (indexToRemove?: number) => {
        if (indexToRemove === undefined) {
            setDataPoints([...dataPoints, { X: 0, Y: 0 }])
        } else {
            const newDataPoints = [...dataPoints];
            newDataPoints.splice(indexToRemove, 1);
            setDataPoints(newDataPoints);
        }
    }

    // Server request state

    const [response, setResponse] =
        useState<CurveServerResponseType>(defaultServerResponse);

    const handleSumbit = (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        const params: CurveRequestParamsType = { userPoints: dataPoints, fitType: fit.value }

        const { error: validarionError, message: validationMessage } = validateInputs(params)

        if (validarionError) {
            return setResponse({
                data: undefined,
                loading: false,
                error: true,
                message: validationMessage,
            })
        }

        setResponse({ ...response, loading: true });

        getCurveData('api/curvefit', params)
            .then((data) => {
                data && setResponse({
                    data: data,
                    loading: false,
                    error: false,
                    message: '',
                })
            })
            .catch((err) => {
                console.error(err);
                setResponse({
                    data: undefined,
                    loading: false,
                    error: true,
                    message: 'Something went wrong. Please try again later.'
                })
            });
    }

    return (
        <div className="plotAreaContainer">
            <UserInputs
                fit={fit}
                dataPoints={dataPoints}
                handleFitChange={handleFitChange}
                handleDataPointChange={handleDataPointChange}
                handleDataPointsAmount={handleDataPointsAmount}
                handleSubmit={handleSumbit}
            />
            {response.error && (
                <p>{response.message}</p>
            )}
            {response.loading && (
                <p>Loading...</p>
            )}
            {response.data?.UserDataPoints && (
                <div>
                    <Equation Equation={response.data.Equation} />
                    <CurrentPlot
                        userDataPoints={response.data?.UserDataPoints}
                        fitDataPoints={response.data?.FitDataPoints}
                    />
                </div>
            )}
        </div>
    )
}
