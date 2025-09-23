import type { AccountModel } from "../../../models/account-model";
import type { BaseAccountModalProps } from "./base-account-modal-props";
export interface LoginModalProps extends Omit<BaseAccountModalProps, 'onSuccess'> {
    onSuccess: (account: AccountModel) => void;    
}