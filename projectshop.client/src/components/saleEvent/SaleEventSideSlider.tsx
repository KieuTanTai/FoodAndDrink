import type { SaleEventItemsProps } from "../../models/props/sale_events/sale-event-items-props";
import AbsoluteArrowNavigationModal from "../../modal/components/arrowNavigation/AbsoluteArrowNavigationModal";
import { useContext } from "react";
import { AbsoluteArrowNavigationContext } from "../../contexts/absoluteArrow/absoluteArrowNavigationContext";
import DotNavigationModal from "../../modal/components/dots/DotNavigationModal";

function SaleEventSideSlider({ saleEventItems }: SaleEventItemsProps) {
  const context = useContext(AbsoluteArrowNavigationContext);
  if (!context) {
    return null;
  }
  const { current, setCurrent, handleSlideNext, handleSlidePrev } = context;

  const event = saleEventItems[current];
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
            onNext={() => handleSlideNext(current)}
            onPrev={() => handleSlidePrev(current)}
          />
          {/* Dot navigation */}
          <div className="flex justify-center gap-2 mt-2 absolute right-0 bottom-2 w-full">
            <DotNavigationModal
              current={current}
              length={saleEventItems.length}
              setCurrent={setCurrent}
            />
          </div>
        </div>
      </div>
    </div>
  );
}

export default SaleEventSideSlider;