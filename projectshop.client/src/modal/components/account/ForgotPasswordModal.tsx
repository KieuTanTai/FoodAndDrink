import Modal from "react-modal";
import type { BaseAccountModalProps } from "../../props/account-props/BaseAccountModalProps";
import ForgotPasswordForm from "../../../components/account/ForgotPasswordForm";

function ForgotPasswordModal({ isOpen, onRequestClose, onSuccess, dictLinksClick }: BaseAccountModalProps) {
    return (
        <Modal
            isOpen={isOpen}
            onRequestClose={onRequestClose}
            contentLabel="Forgot Password Form"
            overlayClassName="fixed inset-0 bg-black/50 flex items-center justify-center z-[1000]"
            className="relative bg-white rounded-xl min-w-[350px] shadow-lg flex flex-col"
            bodyOpenClassName="overflow-hidden"
        >
            <button
                className="absolute top-0 right-0 w-8 h-8 flex items-center justify-center rounded-full text-red-500 text-lg transition bg-transparent! p-1!"
                onClick={onRequestClose}
                aria-label="close"
            >
                X
            </button>
            <ForgotPasswordForm
                onForgotPasswordSuccess={onSuccess}
                onBackToLogin={dictLinksClick ? dictLinksClick.login : () => { }} />
        </Modal>
    );
}

export default ForgotPasswordModal;