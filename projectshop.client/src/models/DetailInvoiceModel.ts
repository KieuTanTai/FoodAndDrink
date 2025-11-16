import type{ InvoiceModel } from "./invoice-model";
import type{ ProductModel } from "./product-model";

export interface DetailInvoiceModel {
    detailInvoiceId: number;
    invoiceId: number;
    productBarcode: string;
    detailInvoiceQuantity: number;
    detailInvoicePrice: number;
    detailInvoiceStatus: boolean;
    invoice: InvoiceModel | null;
    product: ProductModel | null;
}