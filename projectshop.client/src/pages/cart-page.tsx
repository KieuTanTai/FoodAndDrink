import React, { useState } from 'react';
import {Trash2, ShoppingBag, Clock } from 'lucide-react';

interface CartItem {
     id: string;
     name: string;
     category: 'food' | 'drink';
     price: number;
     quantity: number;
     image: string;
     description?: string;
}

const Cart: React.FC = () => {
     const [cartItems, setCartItems] = useState<CartItem[]>([
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
     ]);

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
                    <div className="border-b shadow-sm">
                         <div className="mx-auto max-w-6xl px-4 py-6">
                              <div className="flex items-center gap-3">
                                   <ShoppingBag className="h-8 w-8 text-orange-500" />
                                   <div className="flex flex-row items-end gap-4">
                                        <h1 className="text-2xl font-bold text-gray-900">Your Cart</h1>
                                        <p className="text-violet-900">{getTotalItems()} items</p>
                                   </div>
                              </div>
                         </div>
                    </div>
                    <div className="mx-auto max-w-6xl px-4 py-8">
                         {/* Main Grid for Cart Items and Summary */}
                         <div className="grid grid-cols-1 gap-8 lg:grid-cols-3">
                              {/* Cart Items */}
                              <div className="lg:col-span-2">
                                   <div className="space-y-4">
                                        {cartItems.length === 0 ? (
                                             <div className="rounded-lg bg-white p-8 text-center">
                                                  <ShoppingBag className="mx-auto mb-4 h-16 w-16 text-gray-300" />
                                                  <h3 className="mb-2 text-lg font-medium text-gray-900">Your cart is empty</h3>
                                                  <p className="text-gray-500">Add some delicious food and drinks to get started!</p>
                                             </div>
                                        ) : (
                                             cartItems.map((item) => (
                                                  <div key={item.id} className="rounded-lg border border-gray-200 bg-white p-6 shadow-sm transition-shadow hover:shadow-md">
                                                       {/* Item Grid */}
                                                       <div className="relative grid grid-cols-[auto,1fr] items-center gap-4">
                                                            {/* Image */}
                                                            <div>
                                                                 <img
                                                                      src={item.image}
                                                                      alt={item.name}
                                                                      className="h-20 w-20 rounded-lg object-cover"
                                                                 />
                                                            </div>
                                                            {/* Details */}
                                                            <div className="grid grid-cols-1 items-center gap-y-4 sm:grid-cols-2">
                                                                 <div className="sm:pr-4">
                                                                      <div className="flex items-start justify-between gap-2 sm:justify-start">
                                                                           <h3 className="text-lg font-semibold text-gray-900">{item.name}</h3>
                                                                           <button
                                                                                onClick={() => removeItem(item.id)}
                                                                                className="text-gray-400 hover:text-red-500 transition-colors p-1 sm:hidden"
                                                                           >
                                                                                <Trash2 className="h-5 w-5" />
                                                                           </button>
                                                                      </div>
                                                                      <p className="mt-1 text-sm text-gray-600">{item.description}</p>
                                                                      <div className="mt-2 flex items-center gap-2">
                                                                           <span className={`inline-flex items-center px-2.5 py-0.5 rounded-full text-xs font-medium ${item.category === 'food'
                                                                                ? 'bg-orange-100 text-orange-800'
                                                                                : 'bg-blue-100 text-blue-800'
                                                                                }`}>
                                                                                {item.category}
                                                                           </span>
                                                                      </div>
                                                                 </div>

                                                                 <div className="flex items-center justify-between gap-4 sm:justify-end">
                                                                      {/* Quantity Controls */}
                                                                      <div className="flex items-center gap-3">
                                                                           <button
                                                                                onClick={() => updateQuantity(item.id, item.quantity - 1)}
                                                                                className="w-8 h-8 rounded-full bg-gray-100 hover:bg-gray-200 flex items-center justify-center transition-colors text-gray-700 text-xl font-bold"
                                                                           >
                                                                                <span>-</span>
                                                                           </button>
                                                                           <span className="min-w-[2rem] text-center text-lg font-medium">{item.quantity}</span>
                                                                           <button
                                                                                onClick={() => updateQuantity(item.id, item.quantity + 1)}
                                                                                className="w-8 h-8 rounded-full bg-gray-100 hover:bg-gray-200 flex items-center justify-center transition-colors text-gray-700 text-xl font-bold"
                                                                           >
                                                                                <span>+</span>
                                                                           </button>
                                                                      </div>
                                                                      {/* Price */}
                                                                      <div className="text-right">
                                                                           <div className="text-lg font-bold text-gray-900">
                                                                                ${(item.price * item.quantity).toFixed(2)}
                                                                           </div>
                                                                           <div className="text-sm text-gray-500">
                                                                                ${item.price.toFixed(2)} each
                                                                           </div>
                                                                      </div>
                                                                 </div>
                                                            </div>
                                                            <div className="absolute top-6 right-6 hidden sm:block">
                                                                 <button
                                                                      onClick={() => removeItem(item.id)}
                                                                      className="transition-colors p-1 rounded-md"
                                                                 >
                                                                      <Trash2 className="h-5 w-5" />
                                                                 </button>
                                                            </div>
                                                       </div>
                                                  </div>
                                             ))
                                        )}
                                   </div>
                              </div>
                              {/* Order Summary */}
                              <div className="space-y-6 lg:col-span-1">
                                   <div className="rounded-lg border border-gray-200 bg-white p-6 shadow-sm">
                                        <h2 className="mb-4 text-xl font-semibold text-gray-900">Hóa Đơn</h2>
                                        <div className="space-y-3">
                                             <div className="flex justify-between">
                                                  <span className="text-gray-600">Tổng tiền hàng</span>
                                                  <span className="font-medium">${getTotalPrice().toFixed(2)}</span>
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
                                             disabled={cartItems.length === 0}
                                        >
                                             Xác nhận
                                        </button>
                                   </div>
                                   {/* Delivery Info */}
                                   <div className="rounded-lg border border-green-200 bg-green-50 p-4">
                                        <div className="mb-2 flex items-center gap-2">
                                             <Clock className="h-5 w-5 text-green-600" />
                                             <span className="font-medium text-green-900">Thời gian giao hàng dự kiến</span>
                                        </div>
                                        <p className="text-sm text-green-800">25-35 phút</p>
                                   </div>
                                   {/* Promo Code */}
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
                              </div>
                         </div>
                    </div>
               </div>
          </div>
     );
};

export default Cart;