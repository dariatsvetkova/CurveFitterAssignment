import { ArchivedCurveType } from "../models/ArchiveTypes";
import { } from "../models/CurveTypes";
import { UserType } from "../models/UserTypes";

export async function getArchives(
    userId: UserType['id'],
): Promise<ArchivedCurveType[]> {
    const fetchUrl = encodeURI(
        "https://localhost:7228/"
        + 'api/archives'
        + `?userId=${userId}`
    )

    console.log('fetchUrl:', fetchUrl)

    return fetch(
        fetchUrl,
        {
            method: "GET",
            headers: {
                "Accept": "application/json; charset=utf-8",
                "Origin": "https://localhost:5173"
            },
        }
    )
        .then((res) => {
            if (res.ok) {
                return res.json()
            } else {
                throw new Error('Server request failed')
            }
        })
    // Errors to be handled on component level
}