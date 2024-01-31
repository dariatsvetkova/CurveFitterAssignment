import { ArchivePostParamsType } from "../models/CurveTypes"
import { UserType } from "../models/UserTypes"

export async function createArchive(
    params: ArchivePostParamsType,
): Promise<UserType | null> {
    const fetchUrl = encodeURI(
        "https://localhost:7228/"
        + "api/archives/add"
    )

    return fetch(
        fetchUrl,
        {
            method: "POST",
            headers: {
                "Accept": "application/json; charset=utf-8",
                "Origin": "https://localhost:5173"
            },
            body: JSON.stringify(params)
        }
    )
        .then((res) => {
            if (res.ok) {
                return res.json()
            } else {
                throw new Error('Server request failed')
            }
        })
        .then((newUser) => {
            return newUser
        })
    // Errors to be handled on component level
}
