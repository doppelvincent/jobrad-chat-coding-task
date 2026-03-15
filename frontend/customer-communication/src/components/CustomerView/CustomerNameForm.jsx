import "./CustomerNameForm.css";

export function CustomerNameForm({ name, onNameChange, onStart, error }) {
    return (
        <div className="name-form">
            <div className="name-form__card">
                <h2 className="name-form__title">Customer Chat</h2>
                <input
                    className="name-form__input"
                    placeholder="Your name"
                    value={name}
                    onChange={(e) => onNameChange(e.target.value)}
                    onKeyDown={(e) => e.key === "Enter" && onStart()}
                    autoFocus
                />
                <button className="name-form__btn" onClick={onStart} disabled={!name.trim()}>
                    Start Chat
                </button>
                {error && <p className="name-form__error">{error}</p>}
            </div>
        </div>
    );
}
