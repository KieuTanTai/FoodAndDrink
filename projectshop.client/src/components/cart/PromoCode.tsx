import React from 'react';

const PromoCode: React.FC = () => (
  <div className="rounded-lg border border-gray-200 bg-white p-4" id="promo-code-container">
    <h3 className="mb-3 font-medium text-gray-900">Mã giảm giá</h3>
    <div className="flex gap-2">
      <input
        type="text"
        placeholder="Nhập mã giảm"
        id="cart-promo-code"
        className="flex-1 rounded-md border border-gray-300 px-3 py-2 text-sm focus:border-transparent focus:ring-2 focus:ring-orange-500 focus:outline-none"
      />
      <button className="rounded-md px-4 py-2 text-sm font-medium whitespace-nowrap transition-colors hover:bg-gray-200">
        Xác nhận
      </button>
    </div>
  </div>
);

export default PromoCode;