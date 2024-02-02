import { UserArchivesServerResponseType } from "../models/ArchiveTypes";
import { CurveFitType, CurveServerResponseType } from "../models/CurveTypes";
import { UserType } from "../models/UserTypes";

export const LOCAL_STORAGE_KEY = 'DTSCurveFitter';

export const LOCAL_STORAGE_ITEMS = {
    user: 'user',
    currentPlot: 'currentPlot',
}

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

export const defaultServerResponse: Omit<CurveServerResponseType, "data"> = {
    loading: false,
    error: false,
    message: '',
}

export const defaultCurveServerResponse: CurveServerResponseType = {
    ...defaultServerResponse,
    data: undefined,
}

export const defaultArchiveServerResponse: UserArchivesServerResponseType = {
    ...defaultServerResponse,
    data: [],
}

export const PRECISION = 3;

export const defaultUserObj: UserType = {
    id: 0,
    archives: [],
}
