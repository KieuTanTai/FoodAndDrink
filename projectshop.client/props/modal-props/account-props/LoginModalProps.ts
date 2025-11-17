import type { AccountModel } from "@/models/AccountModel";
import { BaseAccountModalProps } from "./BaseAccountModalProps";
export interface LoginModalProps
  extends Omit<BaseAccountModalProps, "onSuccess"> {
  onSuccess: (account: AccountModel) => void;
}
