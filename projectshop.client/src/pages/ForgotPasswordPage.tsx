import React, { useState } from "react";
import ForgotPasswordForm from "../components/ForgotPasswordForm";

const ForgotPasswordPage: React.FC = () => {
    const [showLogin, setShowLogin] = useState(false);

    const handleBackToLogin = () => {
        setShowLogin(true);
    };

    // Nếu muốn hiển thị login page thì render LoginPage ở đây, ví dụ:
    // if (showLogin) return <LoginPage />;

    return (
        <ForgotPasswordForm onBackToLogin={handleBackToLogin} />
    );
};

export default ForgotPasswordPage;