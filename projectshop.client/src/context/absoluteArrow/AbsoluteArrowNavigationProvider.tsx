import { useCallback, useState, type ReactNode } from "react";
import type { SaleEventItemsProps } from "../../models/props/sale_events/sale-event-items-props";
import type { SaleEventItemProps } from "../../models/props/sale_events/sale-event-item-props";

function CheckSideEvents(value: SaleEventItemsProps) {
    const isHaveSideEvent = value.isHaveSideEvent ?? false;

    if (isHaveSideEvent) {
        if (value.sideSaleEvents && value.sideSaleEvents.length >= 0)
            return true;
        return false;
    }
    return isHaveSideEvent;
}

export const AbsoluteArrowNavigationProvider = ({ children, value }: { children: ReactNode, value: SaleEventItemsProps }) => {
    const [current, setCurrent] = useState(0);

    const handleSlideNext = useCallback((idx: number, isSideEvent: boolean) => {
        const mainEvents = value.mainSaleEvents ?? [];
        if (mainEvents.length === 0)
            return;
        if (CheckSideEvents(value) && !isSideEvent)
            return;
        let nextIndex = 0;
        if (!isSideEvent) {
            const length = mainEvents.length;
            nextIndex = (idx + 1 + length) % length;
            if (nextIndex > length)
                nextIndex = 0;
            setCurrent(nextIndex);
        }
        else 
        {
            const sideEvents = 
        }

    }, []);

    const handleSlidePrev = useCallback((idx: number, isSideEvent: boolean) => {
        const events = value.sideSaleEvents ?? [];
        if (events.length === 0) return;
        const prevIndex = (idx - 1 + events.length) % events.length;
        setCurrent(prevIndex);
    }, [value.sideSaleEvents]);
}
