import React, { useState, type FormEvent } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faUserCircle, faEnvelope, faArrowLeft } from "@fortawesome/free-solid-svg-icons";

interface ForgotPasswordFormProps {
    onBackToLogin: () => void;
}

// Giả lập API kiểm tra username có gắn email không
async function mockCheckUsernameHasEmail(username: string): Promise<boolean> {
    // Ví dụ: chỉ username "demo" là không có email
    await new Promise((r) => setTimeout(r, 350));
    return username.trim().toLowerCase() !== "demo";
}

const ForgotPasswordForm: React.FC<ForgotPasswordFormProps> = ({ onBackToLogin }) => {
    const [username, setUsername] = useState<string>("");
    const [email, setEmail] = useState<string>("");
    const [useRegisteredEmail, setUseRegisteredEmail] = useState<boolean>(true);
    const [hasRegisteredEmail, setHasRegisteredEmail] = useState<boolean | null>(null);
    const [checkingEmail, setCheckingEmail] = useState<boolean>(false);
    const [sent, setSent] = useState<boolean>(false);
    const [errorMsg, setErrorMsg] = useState<string>("");

    // Khi username thay đổi, kiểm tra lại email đã đăng kí (chỉ khi tick vào checkbox)
    React.useEffect(() => {
        if (useRegisteredEmail && username.trim()) {
            setCheckingEmail(true);
            mockCheckUsernameHasEmail(username.trim())
                .then((result) => {
                    setHasRegisteredEmail(result);
                    setCheckingEmail(false);
                })
                .catch(() => {
                    setHasRegisteredEmail(null);
                    setCheckingEmail(false);
                });
        } else {
            setHasRegisteredEmail(null);
        }
        // eslint-disable-next-line
    }, [username, useRegisteredEmail]);

    const handleSubmit = (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        if (!username || username.length < 3) {
            setErrorMsg("Vui lòng nhập tên đăng nhập hợp lệ.");
            return;
        }
        if (!useRegisteredEmail) {
            if (!email || !/\S+@\S+\.\S+/.test(email)) {
                setErrorMsg("Vui lòng nhập email hợp lệ.");
                return;
            }
        }
        setErrorMsg("");
        setSent(true);
        // TODO: Gọi API gửi yêu cầu quên mật khẩu với username và email (nếu có)
    };

    // Chỉ enable email input khi tickbox KHÔNG được tích
    const emailInputDisabled = useRegisteredEmail;

    return (
        <div
            className="mx-auto flex max-w-[75rem] flex-col items-center justify-center p-4 lg:px-0"
            style={{ gridTemplateColumns: "1fr minmax(0, 400px) 1fr" }}
        >
            <div></div>
            <div className="w-full max-w-lg space-y-6 rounded-xl border border-gray-200 bg-white p-8 shadow-lg">
                <button
                    type="button"
                    onClick={onBackToLogin}
                    className="mb-4 flex items-center bg-white! text-sm text-blue-300 hover:text-blue-400"
                >
                    <FontAwesomeIcon icon={faArrowLeft} className="mr-2" />
                    Quay lại đăng nhập
                </button>
                <div className="text-center">
                    <h2 className="text-2xl font-bold text-gray-900">Quên mật khẩu?</h2>
                    <p className="mt-2 text-sm text-gray-500">
                        Điền tên đăng nhập và email để nhận hướng dẫn đặt lại mật khẩu
                    </p>
                </div>
                {sent ? (
                    <div className="text-center font-medium text-green-600">
                        Yêu cầu đặt lại mật khẩu đã được gửi!
                    </div>
                ) : (
                    <form className="space-y-4" onSubmit={handleSubmit}>
                        {/* Username */}
                        <div>
                            <label htmlFor="forgot-username" className="sr-only">
                                Username
                            </label>
                            <div className="relative">
                                <div className="pointer-events-none absolute inset-y-0 left-0 flex items-center pl-3">
                                    <FontAwesomeIcon icon={faUserCircle} className="text-gray-400" />
                                </div>
                                <input
                                    id="forgot-username"
                                    name="forgot_username"
                                    type="text"
                                    autoComplete="username"
                                    required
                                    placeholder="Nhập tên đăng nhập"
                                    value={username}
                                    onChange={(e) => {
                                        setUsername(e.target.value);
                                        setSent(false);
                                    }}
                                    className="w-full rounded-lg border border-gray-300 py-2 pl-10 pr-3 text-sm placeholder-gray-400 shadow-sm focus:outline-none"
                                />
                            </div>
                        </div>
                        {/* Email */}
                        <div>
                            <label htmlFor="forgot-email" className="sr-only">
                                Email
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
                                    placeholder="Nhập email nhận link đặt lại mật khẩu"
                                    value={email}
                                    onChange={(e) => setEmail(e.target.value)}
                                    className={`w-full rounded-lg border ${emailInputDisabled ? "bg-gray-100 border-gray-200" : "border-gray-300"} py-2 pl-10 pr-3 text-sm placeholder-gray-400 shadow-sm focus:outline-none`}
                                    disabled={emailInputDisabled}
                                    required={!emailInputDisabled}
                                />
                            </div>
                        </div>
                        {/* Checkbox: gửi đến email đã đăng ký */}
                        <div className="flex items-center">
                            <input
                                id="use-registered-email"
                                type="checkbox"
                                checked={useRegisteredEmail}
                                disabled={checkingEmail || hasRegisteredEmail === false}
                                onChange={(e) => setUseRegisteredEmail(e.target.checked)}
                                className="h-4 w-4 rounded border-gray-300 text-blue-600"
                            />
                            <label htmlFor="use-registered-email" className="ml-2 block text-sm text-gray-900 select-none">
                                Gửi link đặt lại mật khẩu tới email đã đăng ký
                            </label>
                        </div>
                        {/* Nếu không có email đăng kí, thông báo lỗi (chữ đỏ) */}
                        {useRegisteredEmail && hasRegisteredEmail === false && (
                            <div className="text-sm text-red-600">
                                Tài khoản này chưa đăng ký email. Vui lòng bỏ chọn để nhập email nhận link đặt lại mật khẩu!
                            </div>
                        )}
                        {/* Error */}
                        {errorMsg && (
                            <div className="mt-1 h-4 text-xs text-red-500">
                                {errorMsg}
                            </div>
                        )}
                        {/* Submit */}
                        <div>
                            <button
                                type="submit"
                                className="flex w-full justify-center rounded-lg border border-transparent px-4 py-2 text-sm font-medium shadow-sm focus:outline-none"
                                disabled={checkingEmail}
                            >
                                {checkingEmail ? "Đang kiểm tra..." : "Gửi yêu cầu đặt lại mật khẩu"}
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