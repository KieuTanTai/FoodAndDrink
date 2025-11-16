import React from "react";

interface ProductContainerProps {
     id: string;
     header?: React.ReactNode;
     bodyBg?: string;
     children?: React.ReactNode;
     footer?: React.ReactNode;
}

export default function ProductContainer({
     id,
     header,
     bodyBg = "bg-white",
     children,
     footer,
}: ProductContainerProps) {
     return (
          <section
               className="mx-auto flex max-w-[1200px] flex-col items-center justify-center p-4 lg:px-0"
               id={`container-${id}`}
          >
               <div className={`p-4 shadow-md rounded-lg w-full ${bodyBg}`}>
                    {/* Header */}
                    <div className="mb-6 border-b pb-2 flex items-center min-h-[48px]">
                         {header}
                    </div>
                    {/* Body: grid responsive, max 5 cột, full 1200px */}
                    <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-5 gap-4">
                         {children}
                    </div>
                    {/* Footer */}
                    {footer && (
                         <div className="flex justify-center items-center font-bold capitalize mt-6">
                              {footer}
                         </div>
                    )}
               </div>
          </section>
     );
}