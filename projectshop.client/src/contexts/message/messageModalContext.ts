import { createContext } from "react";
import type { MessageType } from "../../modal/props/popup-message/MessageModalProps";

export type MessageModalContextType = {
    showMessage: (
        message: string,
        type?: MessageType,
        timeout?: number
    ) => void;
};

export const defaultMessageModalContext: MessageModalContextType = {
    showMessage: () => { }
};

export const MessageModalContext = createContext<MessageModalContextType>(defaultMessageModalContext);