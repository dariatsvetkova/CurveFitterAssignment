import { CurveType } from "./CurveTypes"

export type UserType = {
    id: number,
    archives?: CurveType[]
}

export type UserProviderType = [
    user: UserType,
    setUser: (newValue: UserType) => void
]
