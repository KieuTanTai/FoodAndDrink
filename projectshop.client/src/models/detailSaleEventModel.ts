import type{ SaleEventModel } from './saleEventModel';
import type{ EDiscountType } from './../enums/eDiscountType';
import type{ ProductModel } from './productModel';
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