import "./AgentSessionList.css";

export function AgentSessionList({ sessions, onReload, onJoin, error }) {
    return (
        <div className="session-list">
            <div className="session-list__card">
                <div className="session-list__header">
                    <h2 className="session-list__title">Agent View</h2>
                    <button className="session-list__reload-btn" onClick={onReload}>
                        Reload Sessions
                    </button>
                </div>
                {error && <p className="session-list__error">{error}</p>}
                {sessions.length === 0 ? (
                    <p className="session-list__empty">No waiting sessions.</p>
                ) : (
                    <ul className="session-list__list">
                        {sessions.map((s) => (
                            <li key={s.id} className="session-list__item">
                                <span className="session-list__item-label">
                                    Session with <strong>{s.customer?.name ?? "unknown"}</strong>
                                </span>
                                <button className="session-list__join-btn" onClick={() => onJoin(s)}>Join</button>
                            </li>
                        ))}
                    </ul>
                )}
            </div>
        </div>
    );
}
