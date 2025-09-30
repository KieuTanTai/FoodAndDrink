import { createContext } from "react";
import type { SaleEventItemProps } from "../../models/props/sale_events/sale-event-item-props";

export type AbsoluteArrowNavigationContextType = {
    current: number;
    saleEventItems?: SaleEventItemProps;
    setSaleEventItems: (items: SaleEventItemProps[]) => void;
    handleSlideNext: (idx: number) => void;
    handleSlidePrev: (idx: number) => void;
    setCurrent: (current: number) => void;
}

export const defaultAbsoluteArrowNavigationContext: AbsoluteArrowNavigationContextType = {
    current: 0,
    handleSlideNext: () => { },
    handleSlidePrev: () => { },
    setSaleEventItems: () => { },
    setCurrent: () => { }
}

export const AbsoluteArrowNavigationContext = createContext<AbsoluteArrowNavigationContextType>(defaultAbsoluteArrowNavigationContext);