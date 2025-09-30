import SaleEventMainSlider from "./SaleEventMainSlider";
import SaleEventSideSlider from "./SaleEventSideSlider";
import type { SaleEventItemsProps } from "../../models/props/sale_events/sale-event-items-props";


function SaleEventBlock({ mainSaleEvents = [], sideSaleEvents = [] }: SaleEventItemsProps) {
  const [isHaveMainEvent, isHaveSideEvent] = [mainSaleEvents.length > 0, sideSaleEvents.length > 0];
  if (!isHaveMainEvent) {
    return null; // Không hiển thị gì nếu không có sự kiện chính
  }

  if (!isHaveSideEvent) {
    // Chỉ có sự kiện chính, hiển thị toàn bộ chiều rộng
    return (
      <section className="w-full py-4">
        <div className="mx-auto max-w-[1200px]">
          <SaleEventMainSlider events={mainSaleEvents} />
        </div>
      </section>
    );
  }

  return (
    <section className="w-full py-4">
      <div className="mx-auto max-w-[1200px] grid grid-cols-1 lg:grid-cols-3 gap-4">
        {/* Ảnh lớn slider bên trái */}
        <div className="lg:col-span-2">
          <SaleEventMainSlider events={mainSaleEvents} />
        </div>
        {/* Hai ảnh nhỏ slider bên phải */}
        <div className="lg:col-span-1 flex flex-col gap-4">
          <SaleEventSideSlider sideSaleEvents={sideSaleEvents.slice(0, 2)} />
        </div>
      </div>
    </section>
  );
};

export default SaleEventBlock;