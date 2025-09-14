import React from 'react';
import { Clock } from 'lucide-react';

const DeliveryInfo: React.FC = () => (
  <div className="rounded-lg border border-green-200 bg-green-50 p-4">
    <div className="mb-2 flex items-center gap-2">
      <Clock className="h-5 w-5 text-green-600" />
      <span className="font-medium text-green-900">Thời gian giao hàng dự kiến</span>
    </div>
    <p className="text-sm text-green-800">25-35 phút</p>
  </div>
);

export default DeliveryInfo;