import { CurveFitType, CurveType } from "./CurveTypes";

export interface ArchivePostParamsType extends CurveType {
    Name: string;
    UserId: number;
    FitType: CurveFitType['value'];
}

export interface ArchivedCurveType extends CurveType {
    Id: number;
    Timestamp: string;
}
