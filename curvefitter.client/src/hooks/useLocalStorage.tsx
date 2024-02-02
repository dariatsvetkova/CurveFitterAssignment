import React from 'react';
import { LOCAL_STORAGE_KEY } from '../utility/constants';

export default function useLocalStorage<T>(
    defaultValue: T,
    key: string,
): [
    value: T,
    setValue: (newValue: T) => void
    ] {
    const [localValue, setLocalValue] = React.useState<T>(() => {
        const localObj = window.localStorage.getItem(LOCAL_STORAGE_KEY)
        const foundValue = localObj ? JSON.parse(localObj)[key] : defaultValue
        return foundValue
    });

    const updateLocalValue = (newValue: T) => {
        const localObj = window.localStorage.getItem(LOCAL_STORAGE_KEY)

        const newLocalObj = localObj ? JSON.parse(localObj) : {}
        newLocalObj[key] = newValue

        window.localStorage.setItem(LOCAL_STORAGE_KEY, JSON.stringify(newLocalObj))
        setLocalValue(newValue)
    }

    return [localValue, updateLocalValue]
}