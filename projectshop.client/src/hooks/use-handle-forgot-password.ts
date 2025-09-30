import { useState } from "react";
import type { ForgotPasswordFormProps } from "../models/props/account/forgot-password-form-props";
import { checkExistedByEmail } from "../api/authApi";

function UseForgotPasswordHandler(): ForgotPasswordFormProps {
    const [canResetPassword, setCanResetPassword] = useState(false);
    const [verifyCode, setVerifyCode] = useState<string | "">("");
    const [verifyCodeErrorMessage, setVerifyCodeErrorMessage] = useState<string | "">("");

    const handleVerifyEmail = async (email: string): Promise<boolean> => {
        try {
            const result = await checkExistedByEmail(email);
            if (result) {
                setCanResetPassword(true);
                return true;
            }
            return false;
        } catch (error) {
            if (error instanceof Error)
                console.error(error.message);
            return false; 
        }
    }
    
    const handleSendingVerifyCode = 
    return { canResetPassword, verifyCode, verifyCodeErrorMessage, handleVerifyEmail };
}
export default UseForgotPasswordHandler;