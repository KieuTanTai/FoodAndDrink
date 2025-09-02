import type { DetailSaleEventModel } from "./detail-sale-event-model";
import type { InvoiceDiscountModel } from "./invoice-discount-model";

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