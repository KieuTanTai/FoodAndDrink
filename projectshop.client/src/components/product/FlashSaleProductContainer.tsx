import React from "react";

interface FlashSaleContainerProps {
    id: string;
    header?: React.ReactNode;
    countdown?: React.ReactNode;
    bodyBg?: string;
    children?: React.ReactNode;
    footer?: React.ReactNode;
}

export default function FlashSaleContainer({
    id,
    header,
    countdown,
    bodyBg = "flashsale-gradient",
    children,
    footer,
}: FlashSaleContainerProps) {
    return (
        <section
            className="mx-auto flex max-w-[1200px] flex-col items-center justify-center p-4 lg:px-0"
            id={`flashsale-container-${id}`}
        >
            <div className={`p-4 shadow-lg rounded-lg border-2 border-red-400 w-full ${bodyBg}`}>
                {/* Header kiểu margin nổi bật */}
                <div
                    className="
                        flex items-center gap-4
                        bg-white rounded-lg shadow-lg
                        px-6 py-3
                        min-h-[56px]
                        border border-gray-200
                        mx-0 mb-6
                    "
                >
                    {header}
                    {countdown && <div className="ml-4">{countdown}</div>}
                </div>

                {/* Body */}
                <div>
                    <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-5 gap-4">
                        {children}
                    </div>
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