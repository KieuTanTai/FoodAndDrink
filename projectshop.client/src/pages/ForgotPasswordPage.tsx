import React, { useState } from "react";
import LoginPage from "./LoginPage";
import ForgotPasswordForm from "../components/account/ForgotPasswordForm";

const ForgotPasswordPage: React.FC = () => {
    const [showLogin, setShowLogin] = useState(false);

    const handleBackToLogin = () => {
        setShowLogin(true);
    };

    // Nếu muốn hiển thị login page thì render LoginPage ở đây, ví dụ:
    if (showLogin) return <LoginPage />;

    return (
        <ForgotPasswordForm onBackToLogin={handleBackToLogin} />
    );
};

export default ForgotPasswordPage;