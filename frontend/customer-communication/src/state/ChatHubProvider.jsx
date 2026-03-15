import {useEffect, useRef, useState} from "react";
import {HubConnectionBuilder, HubConnectionState} from "@microsoft/signalr";
import {ChatHubContext} from "./ChatHubContext.js";
import {createSession, fetchChatHistory, joinChat as joinChatApi, HUB_URL} from "../api.js";

export function ChatHubProvider({ children }) {
    const connectionRef = useRef(null);
    const [session, setSession] = useState(null);

    useEffect(() => {
        const connection = new HubConnectionBuilder()
            .withUrl(HUB_URL)
            .withAutomaticReconnect()
            .build();

        connectionRef.current = connection;

        connection.on("ReceiveMessage", (message) => {
            setSession((prev) => ({
                ...prev,
                messages: [...(prev?.messages ?? []), message],
            }));
        });

        connection.on("AgentJoin", (session) => setSession(session));

        connection.start().catch(console.error);

        return () => {
            connection.stop().catch(() => {});
        };
    }, []);

    async function getConnectionId() {
        const connection = connectionRef.current;
        if (!connection) return null;

        if (connection.state === HubConnectionState.Disconnected) {
            await connection.start();
        }

        while (connection.state !== HubConnectionState.Connected) {
            await new Promise((resolve) => setTimeout(resolve, 50));
        }

        return connection.connectionId;
    }

    async function startChat(userName, isAgent) {
        const connectionId = await getConnectionId();
        if (!connectionId) return;

        const newSession = await createSession(userName.trim(), isAgent, connectionId);
        setSession(newSession);
    }

    async function joinChat(sessionId, name, isAgent) {
        const connectionId = await getConnectionId();

        if (!connectionId) return;

        const updatedSession = await joinChatApi(sessionId, name, isAgent, connectionId);
        const history = await fetchChatHistory(updatedSession.id);

        setSession({ ...updatedSession, messages: history });
    }

    const value = {
        chat: {
            data: session,
            start: startChat,
            join: joinChat,
        },
    };

    return (
        <ChatHubContext.Provider value={value}>
            {children}
        </ChatHubContext.Provider>
    );
}
