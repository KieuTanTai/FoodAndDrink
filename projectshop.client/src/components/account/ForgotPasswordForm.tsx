import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faEnvelope, faArrowLeft, faKey, faLock } from "@fortawesome/free-solid-svg-icons";
import UseForm from "../../hooks/use-auth";
import { forgotPassword } from "../../api/authApi";
import { useMessageModalProvider } from "../../hooks/use-message-modal-context";
import type { UIForgotPasswordData } from "../../ui-props/accounts/forgot-password";
import type { JsonLogEntry } from "../../value-objects/json-log-entry";
import { useState } from "react";

// TODO: fix after build product funcs

function ForgotPasswordForm({ onForgotPasswordSuccess, onBackToLogin }: { onForgotPasswordSuccess: () => void, onBackToLogin: () => void }) {
    const { showMessage } = useMessageModalProvider();
    const [sent, setSent] = useState(false);
    const [sendingCode, setSendingCode] = useState(false);
    const [codeSent, setCodeSent] = useState(false);
    const [verifying, setVerifying] = useState(false);
    const [canResetPassword, setCanResetPassword] = useState(false);
    const [verifyErrorMsg, setVerifyErrorMsg] = useState("");
    const [resetErrorMsg, setResetErrorMsg] = useState("");

    const { formData, isSubmitting, userNameErrorMessage, passwordErrorMessage, handleChange, handleSubmit, handleCopy }
        = UseForm(
            { email: "", verifyCode: '', newPassword: "", confirmNewPassword: "" },
            async (data: UIForgotPasswordData): Promise<JsonLogEntry> => {
                try {
                    const result = await forgotPassword(data);
                    if (!(result instanceof Array) && result.errorName === "" && result.errorMessage === "") {
                        onForgotPasswordSuccess();
                        showMessage("Đăng ký thành công! Vui lòng đăng nhập.", "success");
                        return result;
                    }
                    else if (result instanceof Array)
                        showMessage(result[0].message ?? "lỗi khi đăng kí, kiểm tra lại thông tin ", "error");
                    else
                        showMessage("lỗi khi đăng kí, vui lòng thử lại!", "error");
                    return {} as JsonLogEntry;
                } catch (error) {
                    if (error instanceof Error)
                        throw new Error(error.message);
                    else
                        throw new Error('An unknown error occurred during submission.');
                }
            }
        )


    return (
        <div className="mx-auto flex max-w-[75rem] flex-col items-center justify-center lg:px-0"
            style={{ gridTemplateColumns: "1fr minmax(0, 400px) 1fr" }}>
            <div></div>
            <div className="w-full max-w-lg space-y-6 rounded-xl bg-white p-8 shadow-lg">
                <button
                    type="button"
                    onClick={onBackToLogin}
                    className="main-color flex items-center bg-transparent! text-sm! text-blue-400! hover:text-blue-500! absolute top-0 left-0"
                >
                    <FontAwesomeIcon icon={faArrowLeft} className="mr-2" />
                    Quay lại đăng nhập
                </button>
                <div className="text-center mt-2">
                    <h2 className="text-2xl font-bold text-gray-900">Quên mật khẩu?</h2>
                    <p className="mt-2 text-sm text-gray-500">
                        Nhập email đã đăng ký để nhận mã xác thực và đặt lại mật khẩu
                    </p>
                </div>
                {sent ? (
                    <div className="text-center font-medium text-green-600">
                        Đổi mật khẩu thành công! Kiểm tra email để đăng nhập lại.
                    </div>
                ) : (
                    <form className="space-y-4" onSubmit={handleSubmit}>
                        {/* Email + Xác nhận email */}
                        <div>
                            <label htmlFor="forgot-email" className="sr-only">
                                Email đã đăng ký
                            </label>
                            <div className="relative flex flex-row items-center gap-2">
                                <div className="flex-1">
                                    <div className="pointer-events-none absolute inset-y-0 left-0 flex items-center pl-3">
                                        <FontAwesomeIcon icon={faEnvelope} className="text-gray-400" />
                                    </div>
                                    <input
                                        id="forgot-email"
                                        name="forgot_email"
                                        type="email"
                                        autoComplete="email"
                                        required
                                        placeholder="Nhập email đã đăng ký"
                                        value={formData.email}
                                        onChange={(e) => handleChange(e, true)}
                                        className="w-full rounded-lg border border-gray-300 py-2 pl-10 pr-3 text-sm placeholder-gray-400 shadow-sm focus:outline-none"
                                    />
                                </div>
                                <button
                                    type="button"
                                    className="rounded-lg text-sm font-medium shadow-sm px-4 py-2"
                                    onClick={handleSubmit}
                                >
                                    Xác nhận
                                </button>
                            </div>
                            {/* Error email */}
                            <div className="mt-1 h-4 text-xs text-red-500" id="forgot-email-error-msg">
                                {/* Placeholder cho lỗi email */}
                                Email error placeholder
                            </div>
                        </div>
                        {/* Mã xác thực + gửi mã */}
                        <div className="flex flex-row items-center gap-2 m-0 relative">
                            <div className="flex-1">
                                <label htmlFor="verify-code" className="sr-only">
                                    Mã xác thực
                                </label>
                                <div className="pointer-events-none absolute inset-y-0 left-0 flex items-center pl-3">
                                    <FontAwesomeIcon icon={faKey} className="text-gray-400" />
                                </div>
                                <input
                                    id="verify-code"
                                    name="verify_code"
                                    type="text"
                                    placeholder="Mã xác thực"
                                    value={formData.verifyCode}
                                    onChange={(e) => handleChange(e)}
                                    className="w-full rounded-lg border border-gray-300 py-2 pl-10 pr-3 text-sm placeholder-gray-400 shadow-sm focus:outline-none"
                                    disabled={!codeSent || canResetPassword}
                                    required
                                />
                            </div>
                            <button
                                type="button"
                                onClick={() => { }}
                                disabled={!formData.email || !/\S+@\S+\.\S+/.test(formData.email) || sendingCode || codeSent}
                                className="rounded-lg text-sm font-medium shadow-sm disabled:cursor-not-allowed disabled:opacity-50 px-4 py-2 bg-blue-600 text-white"
                            >
                                {sendingCode ? "Đang gửi..." : codeSent ? "Đã gửi" : "Gửi mã"}
                            </button>
                        </div>
                        {/* Error verify code */}
                        <div className="mt-1 h-4 text-xs text-red-500" id="verify-code-error-msg">
                            {/* Placeholder cho lỗi mã xác thực */}
                            Verify code error placeholder
                        </div>
                        {/* Xác thực mã */}
                        {codeSent && !canResetPassword && (
                            <div className="flex items-center gap-2">
                                <button
                                    type="button"
                                    onClick={() => { }}
                                    disabled={!formData.verifyCode || verifying}
                                    className="bg-green-500 hover:bg-green-600"
                                    style={{ color: "white", minWidth: 120 }}
                                >
                                    {verifying ? "Đang xác thực..." : "Xác thực mã"}
                                </button>
                                {/* Error xác thực mã */}
                                <div className="text-xs text-red-500" id="verify-error-msg">
                                    Verify error placeholder
                                </div>
                            </div>
                        )}
                        {/* Đặt lại mật khẩu */}
                        <div className="space-y-2">
                            <div className="relative">
                                <div className="pointer-events-none absolute inset-y-0 flex items-center pl-3">
                                    <FontAwesomeIcon icon={faLock} className="text-gray-400" />
                                </div>
                                <input
                                    id="new-password"
                                    name="new_password"
                                    type="password"
                                    placeholder="Mật khẩu mới"
                                    value={formData.newPassword}
                                    onChange={(e) => handleChange(e, false, true)}
                                    disabled={!canResetPassword}
                                    className={`w-full rounded-lg border border-gray-300 py-2 pl-10 pr-3 text-sm placeholder-gray-400 shadow-sm focus:outline-none
                                ${!canResetPassword ? "bg-gray-100 text-gray-400 cursor-not-allowed" : ""}`}
                                    required={canResetPassword}
                                />
                            </div>
                            <div className="relative">
                                <div className="pointer-events-none absolute inset-y-0 left-0 flex items-center pl-3">
                                    <FontAwesomeIcon icon={faLock} className="text-gray-400" />
                                </div>
                                <input
                                    id="confirm-password"
                                    name="confirm_password"
                                    type="password"
                                    placeholder="Xác nhận mật khẩu mới"
                                    value={formData.confirmNewPassword}
                                    onChange={(e) => handleChange(e)}
                                    disabled={!canResetPassword}
                                    className={`w-full rounded-lg border border-gray-300 py-2 pl-10 pr-3 text-sm placeholder-gray-400 shadow-sm focus:outline-none
                                ${!canResetPassword ? "bg-gray-100 text-gray-400 cursor-not-allowed" : ""}`}
                                    required={canResetPassword}
                                />
                            </div>
                            {/* Error reset password */}
                            <div className="text-xs text-red-500" id="reset-password-error-msg">
                                Reset password error placeholder
                            </div>
                        </div>
                        {/* Submit */}
                        <div>
                            <button
                                type="submit"
                                disabled={!canResetPassword}
                                style={{
                                    width: "100%",
                                    borderRadius: 8,
                                    backgroundColor: "var(--main-color)",
                                    color: !canResetPassword ? "#a3a3a3" : "#fff",
                                    border: "1px solid transparent",
                                    padding: "0.6em 1.2em",
                                    fontSize: "1em",
                                    fontWeight: 500,
                                    fontFamily: "inherit",
                                    cursor: !canResetPassword ? "not-allowed" : "pointer",
                                    background: !canResetPassword ? "#e5e7eb" : "var(--main-color)",
                                    transition: "border-color 0.25s"
                                }}
                                onClick={handleSubmit}
                            >
                                Đặt lại mật khẩu
                            </button>
                        </div>
                    </form>
                )}
            </div>
            <div></div>
        </div>
    );
};

export default ForgotPasswordForm;