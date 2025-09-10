import type { AccountModel } from "../../../models/account-model";
export interface LoginModalProps {
    isOpen : boolean;
    onLoginSuccess: (account: AccountModel) => void;
    onRequestClose: () => void;
}