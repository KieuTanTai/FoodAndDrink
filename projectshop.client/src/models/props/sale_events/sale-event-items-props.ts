import type { SaleEventItemProps } from "./sale-event-item-props";

export interface SaleEventItemsProps {
  isHaveSideEvent?: boolean;
  mainSaleEvents?: SaleEventItemProps[];
  sideSaleEvents?: SaleEventItemProps[];  
}

export const defaultSaleEventItemsProps: SaleEventItemsProps = {
  isHaveSideEvent: false,
  mainSaleEvents: [],
  sideSaleEvents: [],
};