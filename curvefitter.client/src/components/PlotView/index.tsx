import React, { useEffect, useState } from 'react';
import {
    CurveFitType,
    DataPointType,
    CurveServerResponseType,
    CurveRequestParamsType,
} from '../../models/CurveTypes';
import {
    curveFitOptions,
    defaultCurveServerResponse,
} from '../../utility/constants';
import { getCurveData } from '../../dbUtility/getCurveData';
import { validateInputs } from '../../utility/validateInputs';
import SavePlot from './SavePlot';
import Graph from '../Graph';
import UserInputs from './UserInputs';
import Equation from '../Equation';

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
        useState<CurveServerResponseType>(defaultCurveServerResponse);

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

        getCurveData(params)
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
        <>
            <h2>New Plot</h2>
            <div className="plotAreaContainer">
                <UserInputs
                    fit={fit}
                    dataPoints={dataPoints}
                    handleFitChange={handleFitChange}
                    handleDataPointChange={handleDataPointChange}
                    handleDataPointsAmount={handleDataPointsAmount}
                    handleSubmit={handleSumbit}
                    loading={response.loading}
                />
                {response.error && (
                    <p className="error">{response.message}</p>
                )}
                {response.loading && (
                    <p>Loading...</p>
                )}
                {response.data?.UserDataPoints && (
                    <div>
                        <Equation Equation={response.data.Equation} />
                        <Graph
                            userDataPoints={response.data?.UserDataPoints}
                            fitDataPoints={response.data?.FitDataPoints}
                        />

                        <SavePlot data={response.data} fitType={fit.value} />
                    </div>
                )}
            </div>
        </>
    )
}
