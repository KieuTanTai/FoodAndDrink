import type { AccountModel } from "@/AccountModel";

export default interface LoginFormProps {
  onSuccess: (account: AccountModel) => void;
  onRegisterLinkClick: () => void;
  onForgotPasswordLinkClick: () => void;
}
