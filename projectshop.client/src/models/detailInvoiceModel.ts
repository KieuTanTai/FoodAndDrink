import { InvoiceModel } from "./invoiceModel";
import { ProductModel } from "./productModel";

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