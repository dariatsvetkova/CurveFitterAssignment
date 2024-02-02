import React from 'react';
import { UserProvider } from '../store/userContext';

interface LayoutProps {
    children: React.ReactNode;
}

export default function Layout(props: LayoutProps) {
    return (
        <UserProvider>
            <div>
                {props.children}
            </div>
        </UserProvider>
    )
}
