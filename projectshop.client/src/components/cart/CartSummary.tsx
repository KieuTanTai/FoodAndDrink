import React from 'react';

interface CartSummaryProps {
  total: number;
  deliveryFee: number;
  tax: number;
  finalTotal: number;
  disabled: boolean;
}

const CartSummary: React.FC<CartSummaryProps> = ({
  total, deliveryFee, tax, finalTotal, disabled
}) => (
  <div className="rounded-lg border border-gray-200 bg-white p-6 shadow-sm">
    <h2 className="mb-4 text-xl font-semibold text-gray-900">Hóa Đơn</h2>
    <div className="space-y-3">
      <div className="flex justify-between">
        <span className="text-gray-600">Tổng tiền hàng</span>
        <span className="font-medium">${total.toFixed(2)}</span>
      </div>
      <div className="flex justify-between">
        <span className="text-gray-600">Phí vận chuyển</span>
        <span className="font-medium">${deliveryFee.toFixed(2)}</span>
      </div>
      <div className="flex justify-between">
        <span className="text-gray-600">Thuế</span>
        <span className="font-medium">${tax.toFixed(2)}</span>
      </div>
      <div className="border-t pt-3">
        <div className="flex justify-between">
          <span className="text-lg font-semibold">Tổng thanh toán</span>
          <span className="text-lg font-bold text-orange-600">${finalTotal.toFixed(2)}</span>
        </div>
      </div>
    </div>
    <button
      className="bpx-4 mt-6 w-full rounded-lg py-3 font-semibold transition-colors disabled:cursor-not-allowed disabled:bg-gray-300"
      disabled={disabled}
    >
      Xác nhận
    </button>
  </div>
);

export default CartSummary;