import type { FormProps } from "react-router-dom";

export interface ForgotPasswordFormProps extends FormProps {
    canResetPassword: boolean;
    verifyCode: string;
    verifyCodeErrorMessage: string;
    handleVerifyEmail: (email: string) => boolean | Promise<boolean>;
    handleSendingVerifyCode: () => boolean | Promise<boolean>;
    handleCheckVerifyCode: () => boolean;
}