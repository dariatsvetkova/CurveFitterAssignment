import { ArchiveDeleteParamsType, ArchivedCurveType } from "../models/ArchiveTypes";

export async function deleteArchive(
    params: ArchiveDeleteParamsType,
): Promise<ArchivedCurveType['Id']> {
    const fetchUrl = encodeURI(
        "https://localhost:7228/"
        + 'api/archives/delete'
        + `?userId=${params.UserId}&archiveId=${params.ArchiveId}`
    )

    console.log('fetchUrl:', fetchUrl)

    return fetch(
        fetchUrl,
        {
            method: "DELETE",
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