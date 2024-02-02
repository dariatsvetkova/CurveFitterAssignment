import { ArchivedCurveType } from "./ArchiveTypes"

export type UserType = {
    id: number,
    archives?: ArchivedCurveType[]
}

export type UserProviderType = [
    user: UserType,
    setUser: (newValue: UserType) => void
]
