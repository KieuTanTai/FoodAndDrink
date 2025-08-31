import { SaleEventModel } from './saleEventModel';
import { EDiscountType } from './../Enums/eDiscountType';
import { ProductModel } from './productModel';
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