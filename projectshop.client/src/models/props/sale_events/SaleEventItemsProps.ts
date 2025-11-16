import type { SaleEventItemProps } from "./sale-event-item-props";

export interface SaleEventItemsProps {
  saleEventItems: SaleEventItemProps[];  
}

export const defaultSaleEventItemsProps: SaleEventItemsProps = {
  saleEventItems: [],
};