import React, { useState } from "react";
import type { SaleEventItemProps } from "../../models/props/sale_events/sale-event-item-props";
import { CircleIcon } from "lucide-react";

interface Props {
  events: SaleEventItemProps[];
}

const SaleEventMainSlider: React.FC<Props> = ({ events }) => {
  const [current, setCurrent] = useState(0);

  const handleDotClick = (index: number) => {
    setCurrent(index);
    console.log(`[SaleEventMainSlider] Chuyển slide sang: ${index}`);
  };

  return (
    <div className="relative rounded-xl overflow-hidden shadow-lg h-[380px]">
      <img
        src={events[current]?.image}
        alt={events[current]?.title}
        className="object-cover w-full h-full"
        loading="lazy"
      />
      <div className="absolute inset-0 bg-black/20 flex flex-col justify-end p-6">
        <h3 className="text-white text-2xl font-bold">{events[current]?.title}</h3>
        <p className="text-white text-sm">{events[current]?.description}</p>
        {events[current]?.time && (
          <span className="text-yellow-300 text-xs mt-2">{events[current].time}</span>
        )}
        {/* Dot navigation */}
        <div className="flex justify-center gap-2 mt-2 absolute right-0 bottom-2 w-full">
          {events.map((_, idx) => (
            <CircleIcon key={idx} className={`w-4 h-4 rounded-full shadow-2xs
              ${current === idx ? "bg-blue-600" : "bg-gray-300"} transition`}
              onClick={() => handleDotClick(idx)} />
          ))}
        </div>
      </div>
      
    </div>
  );
};

export default SaleEventMainSlider;