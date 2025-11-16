import { Trash2 } from 'lucide-react';
import type { CartItemProps } from '../../models/props/CartItemProps';

function CartItem({ item, onQuantityChange, onRemove }: CartItemProps) {
  return (
    <div className="rounded-lg border border-gray-200 bg-white p-6 shadow-sm transition-shadow hover:shadow-md">
      <div className="relative grid grid-cols-[auto,1fr] items-center gap-4">
        <div>
          <img src={item.image} alt={item.name} className="h-20 w-20 rounded-lg object-cover" />
        </div>
        <div className="grid grid-cols-1 items-center gap-y-4 sm:grid-cols-2">
          <div className="sm:pr-4">
            <div className="flex items-start justify-between gap-2 sm:justify-start">
              <h3 className="text-lg font-semibold text-gray-900">{item.name}</h3>
              <button
                onClick={() => onRemove(item.id)}
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
            <div className="flex items-center gap-3">
              <button
                onClick={() => onQuantityChange(item.id, item.quantity - 1)}
                className="w-8 h-8 rounded-full bg-gray-100 hover:bg-gray-200 flex items-center justify-center transition-colors text-gray-700 text-xl font-bold"
              >
                <span>-</span>
              </button>
              <span className="min-w-[2rem] text-center text-lg font-medium">{item.quantity}</span>
              <button
                onClick={() => onQuantityChange(item.id, item.quantity + 1)}
                className="w-8 h-8 rounded-full bg-gray-100 hover:bg-gray-200 flex items-center justify-center transition-colors text-gray-700 text-xl font-bold"
              >
                <span>+</span>
              </button>
            </div>
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
            onClick={() => onRemove(item.id)}
            className="transition-colors p-1 rounded-md"
          >
            <Trash2 className="h-5 w-5" />
          </button>
        </div>
      </div>
    </div>
  );
};

export default CartItem;