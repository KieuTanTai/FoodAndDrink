import type { InvoiceModel } from "@/models/InvoiceModel";
import type { SaleEventModel } from "@/models/SaleEventModel";

export interface InvoiceDiscountKey {
  invoiceId: number;
  saleEventId: number;
}

export interface InvoiceDiscountModel {
  invoiceId: number;
  saleEventId: number;
  saleEvent: SaleEventModel | null;
  invoice: InvoiceModel | null;
}
