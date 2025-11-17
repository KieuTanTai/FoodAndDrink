import type { ProductLotModel } from "@/models/ProductLotModel";
import type { ProductModel } from "@/models/ProductModel";

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
