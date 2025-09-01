import type{ InventoryModel } from "./inventoryModel";
import type{ ProductModel } from "./productModel";

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