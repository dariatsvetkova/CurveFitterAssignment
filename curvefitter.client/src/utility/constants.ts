import { CurveFitType, CurveServerResponseType } from "../models/CurveTypes";

export const curveFitOptions: CurveFitType[] = [
    {
        name: 'linear',
        value: 1,
        minDataPoints: 2,
    },
    {
        name: 'quadratic',
        value: 2,
        minDataPoints: 3,
    },
    {
        name: 'cubic',
        value: 3,
        minDataPoints: 4,
    },
]

export const defaultServerResponse: CurveServerResponseType = {
    data: undefined,
    loading: false,
    error: false,
    message: '',
}

export const PRECISION = 3;
