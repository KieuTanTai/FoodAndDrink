import type { ReactNode } from "react";


function SaleEventBlock({ mainBlock, sideBlock }: { mainBlock: ReactNode, sideBlock: ReactNode }) {


  return (
    <section className="w-full py-4">
      <div className="mx-auto max-w-[1200px] grid grid-cols-1 lg:grid-cols-3 gap-4">
        {/* Ảnh lớn slider bên trái */}
        <div className="lg:col-span-2">
          {mainBlock}
        </div>
        {/* Hai ảnh nhỏ slider bên phải */}
        <div className="lg:col-span-1 flex flex-col gap-4">
          {sideBlock}
        </div>
      </div>
    </section>
  );
};

export default SaleEventBlock;