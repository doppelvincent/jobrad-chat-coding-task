import { ChatHubProvider } from "./state/ChatHubProvider.jsx";
import { CustomerView } from "./components/CustomerView/CustomerView.jsx";
import { AgentView } from "./components/AgentView/AgentView.jsx";
import "./App.css";

const isAdmin = window.location.pathname === "/agent";

export default function App() {
    return (
        <ChatHubProvider>
            {isAdmin ? <AgentView /> : <CustomerView />}
        </ChatHubProvider>
    );
}
