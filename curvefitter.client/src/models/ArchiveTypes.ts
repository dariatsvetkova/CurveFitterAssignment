import { CurveFitType, CurveType } from "./CurveTypes";
import { UserType } from "./UserTypes";

export interface ArchivePostParamsType extends CurveType {
    Name: string;
    UserId: number;
    FitType: CurveFitType['value'];
}

export interface ArchivedCurveType extends ArchivePostParamsType {
    Id: number;
    Timestamp: string;
}

export interface ArchiveDeleteParamsType {
    UserId: UserType['id'];
    ArchiveId: ArchivedCurveType['Id'];
}
