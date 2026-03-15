import { useContext } from "react";
import { ChatHubContext } from "./ChatHubContext.js";

export function useChatHub() {
    const context = useContext(ChatHubContext);

    if (!context) {
        throw new Error("useChatHub must be used within a ChatHubProvider");
    }

    return context;
}
