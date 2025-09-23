import { useEffect } from "react";

/**
 * Hook fix cứng padding-right cho body khi mở modal.
 * Dùng: gọi useFixedScrollbarCompensate(isOpen) ở component modal.
 * Chỉ cần thêm CSS sau vào global.css:
 * 
 * body.modal-open {
 *   padding-right: 15px !important;
 *   overflow: hidden;
 * }
 */
export default function useFixedScrollbarCompensate(isOpen: boolean) {
  useEffect(() => {
    if (isOpen) {
      document.body.classList.add("modal-open");
      console.log("[Modal][useFixedScrollbarCompensate] Đã thêm class modal-open cho body.");
    } else {
      document.body.classList.remove("modal-open");
      console.log("[Modal][useFixedScrollbarCompensate] Đã xóa class modal-open khỏi body.");
    }
    // Dọn dẹp khi unmount hoặc crash
    return () => {
      document.body.classList.remove("modal-open");
      console.log("[Modal][useFixedScrollbarCompensate] Cleanup: Đã xóa class modal-open khỏi body.");
    };
  }, [isOpen]);
}