import type{ ProductLotModel } from "./product-lot-model";
import type{ ProductModel } from "./product-model";

export interface DetailProductLotKey {
    productLotId: number;
    productBarcode: string;
}

export interface DetailProductLotModel {
    productLotId: number;
    productBarcode: string;
    productLotMfgDate: string;
    productLotExpDate: string;
    productLotInitialQuantity: number;
    product: ProductModel | null;
    productLot: ProductLotModel | null;
}