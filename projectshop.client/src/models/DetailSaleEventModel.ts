import type{ SaleEventModel } from './sale-event-model';
import type{ EDiscountType } from './../enums/e-discount-type';
import type{ ProductModel } from './product-model';
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