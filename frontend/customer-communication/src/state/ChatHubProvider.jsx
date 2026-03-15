import { useEffect, useRef, useState } from "react";
import { HubConnectionBuilder, HubConnectionState } from "@microsoft/signalr";
import { ChatHubContext } from "./ChatHubContext.js";
import { HUB_URL } from "../api.js";

export function ChatHubProvider({ children }) {
    const connectionRef = useRef(null);

    const [messages, setMessages] = useState([]);

    useEffect(() => {
        const connection = new HubConnectionBuilder()
            .withUrl(HUB_URL)
            .withAutomaticReconnect()
            .build();

        connectionRef.current = connection;

        connection.on("ReceiveMessage", (message) => {
            setMessages((prev) => [...prev, message]);
        });

        return () => {
            connection.stop().catch(() => {});
        };
    }, []);

    const contextValue = {
    };

    return (
        <ChatHubContext.Provider value={contextValue}>
            {children}
        </ChatHubContext.Provider>
    );
}
