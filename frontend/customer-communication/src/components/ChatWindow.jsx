import { useRef, useEffect } from "react";
import "./ChatWindow.css";

export function ChatWindow({ title, messages, statusMessage, input, onInputChange, onSend, error }) {
    const messagesEndRef = useRef(null);

    useEffect(() => {
        messagesEndRef.current?.scrollIntoView({ behavior: "smooth" });
    }, [messages]);

    return (
        <div className="chat-window">
            <h2 className="chat-window__title">{title}</h2>
            <div className="chat-window__messages">
                {statusMessage && <p className="chat-window__status">{statusMessage}</p>}
                {messages?.map((m, i) => (
                    <div key={i} className="chat-window__message">
                        <strong>{m.sender?.name}:</strong> {m.content}
                    </div>
                ))}
                <div ref={messagesEndRef} />
            </div>
            <div className="chat-window__input-row">
                <input
                    className="chat-window__input"
                    value={input}
                    onChange={(e) => onInputChange(e.target.value)}
                    onKeyDown={(e) => e.key === "Enter" && onSend()}
                    placeholder="Type a message…"
                    autoFocus
                />
                <button className="chat-window__send-btn" onClick={onSend} disabled={!input.trim()}>
                    Send
                </button>
            </div>
            {error && <p className="chat-window__error">{error}</p>}
        </div>
    );
}
