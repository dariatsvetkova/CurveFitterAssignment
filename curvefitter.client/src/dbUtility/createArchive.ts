import { ArchivePostParamsType, ArchivedCurveType } from "../models/ArchiveTypes"

const paramsOrder: Array<keyof ArchivePostParamsType> = [
    "Name",
    "UserId",
    "FitType",
    "Equation",
    "UserDataPoints",
    "FitDataPoints",
]

function convertBody(params: ArchivePostParamsType): string {
    const body: Partial<ArchivePostParamsType>= {};

    paramsOrder.forEach((key) => {
        if (typeof params[key] !== 'undefined') {
            Object.assign(body, { [key]: params[key] })
        }
    })

    return JSON.stringify(body);
}

export async function createArchive(
    params: ArchivePostParamsType,
): Promise<ArchivedCurveType | null> {
    const fetchUrl = encodeURI(
        "https://localhost:7228/"
        + "api/archives/add"
    )

    // Note: because we are serializing JSON into a string,
    // the parameters must appear in the same order as in the Archive model on the server
    // to be deserialized correctly
    const requestBody = convertBody(params)

     return fetch(
         fetchUrl,
         {
             method: "POST",
             headers: {
                 "Content-Type": "application/json; charset=utf-8",
                 "Accept": "application/json; charset=utf-8",
                 "Origin": "https://localhost:5173"
             },
             body: requestBody
         }
     )
         .then(async (res) => {
             if (res.ok) {
                 return res.json()
             } else {
                 console.error(await res.json())
                 throw new Error('Server request failed')
             }
         })
         .then((newArchive) => {
             return newArchive
         })
    // Errors to be handled on component level
}
