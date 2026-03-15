export const API_BASE_URL = "http://localhost:24050/api";
export const HUB_URL = "http://localhost:24050/chat-hub";

export async function createSession(name, isAgent = false, connectionId) {
    const res = await fetch(`${API_BASE_URL}/chatsession/create`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ name, isAgent, connectionId }),
    });

    if (!res.ok) throw new Error(`Failed to create session: ${res.status}`);

    return res.json();
}

export async function fetchWaitingSessions() {
    const res = await fetch(`${API_BASE_URL}/chatsession`);

    if (!res.ok) throw new Error(`Failed to fetch sessions: ${res.status}`);

    return res.json();
}

export async function joinChat(sessionId, name, isAgent = false, connectionId) {
    const res = await fetch(`${API_BASE_URL}/chatsession/${sessionId}/join`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ name, isAgent, connectionId }),
    });

    if (!res.ok) throw new Error(`Failed to join session: ${res.status}`);

    return res.json();
}

export async function closeSession(sessionId) {
    const res = await fetch(`${API_BASE_URL}/chatsession/${sessionId}/close`, { method: "POST" });

    if (!res.ok) throw new Error(`Failed to close session: ${res.status}`);

    return res.json();
}

export async function sendMessage(sessionId, userId, content) {
    const res = await fetch(`${API_BASE_URL}/chat/${sessionId}/send-message`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ userId, content }),
    });

    if (!res.ok) throw new Error(`Failed to send message: ${res.status}`);
}

export async function fetchChatHistory(sessionId) {
    const res = await fetch(`${API_BASE_URL}/chat/${sessionId}/chat-history`);

    if (!res.ok) throw new Error(`Failed to fetch history: ${res.status}`);

    return res.json();
}
