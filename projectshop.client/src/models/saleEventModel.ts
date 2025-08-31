import { DetailSaleEventModel } from "./detailSaleEventModel";
import { InvoiceDiscountModel } from "./invoiceDiscountModel";

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