import React from 'react';
import { LOCAL_STORAGE_ITEMS, defaultUserObj } from '../utility/constants';
import useLocalStorage from '../hooks/useLocalStorage';
import { UserProviderType, UserType } from '../models/UserTypes';

interface UserProviderProps {
    children: React.ReactNode;
}

export const UserContext = React.createContext<UserProviderType>([
    defaultUserObj,
    () => { },
]);

export function UserProvider(props: UserProviderProps) {
    const [user, setUser] = useLocalStorage<UserType>(
        defaultUserObj,
        LOCAL_STORAGE_ITEMS.user
    );

    return (
        <UserContext.Provider value={[user, setUser]}>
            {props.children}
        </UserContext.Provider>
    )
}

