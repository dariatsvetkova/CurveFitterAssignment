import { CurveRequestParamsType, DataPointType } from "../models/CurveTypes";

// TBD: add tests
export function validateInputs(
    params: CurveRequestParamsType
): {
    error: boolean;
    message: string;
} {
    const { fitType, userPoints } = params;

    if (userPoints.length < fitType + 1) {
        return {
            error: true,
            message: `Please enter at least ${fitType + 1} data points for this type of curve.`,
        }
    }

    if ((new Set(userPoints)).size !== userPoints.length) {
        return {
            error: true,
            message: `Please remove duplicate data points`,
        }
    }

    const xValues = userPoints.map((point: DataPointType) => point.X);

    if (
        xValues.some((x: number, ind: number) => xValues.slice(ind + 1).includes(x))
    ) {
        return {
            error: true,
            message: `Please remove duplicate x values`,
        }
    }

    return {
        error: false,
        message: '',
    }
}