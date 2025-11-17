import type { DetailSaleEventModel } from "@/models/DetailSaleEventModel";
import type { InvoiceDiscountModel } from "@/models/InvoiceDiscountModel";

export interface SaleEventModel {
  saleEventId: number;
  saleEventStartDate: string;
  saleEventEndDate: string;
  saleEventName: string;
  saleEventStatus: boolean;
  saleEventDescription: string;
  saleEventDiscountCode: string;
  detailSaleEvents: DetailSaleEventModel[] | [];
  invoiceDiscounts: InvoiceDiscountModel[] | [];
}
