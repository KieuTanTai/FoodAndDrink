import { useContext } from "react";
import { MessageModalContext } from "../context/MessageModalContext";

export function useMessageModalProvider() {
    const context = useContext(MessageModalContext);
    if (!context) {
        throw new Error('useMessageModal must be used within a MessageModalProvider');
    }
    return context;
}