import { useCallback, useEffect, useRef, useState, type ReactNode } from "react";
import { AbsoluteArrowNavigationContext } from "./navigationContext";
import type { SaleEventItemsProps } from "../../models/props/sale_events/sale-event-items-props";
import type { SaleEventItemProps } from "../../models/props/sale_events/sale-event-item-props";
/**
 * Provider quản lý trạng thái slider (arrow/dot), interval và log chi tiết.
 * - Khi click arrow/dot sẽ reset lại interval.
 * - Đảm bảo không tạo nhiều interval cùng lúc.
 * - Code dễ maintain, tách rõ hàm, log rõ ràng.
 */
export const AbsoluteArrowNavigationProvider = ({
    children,
    value,
    timeInterval = 3000
}: {
    children: ReactNode,
    value: SaleEventItemsProps,
    timeInterval?: number
}) => {
    const [saleEventItems, setSaleEventItems] = useState<SaleEventItemProps[]>(value.saleEventItems ?? []);
    const [current, setCurrent] = useState<number>(0);
    const intervalRef = useRef<NodeJS.Timeout | null>(null);

    const updateSaleEventItems = useCallback((items: SaleEventItemProps[]) => {
        setSaleEventItems(items);
        setCurrent(0);
    }, []);

    const handleSlideNext = useCallback((idx?: number) => {
        setCurrent((prev) => {
            const length = saleEventItems.length;
            if (length === 0) return 0;
            const nextIdx = typeof idx === "number" ? (idx + 1) % length : (prev + 1) % length;
            return nextIdx;
        });
    }, [saleEventItems.length]);

    const handleSlidePrev = useCallback((idx?: number) => {
        setCurrent((prev) => {
            const length = saleEventItems.length;
            if (length === 0) return 0;
            const prevIdx = typeof idx === "number"
                ? (idx - 1 + length) % length
                : (prev - 1 + length) % length;
            return prevIdx;
        });
    }, [saleEventItems.length]);

    const resetTimeInterval = useCallback(() => {
        if (intervalRef.current) clearInterval(intervalRef.current);
        if (timeInterval <= 0 || saleEventItems.length <= 1) return;
        intervalRef.current = setInterval(() => {
            setCurrent((prev) => {
                const nextIdx = (prev + 1) % saleEventItems.length;
                return nextIdx;
            });
        }, timeInterval);
    }, [timeInterval, saleEventItems.length]);

    useEffect(() => {
        resetTimeInterval();
        return () => {
            if (intervalRef.current) clearInterval(intervalRef.current);
        };
    }, [resetTimeInterval]);

    const onArrowNext = useCallback((idx: number) => {
        handleSlideNext(idx);
        resetTimeInterval();
    }, [handleSlideNext, resetTimeInterval]);

    const onArrowPrev = useCallback((idx: number) => {
        handleSlidePrev(idx);
        resetTimeInterval();
    }, [handleSlidePrev, resetTimeInterval]);

    const onDotClick = useCallback((idx: number) => {
        setCurrent(idx);
        resetTimeInterval();
        console.log(`[Provider] Dot: Nhảy tới slide ${idx}`);
    }, [resetTimeInterval]);

    return (
        <AbsoluteArrowNavigationContext.Provider value={{
            current,
            saleEventItems,
            setSaleEventItems: updateSaleEventItems,
            handleSlideNext: onArrowNext,
            handleSlidePrev: onArrowPrev,
            setCurrent: onDotClick,
            setTimeInterval: resetTimeInterval
        }}>
            {children}
        </AbsoluteArrowNavigationContext.Provider>
    );
};