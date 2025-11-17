import { SaleEventItemProps } from "@/props/sale_events/SaleEventItemProps";
import { createContext } from "react";

export type AbsoluteArrowNavigationContextType = {
  current: number;
  saleEventItems?: SaleEventItemProps[];
  setSaleEventItems: (items: SaleEventItemProps[]) => void;
  handleSlideNext: (idx: number) => void;
  handleSlidePrev: (idx: number) => void;
  setCurrent: (current: number) => void;
  setTimeInterval: (time: number, isSlideNext?: boolean) => void;
};

export const defaultAbsoluteArrowNavigationContext: AbsoluteArrowNavigationContextType =
  {
    current: 0,
    handleSlideNext: () => {},
    handleSlidePrev: () => {},
    setSaleEventItems: () => {},
    setCurrent: () => {},
    setTimeInterval: () => {},
  };

export const AbsoluteArrowNavigationContext =
  createContext<AbsoluteArrowNavigationContextType>(
    defaultAbsoluteArrowNavigationContext
  );
