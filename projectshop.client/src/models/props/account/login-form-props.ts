import type { AccountModel } from "../../account-model";

export default interface LoginFormProps {
    onSuccess: (account: AccountModel) => void;
    onRegisterLinkClick: () => void;
    onForgotPasswordLinkClick: () => void;
}