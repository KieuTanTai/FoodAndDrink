import type { SaleEventBlockProps } from "../../ui-props/sale-events/SaleEventBlockProps";


function SaleEventBlock({ mainBlock, sideBlocks }: SaleEventBlockProps) {
  sideBlocks = Array.isArray(sideBlocks) ? sideBlocks : [sideBlocks]; 
  return (
    <section className="w-full py-4">
      <div className="mx-auto max-w-[1200px] grid grid-cols-1 lg:grid-cols-3 gap-4">
        {/* Ảnh lớn slider bên trái */}
        <div className="lg:col-span-2">
          {mainBlock}
        </div>
        {/* Các side event, render số lượng bất định */}
        <div className="lg:col-span-1 flex flex-col align-middle gap-5">
          {sideBlocks.map((block, idx) => (
            <div key={idx}>{block}</div>
          ))}
        </div>
      </div>
    </section>
  );
}

export default SaleEventBlock;