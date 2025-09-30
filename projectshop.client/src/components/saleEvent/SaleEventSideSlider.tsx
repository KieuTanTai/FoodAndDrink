import type { SaleEventItemsProps } from "../../models/props/sale_events/sale-event-items-props";
import AbsoluteArrowNavigationModal from "../../modal/arrowNavigation/AbsoluteArrowNavigationModal";
import { useContext } from "react";
import { AbsoluteArrowNavigationContext } from "../../context/absoluteArrow/absoluteArrowNavigationContext";

function SaleEventSideSlider({ events }: SaleEventItemsProps) {

  const [current, setCurrent] = useState(0);

  const event = events[current];
  console.log(`[Slider] Hiển thị sự kiện: ${event.title}, index: ${current}`);

  return (
    <div className="flex flex-col gap-2 h-[200px]">
      <div className="relative rounded-xl overflow-hidden shadow-lg h-[180px]">
        <img
          src={event.image}
          alt={event.title}
          className="object-cover w-full h-full"
          loading="lazy"
        />
        <div className="absolute inset-0 bg-black/30 flex flex-col justify-start p-4">
          <h4 className="text-white text-lg font-bold">{event.title}</h4>
          {event.time && (
            <span className="text-yellow-300 text-xs mt-1">{event.time}</span>
          )}
          <AbsoluteArrowNavigationModal
            onNext={() => handleNextNavigation(current)}
            onPrev={() => handlePrevNavigation(current)}
          />
        </div>
      </div>
    </div>
  );
}

export default SaleEventSideSlider;