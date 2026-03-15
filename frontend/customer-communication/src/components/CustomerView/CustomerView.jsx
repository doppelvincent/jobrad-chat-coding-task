import {useState} from "react";
import {sendMessage} from "../../api.js";
import {useChatHub} from "../../state/useChatHub.js";
import {CustomerNameForm} from "./CustomerNameForm.jsx";
import {ChatWindow} from "../ChatWindow.jsx";

export function CustomerView() {
    const [name, setName] = useState("");
    const [input, setInput] = useState("");
    const [error, setError] = useState(null);

    const {chat} = useChatHub();

    async function handleStart() {
        if (!name.trim()) return;

        chat.start(name.trim(), false);
    }

    async function handleSend() {
        if (!input.trim() || !chat.data) return;

        try {
            await sendMessage(chat.data.id, chat.data.customer.id, input.trim());
            setInput("");
        } catch (e) {
            setError(e.message);
        }
    }

    if (!chat.data) {
        return <CustomerNameForm name={name} onNameChange={setName} onStart={handleStart} error={error} />;
    }

    return (
        <ChatWindow
            title={`Chat – ${name}`}
            messages={chat.data.messages}
            statusMessage={chat.data.agent == null ? "Waiting for an agent to join…" : "Agent has joined the room"}
            input={input}
            onInputChange={setInput}
            onSend={handleSend}
            error={error}
        />
    );
}
