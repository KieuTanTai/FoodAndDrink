import type { SaleEventItemsProps } from "../../models/props/sale_events/sale-event-items-props";
import AbsoluteArrowNavigationModal from "../../modal/components/arrowNavigation/AbsoluteArrowNavigationModal";
import { useContext } from "react";
import { AbsoluteArrowNavigationContext } from "../../contexts/absoluteArrow/navigationContext";
import DotNavigationModal from "../../modal/components/dots/DotNavigationModal";
import { AnimatePresence } from "framer-motion";
import SaleEventSlideItem from "./SaleEventSlideItem";

function SaleEventSideSlider({ saleEventItems, timeInterval }: SaleEventItemsProps & { timeInterval?: number }) {
  const context = useContext(AbsoluteArrowNavigationContext);
  if (!context) {
    return null;
  }
  const { current, setCurrent, handleSlideNext, handleSlidePrev, setTimeInterval } = context;

  if (timeInterval && timeInterval > 0) {
    setTimeInterval(timeInterval, true);
  }

  if (!saleEventItems || saleEventItems.length === 0) {
    return <div className="text-center p-4">Không có sự kiện nào để hiển thị.</div>;
  }

  const event = saleEventItems[current];

  return (
    <div className="flex flex-col gap-2">
      <div className="relative rounded-xl overflow-hidden shadow-lg h-[180px]">
        <AnimatePresence mode="wait">
          <SaleEventSlideItem
            key={current}
            image={event?.image}
            title={event?.title}
            time={event?.time}
          />
        </AnimatePresence>
        <AbsoluteArrowNavigationModal
          onNext={() => handleSlideNext(current)}
          onPrev={() => handleSlidePrev(current)}
        />
        <div className="flex justify-center gap-2 mt-2 absolute right-0 bottom-2 w-full">
          <DotNavigationModal
            current={current}
            length={saleEventItems.length}
            setCurrent={setCurrent}
          />
        </div>
      </div>
    </div>
  );
}

export default SaleEventSideSlider;