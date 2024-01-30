import React from 'react';
import { CurveFitType, DataPointType } from '../models/CurveTypes';
import { curveFitOptions } from '../utility/constants';

interface UserInputProps {
    fit: CurveFitType;
    handleFitChange: (e: React.ChangeEvent<HTMLSelectElement>) => void;
    dataPoints: DataPointType[];
    handleDataPointChange: (
        e: React.ChangeEvent<HTMLInputElement>,
        index: number
    ) => void;
    handleDataPointsAmount: (i?: number) => void;
    handleSubmit: (e: React.FormEvent<HTMLFormElement>) => void;
    loading: boolean;
}

export default function UserInputs({
    fit,
    handleFitChange,
    dataPoints,
    handleDataPointChange,
    handleDataPointsAmount,
    handleSubmit,
    loading,
}: UserInputProps) {
    return (
        <form onSubmit={(e) => handleSubmit(e)}>
            <label htmlFor="curveFitType">Select desired curve fit type:</label>
            <select
                id="curveFitType"
                name="curveFitType"
                onChange={(e) => handleFitChange(e)}
                required
            >
                <option value="" disabled>
                    Select a curve fit type
                </option>
                {curveFitOptions.map((option: CurveFitType) => (
                    <option value={option.value} key={option.name}>
                        {option.name}
                    </option>
                ))}
            </select>
            {dataPoints.map((point, index) => (
                <fieldset key={`dataPoint-${index}`} className="pointInput">
                    <legend>
                        Data point {index + 1}
                        {index < fit.minDataPoints && (
                            <abbr title="required">*</abbr>
                        )}
                    </legend>

                    <label>x:</label>
                    <input
                        name={`X-${index}`}
                        value={point.X}
                        type="number"
                        required
                        onChange={(e) => handleDataPointChange(e, index)}
                    />

                    <label>y:</label>
                    <input
                        name={`Y-${index}`}
                        value={point.Y}
                        type="number"
                        required={index < fit.minDataPoints}
                        onChange={(e) => handleDataPointChange(e, index)}
                    />

                    {index >= fit.minDataPoints && (
                        <button
                            type="button"
                            onClick={() => handleDataPointsAmount(index)}
                        >
                            Remove
                        </button>
                    )}
                </fieldset>
                )
            )}

            <div className="formButtonContainer">
                <button
                    type="button"
                    onClick={() => handleDataPointsAmount()}
                    disabled={loading}
                >
                    Add data point
                </button>

                <button
                    type="submit"
                    disabled={loading}
                >
                    Submit
                </button>
            </div>
        </form>
    )
}