import type { FormProps } from "react-router-dom";

export interface ForgotPasswordFormProps extends FormProps{
    handleVerify: () => boolean;
    handleSendingVerifyCode: () => boolean;
}