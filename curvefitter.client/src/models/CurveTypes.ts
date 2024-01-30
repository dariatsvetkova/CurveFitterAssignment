export type FitsType = 'linear' | 'cubic' | 'quadratic';

export interface CurveFitType {
    name: FitsType;
    value: 1 | 2 | 3;
    minDataPoints: number;
}

export type DataPointType = {
    X: number;
    Y: number;
}

export type EquationType = number[];

export interface CurveType {
    Equation: EquationType;
    UserDataPoints: DataPointType[];
    FitDataPoints: DataPointType[];
}

export interface CurveRequestParamsType {
    userPoints: DataPointType[];
    fitType: CurveFitType['value'];
}

export interface CurveServerResponseType {
    data?: CurveType;
    loading: boolean;
    error: boolean;
    message?: string;
}

export interface CurveToArchiveType extends CurveType {
    userId: number;
    name: string;
}

export interface ArchivedCurveType extends CurveType {
    id: number;
    timestamp: string;
}
