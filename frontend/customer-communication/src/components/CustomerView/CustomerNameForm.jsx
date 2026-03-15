export function CustomerNameForm({ name, onNameChange, onStart, error }) {
    return (
        <div style={{ padding: 32, maxWidth: 400 }}>
            <h2>Customer Chat</h2>
            <input
                placeholder="Your name"
                value={name}
                onChange={(e) => onNameChange(e.target.value)}
                onKeyDown={(e) => e.key === "Enter" && onStart()}
                style={{ width: "100%", marginBottom: 12, padding: 8, boxSizing: "border-box" }}
                autoFocus
            />
            <button onClick={onStart} disabled={!name.trim()}>
                Start Chat
            </button>
            {error && <p style={{ color: "red" }}>{error}</p>}
        </div>
    );
}
