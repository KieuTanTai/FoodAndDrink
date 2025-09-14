import type { AccountModel } from "../../account-model";

export default interface LoginFormProps {
    onLoginSuccess: (account: AccountModel) => void;
}