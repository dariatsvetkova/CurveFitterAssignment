export type FitsType = 'linear' | 'cubic' | 'quadratic';

export interface CurveFitType {
    name: FitsType;
    minDataPoints: number;
}

export type DataPointType = {
    x: number;
    y: number;
}

export type EquationType = number[];

export interface CurveType {
    Equation: EquationType;
    UserDataPoints: DataPointType[];
    FitDataPoints: DataPointType[];
}

export interface ServerResponseType {
    data?: CurveType;
    loading: boolean;
    error: boolean;
}
