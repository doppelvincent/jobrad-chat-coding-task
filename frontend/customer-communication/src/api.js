export const API_BASE_URL = "http://localhost:24050/api";
export const HUB_URL = "http://localhost:24050/chat-hub";

export async function fetchWaitingSessions() {
    const response = await fetch(`${API_BASE_URL}/sessions?status=Waiting`);

    if (!response.ok) throw new Error(`Failed to fetch sessions: ${response.status}`);

    return response.json();
}

export async function fetchSession(sessionId) {
    const response = await fetch(`${API_BASE_URL}/sessions/${sessionId}`);
    if (!response.ok) throw new Error(`Failed to fetch session: ${response.status}`);
    return response.json();
}

export async function closeSession(sessionId) {
    const response = await fetch(`${API_BASE_URL}/sessions/${sessionId}/close`, {
        method: "POST",
    });

    if (!response.ok) throw new Error(`Failed to close session: ${response.status}`);

    return response.json();
}
