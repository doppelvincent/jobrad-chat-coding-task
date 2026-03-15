import { useState, useEffect } from "react";
import { fetchWaitingSessions, sendMessage } from "../../api.js";
import { useChatHub } from "../../state/useChatHub.js";
import { AgentSessionList } from "./AgentSessionList.jsx";
import { ChatWindow } from "../ChatWindow.jsx";
import "./AgentView.css";

const AGENT_NAME = "Agent";

export function AgentView() {
    const [sessions, setSessions] = useState([]);
    const [input, setInput] = useState("");
    const [error, setError] = useState(null);

    const { chat } = useChatHub();

    useEffect(() => {
        loadSessions();
    }, []);

    async function loadSessions() {
        try {
            setError(null);
            setSessions(await fetchWaitingSessions());
        } catch (e) {
            setError(e.message);
        }
    }

    async function handleJoin(sess) {
        try {
            setError(null);
            await chat.join(sess.id, AGENT_NAME, true);
        } catch (e) {
            setError(e.message);
        }
    }

    async function handleSend() {
        if (!input.trim() || !chat.data) return;
        try {
            await sendMessage(chat.data.id, chat.data.agent.id, input.trim());
            setInput("");
        } catch (e) {
            setError(e.message);
        }
    }

    if (!chat.data) {
        return <AgentSessionList sessions={sessions} onReload={loadSessions} onJoin={handleJoin} error={error} />;
    }

    return (
        <ChatWindow
            title={`Chat – ${AGENT_NAME} → ${chat.data.customer?.name}`}
            messages={chat.data.messages}
            input={input}
            onInputChange={setInput}
            onSend={handleSend}
            error={error}
        />
    );
}
