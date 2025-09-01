import type{ InvoiceModel } from "./invoiceModel";
import type{ SaleEventModel } from "./saleEventModel";

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