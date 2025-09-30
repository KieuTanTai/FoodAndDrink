import { createContext } from "react";
import type { SaleEventItemProps } from "../../models/props/sale_events/sale-event-item-props";

export type AbsoluteArrowNavigationContextType = {
    current: number;
    sideSaleEvent?: SaleEventItemProps;
    mainSaleEvent?: SaleEventItemProps;
    isSideEvent: boolean;
    isMainEvent: boolean;
    setSideSaleEvent: (sideSaleEvent: SaleEventItemProps[]) => void;
    setMainSaleEvent: (sideSaleEvent: SaleEventItemProps[]) => void;
    handleSlideNext: (idx: number, isSideEvent: boolean) => void;
    handleSlidePrev: (idx: number, isSideEvent: boolean) => void;
    setCurrent: (current: number) => void;
}

export const defaultAbsoluteArrowNavigationContext: AbsoluteArrowNavigationContextType = {
    current: 0,
    isSideEvent: false,
    isMainEvent: false,
    handleSlideNext: () => { },
    handleSlidePrev: () => { },
    setSideSaleEvent: () => { },
    setMainSaleEvent: () => { },
    setCurrent: () => { }
}

export const AbsoluteArrowNavigationContext = createContext<AbsoluteArrowNavigationContextType>(defaultAbsoluteArrowNavigationContext);