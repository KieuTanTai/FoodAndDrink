import React from 'react';
import CartItem from './CartItem';
import type { CartItemProps } from '../../models/props/cart-item-props';
import { ShoppingBag } from 'lucide-react';

interface CartItemsListProps {
  items: CartItemProps['item'][];
  onQuantityChange: CartItemProps['onQuantityChange'];
  onRemove: CartItemProps['onRemove'];
}

const CartItemsList: React.FC<CartItemsListProps> = ({ items, onQuantityChange, onRemove }) => {
  if (items.length === 0) {
    return (
      <div className="rounded-lg bg-white p-8 text-center">
        <ShoppingBag className="mx-auto mb-4 h-16 w-16 text-gray-300" />
        <h3 className="mb-2 text-lg font-medium text-gray-900">Your cart is empty</h3>
        <p className="text-gray-500">Add some delicious food and drinks to get started!</p>
      </div>
    );
  }
  return (
    <div className="space-y-4">
      {items.map(item => (
        <CartItem
          key={item.id}
          item={item}
          onQuantityChange={onQuantityChange}
          onRemove={onRemove}
        />
      ))}
    </div>
  );
};

export default CartItemsList;