'use client';
import React, { useState } from 'react';
import CartHeader from '../components/cart/CartHeader';
import CartItemsList from '../components/cart/CartItemsList';
import CartSummary from '../components/cart/CartSummary';
import DeliveryInfo from '../components/cart/DeliveryInfo';
import PromoCode from '../components/cart/PromoCode';

export interface CartItem {
  id: string;
  name: string;
  category: 'food' | 'drink';
  price: number;
  quantity: number;
  image: string;
  description?: string;
}

const initialCartItems: CartItem[] = [
  {
    id: '1',
    name: 'Margherita Pizza',
    category: 'food',
    price: 24.99,
    quantity: 1,
    image: 'https://images.unsplash.com/photo-1574071318508-1cdbab80d002?w=100&h=100&fit=crop',
    description: 'Fresh tomato, mozzarella, basil'
  },
  {
    id: '2',
    name: 'Caesar Salad',
    category: 'food',
    price: 18.50,
    quantity: 2,
    image: 'https://images.unsplash.com/photo-1551248429-40975aa4de74?w=100&h=100&fit=crop',
    description: 'Crisp romaine, parmesan, croutons'
  },
  {
    id: '3',
    name: 'Fresh Orange Juice',
    category: 'drink',
    price: 6.99,
    quantity: 1,
    image: 'https://images.unsplash.com/photo-1621506289937-a8e4df240d0b?w=100&h=100&fit=crop',
    description: 'Freshly squeezed'
  },
  {
    id: '4',
    name: 'Iced Coffee',
    category: 'drink',
    price: 4.50,
    quantity: 3,
    image: 'https://images.unsplash.com/photo-1461023058943-07fcbe16d735?w=100&h=100&fit=crop',
    description: 'Cold brew with ice'
  }
];

const CartPage: React.FC = () => {
  const [cartItems, setCartItems] = useState<CartItem[]>(initialCartItems);

  const updateQuantity = (id: string, newQuantity: number) => {
    if (newQuantity <= 0) {
      removeItem(id);
      return;
    }
    setCartItems(items =>
      items.map(item =>
        item.id === id ? { ...item, quantity: newQuantity } : item
      )
    );
  };

  const removeItem = (id: string) => {
    setCartItems(items => items.filter(item => item.id !== id));
  };

  const getTotalPrice = () => {
    return cartItems.reduce((total, item) => total + (item.price * item.quantity), 0);
  };

  const getTotalItems = () => {
    return cartItems.reduce((total, item) => total + item.quantity, 0);
  };

  const deliveryFee = 3.99;
  const tax = getTotalPrice() * 0.08;
  const finalTotal = getTotalPrice() + deliveryFee + tax;

  return (
    <div className="mx-auto flex max-w-[75rem] flex-col items-center justify-center p-4 lg:px-0">
      <div className="w-full rounded-lg bg-white shadow-lg">
        {/* Header */}
        <CartHeader totalItems={getTotalItems()} />
        <div className="mx-auto max-w-6xl px-4 py-8">
          <div className="grid grid-cols-1 gap-8 lg:grid-cols-3">
            {/* Cart Items */}
            <div className="lg:col-span-2">
              <CartItemsList
                items={cartItems}
                onQuantityChange={updateQuantity}
                onRemove={removeItem}
              />
            </div>
            {/* Order Summary */}
            <div className="space-y-6 lg:col-span-1">
              <CartSummary
                total={getTotalPrice()}
                deliveryFee={deliveryFee}
                tax={tax}
                finalTotal={finalTotal}
                disabled={cartItems.length === 0}
              />
              <DeliveryInfo />
              <PromoCode />
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default CartPage;