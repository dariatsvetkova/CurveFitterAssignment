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
}

export default function UserInputs({
    fit,
    handleFitChange,
    dataPoints,
    handleDataPointChange,
}: UserInputProps) {
    

    return (
        <form>
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
                {curveFitOptions.map((option: CurveFitType, index: number) => (
                    <option value={index} key={option.name}>
                        {option.name}
                    </option>
                ))}
            </select>
            {Array.from({ length: fit ? fit.minDataPoints : 0 })
                .map((_, index) => (
                    <fieldset key={`dataPoint-${index}`}>
                        <legend>
                            Data point {index + 1}
                            <abbr title="required">*</abbr>
                        </legend>
                        <label>x:</label>
                        <input
                            name={`x-${index}`}
                            value={dataPoints[index]?.x}
                            type="number"
                            required
                            onChange={(e) => handleDataPointChange(e, index)}
                        />
                        <label>y:</label>
                        <input
                            name={`y-${index}`}
                            value={dataPoints[index]?.y}
                            type="number"
                            required
                            onChange={(e) => handleDataPointChange(e, index)}
                        />
                    </fieldset>
                )
             )}
        </form>
    )
}