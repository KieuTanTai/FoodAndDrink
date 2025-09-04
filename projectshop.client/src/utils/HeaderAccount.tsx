import { useState } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faCircleUser } from "@fortawesome/free-solid-svg-icons";
import LoginModal from "../modal/LoginModal"; 
import SignupModal from "../modal/SignupModal";

function HeaderAccount() {
  const [loginOpen, setLoginOpen] = useState(false);
  const [signupOpen, setSignupOpen] = useState(false);

  return (
    <div className="relative group pb-1 account-hover-area rounded-md ml-4 h-full">
      {/* Icon và text */}
      <div className="flex items-center pl-3 cursor-pointer">
        <FontAwesomeIcon icon={faCircleUser} size="xl" className="main-color" />
        <span className="pl-2">Tài khoản</span>
      </div>

      {/* Menu con */}
      <div className="absolute top-full right-0 mt-4 w-48 bg-white border border-gray-200 rounded-md shadow-lg hidden group-hover:block z-50">
        <div className="flex flex-col p-2">
          <button
            className="py-2 px-4 flex items-center justify-center mb-2 text-left rounded-md font-medium"
            onClick={() => setLoginOpen(true)}
          >
            Đăng nhập
          </button>
          <button className="py-2 px-4 flex items-center justify-center mb-2 text-left rounded-md font-medium"
            onClick={() => setSignupOpen(true)}>
            Đăng kí
          </button>
          <button className="py-2 px-4 items-center justify-center text-left rounded-md hidden font-medium">
            Đăng xuất
          </button>
        </div>
      </div>

      {/* Login Modal */}
      <LoginModal isOpen={loginOpen} onRequestClose={() => setLoginOpen(false)} />
      {/* Signup Modal */}
      <SignupModal isOpen={signupOpen} onRequestClose={() => setSignupOpen(false)} />
    </div>
  );
}

export default HeaderAccount;