import React, { useState, type FormEvent } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faEnvelope, faArrowLeft, faKey, faLock } from "@fortawesome/free-solid-svg-icons";

interface ForgotPasswordFormProps {
    onBackToLogin: () => void;
}

// Giả lập API
async function mockSendVerifyCode(email: string): Promise<boolean> {
    await new Promise((r) => setTimeout(r, 600));
    return /\S+@\S+\.\S+/.test(email);
}
async function mockVerifyCode(email: string, code: string): Promise<boolean> {
    await new Promise((r) => setTimeout(r, 600));
    return code === "123456";
}
async function mockResetPassword(email: string, newPassword: string): Promise<boolean> {
    await new Promise((r) => setTimeout(r, 600));
    return newPassword.length >= 6;
}

const ForgotPasswordForm: React.FC<ForgotPasswordFormProps> = ({ onBackToLogin }) => {
    const [email, setEmail] = useState<string>("");
    const [verifyCode, setVerifyCode] = useState<string>("");
    const [codeSent, setCodeSent] = useState<boolean>(false);
    const [sendingCode, setSendingCode] = useState<boolean>(false);
    const [canResetPassword, setCanResetPassword] = useState<boolean>(false);
    const [verifying, setVerifying] = useState<boolean>(false);

    const [newPassword, setNewPassword] = useState<string>("");
    const [confirmPassword, setConfirmPassword] = useState<string>("");

    const [sent, setSent] = useState<boolean>(false);
    const [errorMsg, setErrorMsg] = useState<string>("");
    const [verifyErrorMsg, setVerifyErrorMsg] = useState<string>("");
    const [resetErrorMsg, setResetErrorMsg] = useState<string>("");

    const handleSendCode = async () => {
        setErrorMsg("");
        if (!email || !/\S+@\S+\.\S+/.test(email)) {
            setErrorMsg("Vui lòng nhập email hợp lệ.");
            return;
        }
        setSendingCode(true);
        const result = await mockSendVerifyCode(email.trim());
        setSendingCode(false);
        if (result) {
            setCodeSent(true);
        } else {
            setErrorMsg("Không thể gửi mã xác thực. Vui lòng thử lại.");
        }
    };

    const handleVerifyCode = async () => {
        setVerifyErrorMsg("");
        setVerifying(true);
        const result = await mockVerifyCode(email.trim(), verifyCode.trim());
        setVerifying(false);
        if (result) {
            setCanResetPassword(true);
        } else {
            setVerifyErrorMsg("Mã xác thực không đúng.");
        }
    };

    const handleSubmit = async (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        setResetErrorMsg("");
        if (!canResetPassword) return;
        if (!newPassword || newPassword.length < 6) {
            setResetErrorMsg("Mật khẩu mới phải có ít nhất 6 ký tự.");
            return;
        }
        if (newPassword !== confirmPassword) {
            setResetErrorMsg("Mật khẩu xác nhận không khớp.");
            return;
        }
        const ok = await mockResetPassword(email.trim(), newPassword);
        if (ok) {
            setSent(true);
        } else {
            setResetErrorMsg("Đổi mật khẩu thất bại. Thử lại!");
        }
    };

    return (
        <div className="mx-auto flex max-w-[75rem] flex-col items-center justify-center p-4 lg:px-0"
            style={{ gridTemplateColumns: "1fr minmax(0, 400px) 1fr" }}>
            <div></div>
            <div className="w-full max-w-lg space-y-6 rounded-xl border border-gray-200 bg-white p-8 shadow-lg">
                <button
                    type="button"
                    onClick={onBackToLogin}
                    className="main-color mb-4 flex items-center bg-transparent! text-sm"
                >
                    <FontAwesomeIcon icon={faArrowLeft} className="mr-2" />
                    Quay lại đăng nhập
                </button>
                <div className="text-center">
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
                        {/* Email */}
                        <div>
                            <label htmlFor="forgot-email" className="sr-only">
                                Email đã đăng ký
                            </label>
                            <div className="relative">
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
                                    value={email}
                                    onChange={(e) => {
                                        setEmail(e.target.value);
                                        setSent(false);
                                        setCodeSent(false);
                                        setVerifyCode("");
                                        setCanResetPassword(false);
                                    }}
                                    className="w-full rounded-lg border border-gray-300 py-2 pl-10 pr-3 text-sm placeholder-gray-400 shadow-sm focus:outline-none"
                                    disabled={sendingCode || codeSent}
                                />
                            </div>
                        </div>
                        {/* Mã xác thực và Gửi mã xác thực kế bên nhau */}
                        <div className="flex flex-row items-center gap-2">
                            <div className="flex-1">
                                <label htmlFor="verify-code" className="sr-only">
                                    Mã xác thực
                                </label>
                                <div className="relative">
                                    <div className="pointer-events-none absolute inset-y-0 left-0 flex items-center pl-3">
                                        <FontAwesomeIcon icon={faKey} className="text-gray-400" />
                                    </div>
                                    <input
                                        id="verify-code"
                                        name="verify_code"
                                        type="text"
                                        placeholder="Mã xác thực"
                                        value={verifyCode}
                                        onChange={(e) => {
                                            setVerifyCode(e.target.value);
                                            setVerifyErrorMsg("");
                                        }}
                                        className="w-full rounded-lg border border-gray-300 py-2 pl-10 pr-3 text-sm placeholder-gray-400 shadow-sm focus:outline-none"
                                        disabled={!codeSent || canResetPassword}
                                        required
                                    />
                                </div>
                            </div>
                            <button
                                type="button"
                                onClick={handleSendCode}
                                disabled={!email || !/\S+@\S+\.\S+/.test(email) || sendingCode || codeSent}
                                    className="rounded-lg text-sm font-medium shadow-sm disabled:cursor-not-allowed disabled:opacity-50"
                            >
                                {sendingCode ? "Đang gửi..." : codeSent ? "Đã gửi" : "Gửi mã"}
                            </button>
                        </div>
                        {/* Xác thực mã */}
                        {codeSent && !canResetPassword && (
                            <div className="flex items-center gap-2">
                                <button
                                    type="button"
                                    onClick={handleVerifyCode}
                                    disabled={!verifyCode || verifying}
                                    className="bg-green-500 hover:bg-green-600"
                                    style={{ color: "white", minWidth: 120 }}
                                >
                                    {verifying ? "Đang xác thực..." : "Xác thực mã"}
                                </button>
                                {verifyErrorMsg && (
                                    <div className="text-xs text-red-500">{verifyErrorMsg}</div>
                                )}
                            </div>
                        )}
                        {/* Error */}
                        {errorMsg && (
                            <div className="mt-1 h-4 text-xs text-red-500">
                                {errorMsg}
                            </div>
                        )}
                        {/* Đặt lại mật khẩu */}
                        <div className="space-y-2">
                            <div className="relative">
                                <div className="pointer-events-none absolute inset-y-0 left-0 flex items-center pl-3">
                                    <FontAwesomeIcon icon={faLock} className="text-gray-400" />
                                </div>
                                <input
                                    id="new-password"
                                    name="new_password"
                                    type="password"
                                    placeholder="Mật khẩu mới"
                                    value={newPassword}
                                    onChange={(e) => setNewPassword(e.target.value)}
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
                                    value={confirmPassword}
                                    onChange={(e) => setConfirmPassword(e.target.value)}
                                    disabled={!canResetPassword}
                                    className={`w-full rounded-lg border border-gray-300 py-2 pl-10 pr-3 text-sm placeholder-gray-400 shadow-sm focus:outline-none
                                        ${!canResetPassword ? "bg-gray-100 text-gray-400 cursor-not-allowed" : ""}`}
                                    required={canResetPassword}
                                />
                            </div>
                            {resetErrorMsg && (
                                <div className="text-xs text-red-500">{resetErrorMsg}</div>
                            )}
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