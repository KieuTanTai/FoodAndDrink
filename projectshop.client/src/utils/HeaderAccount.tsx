import { useState, useEffect } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faCircleUser } from "@fortawesome/free-solid-svg-icons";
import LoginModal from "../modal/components/account/LoginModal";
import SignupModal from "../modal/components/account/SignupModal";
import type { AccountModel } from "../models/AccountModel";
import { getCurrentAccount, logout } from "../api/auth-api";
import { useMessageModalProvider } from "../hooks/useMessageModalContext";
import ForgotPasswordModal from "../modal/components/account/ForgotPasswordModal";
import useFixedScrollbarCompensate from "../hooks/useScrollbarCompensate";


function HeaderAccount() {
  const [currentAccount, setCurrentAccount] = useState<AccountModel | null>(null);
  const { showMessage } = useMessageModalProvider();

  // Handler mở đóng modal
  const [openModal, setOpenModal] = useState<"" | "login" | "signup" | "forgot-password">("");
  const openLogin = () => setOpenModal("login");
  const openSignup = () => setOpenModal("signup");
  const openForgotPassword = () => setOpenModal("forgot-password");
  const closeModal = () => setOpenModal("");
  useFixedScrollbarCompensate(openModal !== "");

  useEffect(() => {
    async function fetchAccount() {
      try {
        if (currentAccount) return; // Nếu đã có tài khoản, không cần gọi API nữa
        const result = await getCurrentAccount({ isGetCustomer: true });
        if (result.data && result.data.userName !== "") {
          setCurrentAccount(result.data);
          console.log("Current account:", result.data.customer?.name);
        }
        else
          setCurrentAccount(null);
      } catch (error) {
        console.error("Error fetching current account:", error);
        setCurrentAccount(null);
      }
    }
    fetchAccount();
  }, [currentAccount]);

  const handleSignup = () => {
    try {
      openLogin();
      showMessage("Đăng ký thành công! Vui lòng đăng nhập.", "success");
    } catch (error) {
      if (error instanceof Error)
        showMessage(error.message, "error");
    }
  }

  const handleLogout = async () => {
    if (currentAccount) {
      try {
        // window.location.reload();
        const result = await logout();
        setCurrentAccount(null);
        showMessage(result, "info");
      } catch (error) {
        if (error instanceof Error)
          showMessage(error.message, "error");
      }
    }
  }

  const showAccountName = () => {
    if (currentAccount) {
      if (currentAccount.customer && currentAccount.customer.name !== "")
        return currentAccount.customer.name;
      else
        return currentAccount.userName;
    }
    return "Tài khoản";
  };

  return (
    <div className="relative group pb-1 account-hover-area rounded-md h-full">
      {/* Icon và text */}
      <div className="flex items-center pl-3 cursor-pointer" id="account-menu-button">
        <FontAwesomeIcon icon={faCircleUser} size="xl" className="main-color" />
        <div className="pl-2 text-nowrap max-w-26 text-ellipsis overflow-hidden">{showAccountName()}</div>
      </div>

      {/* Menu con */}
      <div className="absolute top-full right-0 mt-4 w-48 bg-white border border-gray-200 rounded-md shadow-lg hidden group-hover:block z-50">
        <div className="flex flex-col p-2">
          <div className="flex flex-col border-b border-gray-200 mb-2 pb-2" id="account-menu-options" style={currentAccount ? { display: "none" } : {}}>
            <button
              className="py-2 px-4 flex items-center justify-center mb-2 text-left rounded-md font-medium"
              onClick={openLogin}>
              Đăng nhập
            </button>

            <button className="py-2 px-4 flex items-center justify-center mb-2 text-left rounded-md font-medium"
              onClick={openSignup}>
              Đăng kí
            </button>

            <button className="py-2 px-4 flex items-center justify-center text-left rounded-md font-medium"
              onClick={openForgotPassword}>
              Quên mật khẩu
            </button>
          </div>

          <button className="py-2 px-4 items-center justify-center text-left rounded-md hidden font-medium bg-red-300! text-black!"
            onClick={handleLogout}
            style={{ display: currentAccount ? "block" : "none" }}>
            Đăng xuất
          </button>
        </div>
      </div>

      {/* Login Modal */}
      <LoginModal
        isOpen={openModal === "login"}
        onRequestClose={closeModal}
        onSuccess={(account: AccountModel) => {
          setCurrentAccount(account);
          showMessage(`Đăng nhập thành công! ${account.customer?.name}`, "success");
          closeModal();
        }}
        dictLinksClick={{
          signup: () => openSignup(),
          forgotPassword: () => openForgotPassword()
        }}
      />
      {/* Signup Modal */}
      <SignupModal
        isOpen={openModal === "signup"}
        onRequestClose={closeModal}
        onSuccess={() => handleSignup()}
        dictLinksClick={{ login: () => openLogin() }}
      />
      {/* Forgot Password Modal */}
      <ForgotPasswordModal
        isOpen={openModal === "forgot-password"}
        onRequestClose={closeModal}
        onSuccess={() => {
          openLogin();
          showMessage("Đặt lại mật khẩu thành công! Vui lòng đăng nhập.", "success");
        }}
        dictLinksClick={{ login: () => openLogin() }}
      />
    </div>
  );
}

export default HeaderAccount;