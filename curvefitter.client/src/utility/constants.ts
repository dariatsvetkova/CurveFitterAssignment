import { CurveFitType, ServerResponseType } from "../models/CurveTypes";

export const curveFitOptions: CurveFitType[] = [
    {
        name: 'linear',
        minDataPoints: 2,
    },
    {
        name: 'quadratic',
        minDataPoints: 3,
    },
    {
        name: 'cubic',
        minDataPoints: 4,
    },
]

export const defaultServerResponse: ServerResponseType = {
    data: undefined,
    loading: false,
    error: false,
}
