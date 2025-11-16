import React from 'react';
import { ShoppingBag } from 'lucide-react';

interface CartHeaderProps {
  totalItems: number;
}

const CartHeader: React.FC<CartHeaderProps> = ({ totalItems }) => (
  <div className="border-b shadow-sm">
    <div className="mx-auto max-w-6xl px-4 py-6">
      <div className="flex items-center gap-3">
        <ShoppingBag className="h-8 w-8 text-orange-500" />
        <div className="flex flex-row items-end gap-4">
          <h1 className="text-2xl font-bold text-gray-900">Your Cart</h1>
          <p className="text-violet-900">{totalItems} items</p>
        </div>
      </div>
    </div>
  </div>
);

export default CartHeader;