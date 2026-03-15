import { useRef, useEffect } from "react";

export function ChatWindow({ title, messages, statusMessage, input, onInputChange, onSend, error }) {
    const messagesEndRef = useRef(null);

    useEffect(() => {
        messagesEndRef.current?.scrollIntoView({ behavior: "smooth" });
    }, [messages]);

    return (
        <div style={{ padding: 32, maxWidth: 500 }}>
            <h2>{title}</h2>
            <div
                style={{
                    border: "1px solid #ccc",
                    height: 300,
                    overflowY: "auto",
                    padding: 8,
                    marginBottom: 12,
                    background: "#fafafa",
                }}
            >
                {statusMessage && <p style={{ color: "#888" }}>{statusMessage}</p>}
                {messages?.map((m, i) => (
                    <div key={i} style={{ marginBottom: 6 }}>
                        <strong>{m.sender?.name}:</strong> {m.content}
                    </div>
                ))}
                <div ref={messagesEndRef} />
            </div>
            <div style={{ display: "flex", gap: 8 }}>
                <input
                    value={input}
                    onChange={(e) => onInputChange(e.target.value)}
                    onKeyDown={(e) => e.key === "Enter" && onSend()}
                    style={{ flex: 1, padding: 8 }}
                    placeholder="Type a message…"
                    autoFocus
                />
                <button onClick={onSend} disabled={!input.trim()}>
                    Send
                </button>
            </div>
            {error && <p style={{ color: "red" }}>{error}</p>}
        </div>
    );
}
