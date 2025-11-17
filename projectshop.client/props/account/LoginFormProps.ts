import type { AccountModel } from "@/models/AccountModel";

export default interface LoginFormProps {
  onSuccess: (account: AccountModel) => void;
  onRegisterLinkClick: () => void;
  onForgotPasswordLinkClick: () => void;
}
