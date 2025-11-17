import type { SaleEventModel } from "@/models/SaleEventModel";
import type { EDiscountType } from "@/enums/EDiscountType";
import type { ProductModel } from "@/models/ProductModel";
export interface DetailSaleEventModel {
  detailSaleEventId: number;
  saleEventId: number;
  productBarcode: string;
  discountType: EDiscountType;
  discountPercent: number;
  discountAmount: number;
  maxDiscountPrice: number;
  minPriceToUse: number;
  saleEvent: SaleEventModel | null;
  product: ProductModel | null;
}
