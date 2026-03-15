export function AgentSessionList({ sessions, onReload, onJoin, error }) {
    return (
        <div style={{ padding: 32, maxWidth: 500 }}>
            <h2>Agent View</h2>
            <button onClick={onReload} style={{ marginBottom: 16 }}>
                Load / Reload Sessions
            </button>
            {error && <p style={{ color: "red" }}>{error}</p>}
            {sessions.length === 0 ? (
                <p style={{ color: "#888" }}>No waiting sessions.</p>
            ) : (
                <ul style={{ listStyle: "none", padding: 0 }}>
                    {sessions.map((s) => (
                        <li
                            key={s.id}
                            style={{
                                marginBottom: 8,
                                padding: "8px 12px",
                                border: "1px solid #ddd",
                                borderRadius: 4,
                                display: "flex",
                                alignItems: "center",
                                gap: 12,
                            }}
                        >
                            <span>
                                Session with <strong>{s.customer?.name ?? "unknown"}</strong>
                            </span>
                            <button onClick={() => onJoin(s)}>Join</button>
                        </li>
                    ))}
                </ul>
            )}
        </div>
    );
}
