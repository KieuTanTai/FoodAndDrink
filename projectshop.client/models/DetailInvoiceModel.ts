import type { InvoiceModel } from "@/models/InvoiceModel";
import type { ProductModel } from "@/models/ProductModel";

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
