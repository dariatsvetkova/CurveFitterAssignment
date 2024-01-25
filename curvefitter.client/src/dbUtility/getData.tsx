import { CurveRequestParamsType, CurveType, DataPointType } from "../models/CurveTypes";

/* Convert data point array to string for server request */
function formatPoints(points: DataPointType[]): string {
    let formatted = "";
    points.forEach((point, ind) => {
        formatted += `${point.X}y${point.Y}${ind < points.length - 1 ? "," : ""}`;
    });
    return formatted;
}

function formatData(data: CurveType): CurveType {
    const formattedData = data;
    formattedData.UserDataPoints = formattedData.UserDataPoints.sort((a, b) => a.X - b.X)
    formattedData.FitDataPoints = formattedData.FitDataPoints.sort((a, b) => a.X - b.X)
    return formattedData;
}

export async function getCurveData(
    path: string,
    params: CurveRequestParamsType,
): Promise<CurveType | null> {
    const fetchUrl = encodeURI(
        "https://localhost:7228/"
        + path
        + `?userPoints=${formatPoints(params.userPoints)}&fitType=${encodeURIComponent(params.fitType)}`
    )

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
        .then((data) => {
            return formatData(data)
        })
        // Errors to be handled on component level
}