import type{ InvoiceModel } from "./invoiceModel";
import type{ ProductModel } from "./productModel";

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