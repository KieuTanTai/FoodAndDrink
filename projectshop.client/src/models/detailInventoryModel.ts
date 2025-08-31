import { InventoryModel } from "./inventoryModel";
import { ProductModel } from "./productModel";

export interface DetailInventoryModel {
    detailInventoryId: number;
    inventoryId: number;
    productBarcode: string;
    detailInventoryQuantity: number;
    detailInventoryAddedDate: string;
    detailInventoryLastUpdatedDate: string;
    product: ProductModel | null;
    inventory: InventoryModel | null;
}