import { useCallback, useState, type ReactNode } from "react";
import type { SaleEventItemsProps } from "../../models/props/sale_events/sale-event-items-props";
import { AbsoluteArrowNavigationContext } from "./absoluteArrowNavigationContext";
import type { SaleEventItemProps } from "../../models/props/sale_events/sale-event-item-props";

export const AbsoluteArrowNavigationProvider = ({ children, value }: { children: ReactNode, value: SaleEventItemsProps }) => {
    const [current, setCurrent] = useState(0);

    const handleSlideNext = useCallback((idx: number) => {
        const events = value.saleEventItems ?? [];
        const length = events.length;
        if (length === 0) return;
        let nextIndex = (idx + 1) % length;
        if (nextIndex >= length) 
            nextIndex = 0;
        setCurrent(nextIndex);

    }, [value.saleEventItems]);

    const handleSlidePrev = useCallback((idx: number) => {
        const events = value.saleEventItems ?? [];
        if (events.length === 0) return;
        let prevIndex = (idx - 1 + events.length) % events.length;
        if (prevIndex < 0)
            prevIndex = events.length - 1;
        setCurrent(prevIndex);
    }, [value.saleEventItems]);

    const setSaleEventItems = useCallback((items: SaleEventItemProps[]) => {
        setSaleEventItems(items);
    }, []);

    return (
        <AbsoluteArrowNavigationContext.Provider value={{
            current,
            setSaleEventItems,
            handleSlideNext,
            handleSlidePrev,
            setCurrent
        }}>
            {children}
        </AbsoluteArrowNavigationContext.Provider>
    );
}
