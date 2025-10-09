import type{ InvoiceModel } from "./invoice-model";
import type{ SaleEventModel } from "./sale-event-model";

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