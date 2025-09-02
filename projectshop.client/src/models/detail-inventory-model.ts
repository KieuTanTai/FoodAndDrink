import type{ InventoryModel } from "./inventory-model";
import type{ ProductModel } from "./product-model";

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