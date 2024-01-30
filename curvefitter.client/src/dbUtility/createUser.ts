import { UserType } from "../models/UserTypes"

export async function createUser(): Promise<UserType | null> {
    // TBD: add an auth provider and create user with email and password; validate inputs

    const fetchUrl = encodeURI(
        "https://localhost:7228/"
        + "api/users/create"
    )

    return fetch(
        fetchUrl,
        {
            method: "POST",
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
        .then((newUser) => {
            return newUser
        })
    // Errors to be handled on component level
}
