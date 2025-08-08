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
               <div className="bg-white rounded-lg shadow-lg w-full">
                    {/* Header */}
                    <div className="shadow-sm border-b">
                         <div className="max-w-6xl mx-auto px-4 py-6">
                              <div className="flex items-center gap-3">
                                   <ShoppingBag className="w-8 h-8 text-orange-500" />
                                   <div className="flex flex-row items-end gap-4">
                                        <h1 className="text-2xl font-bold text-gray-900">Your Cart</h1>
                                        <p className="text-violet-900">{getTotalItems()} items</p>
                                   </div>
                              </div>
                         </div>
                    </div>
                    <div className="max-w-6xl mx-auto px-4 py-8">
                         {/* Main Grid for Cart Items and Summary */}
                         <div className="grid grid-cols-1 lg:grid-cols-3 gap-8">
                              {/* Cart Items */}
                              <div className="lg:col-span-2">
                                   <div className="space-y-4">
                                        {cartItems.length === 0 ? (
                                             <div className="bg-white rounded-lg p-8 text-center">
                                                  <ShoppingBag className="w-16 h-16 text-gray-300 mx-auto mb-4" />
                                                  <h3 className="text-lg font-medium text-gray-900 mb-2">Your cart is empty</h3>
                                                  <p className="text-gray-500">Add some delicious food and drinks to get started!</p>
                                             </div>
                                        ) : (
                                             cartItems.map((item) => (
                                                  <div key={item.id} className="bg-white rounded-lg p-6 shadow-sm border border-gray-200 hover:shadow-md transition-shadow">
                                                       {/* Item Grid */}
                                                       <div className="grid grid-cols-[auto,1fr] gap-4 items-center relative">
                                                            {/* Image */}
                                                            <div>
                                                                 <img
                                                                      src={item.image}
                                                                      alt={item.name}
                                                                      className="w-20 h-20 rounded-lg object-cover"
                                                                 />
                                                            </div>
                                                            {/* Details */}
                                                            <div className="grid grid-cols-1 sm:grid-cols-2 gap-y-4 items-center">
                                                                 <div className="sm:pr-4">
                                                                      <div className="flex items-start justify-between sm:justify-start gap-2">
                                                                           <h3 className="font-semibold text-gray-900 text-lg">{item.name}</h3>
                                                                           <button
                                                                                onClick={() => removeItem(item.id)}
                                                                                className="text-gray-400 hover:text-red-500 transition-colors p-1 sm:hidden"
                                                                           >
                                                                                <Trash2 className="w-5 h-5" />
                                                                           </button>
                                                                      </div>
                                                                      <p className="text-sm text-gray-600 mt-1">{item.description}</p>
                                                                      <div className="flex items-center gap-2 mt-2">
                                                                           <span className={`inline-flex items-center px-2.5 py-0.5 rounded-full text-xs font-medium ${item.category === 'food'
                                                                                ? 'bg-orange-100 text-orange-800'
                                                                                : 'bg-blue-100 text-blue-800'
                                                                                }`}>
                                                                                {item.category}
                                                                           </span>
                                                                      </div>
                                                                 </div>

                                                                 <div className="flex items-center justify-between sm:justify-end gap-4">
                                                                      {/* Quantity Controls */}
                                                                      <div className="flex items-center gap-3">
                                                                           <button
                                                                                onClick={() => updateQuantity(item.id, item.quantity - 1)}
                                                                                className="w-8 h-8 rounded-full bg-gray-100 hover:bg-gray-200 flex items-center justify-center transition-colors text-gray-700 text-xl font-bold"
                                                                           >
                                                                                <span>-</span>
                                                                           </button>
                                                                           <span className="font-medium text-lg min-w-[2rem] text-center">{item.quantity}</span>
                                                                           <button
                                                                                onClick={() => updateQuantity(item.id, item.quantity + 1)}
                                                                                className="w-8 h-8 rounded-full bg-gray-100 hover:bg-gray-200 flex items-center justify-center transition-colors text-gray-700 text-xl font-bold"
                                                                           >
                                                                                <span>+</span>
                                                                           </button>
                                                                      </div>
                                                                      {/* Price */}
                                                                      <div className="text-right">
                                                                           <div className="font-bold text-lg text-gray-900">
                                                                                ${(item.price * item.quantity).toFixed(2)}
                                                                           </div>
                                                                           <div className="text-sm text-gray-500">
                                                                                ${item.price.toFixed(2)} each
                                                                           </div>
                                                                      </div>
                                                                 </div>
                                                            </div>
                                                            <div className="hidden sm:block absolute top-6 right-6">
                                                                 <button
                                                                      onClick={() => removeItem(item.id)}
                                                                      className="transition-colors p-1 rounded-md"
                                                                 >
                                                                      <Trash2 className="w-5 h-5" />
                                                                 </button>
                                                            </div>
                                                       </div>
                                                  </div>
                                             ))
                                        )}
                                   </div>
                              </div>
                              {/* Order Summary */}
                              <div className="lg:col-span-1 space-y-6">
                                   <div className="bg-white rounded-lg p-6 shadow-sm border border-gray-200">
                                        <h2 className="text-xl font-semibold text-gray-900 mb-4">Order Summary</h2>
                                        <div className="space-y-3">
                                             <div className="flex justify-between">
                                                  <span className="text-gray-600">Subtotal</span>
                                                  <span className="font-medium">${getTotalPrice().toFixed(2)}</span>
                                             </div>
                                             <div className="flex justify-between">
                                                  <span className="text-gray-600">Delivery Fee</span>
                                                  <span className="font-medium">${deliveryFee.toFixed(2)}</span>
                                             </div>
                                             <div className="flex justify-between">
                                                  <span className="text-gray-600">Tax</span>
                                                  <span className="font-medium">${tax.toFixed(2)}</span>
                                             </div>
                                             <div className="border-t pt-3">
                                                  <div className="flex justify-between">
                                                       <span className="text-lg font-semibold">Total</span>
                                                       <span className="text-lg font-bold text-orange-600">${finalTotal.toFixed(2)}</span>
                                                  </div>
                                             </div>
                                        </div>
                                        <button
                                             className="w-full bg-orange-500 hover:bg-orange-600 text-white font-semibold py-3 px-4 rounded-lg mt-6 transition-colors disabled:bg-gray-300 disabled:cursor-not-allowed"
                                             disabled={cartItems.length === 0}
                                        >
                                             Proceed to Checkout
                                        </button>
                                   </div>
                                   {/* Delivery Info */}
                                   <div className="bg-green-50 border border-green-200 rounded-lg p-4">
                                        <div className="flex items-center gap-2 mb-2">
                                             <Clock className="w-5 h-5 text-green-600" />
                                             <span className="font-medium text-green-900">Estimated Delivery</span>
                                        </div>
                                        <p className="text-green-800 text-sm">25-35 minutes</p>
                                   </div>
                                   {/* Promo Code */}
                                   <div className="bg-white rounded-lg p-4 border border-gray-200" id="promo-code-container">
                                        <h3 className="font-medium text-gray-900 mb-3">Promo Code</h3>
                                        <div className="flex gap-2">
                                             <input
                                                  type="text"
                                                  placeholder="Enter code"
                                                  id="cart-promo-code"
                                                  className="flex-1 border border-gray-300 rounded-md px-3 py-2 text-sm focus:outline-none focus:ring-2 focus:ring-orange-500 focus:border-transparent"
                                             />
                                             <button className="bg-gray-100 hover:bg-gray-200 text-gray-700 px-4 py-2 rounded-md text-sm font-medium transition-colors whitespace-nowrap">
                                                  Apply
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