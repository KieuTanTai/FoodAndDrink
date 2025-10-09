import type{ InventoryMovementModel } from './inventory-movement-model';
import type{ ProductLotModel } from './product-lot-model';
import type{ ProductModel } from "./product-model";

export interface DetailInventoryMovementModel {
    detailInventoryMovementId: number;
    inventoryMovementId: number;
    productBarcode: string;
    productLotId: number;
    detailInventoryMovementQuantity: number;
    product: ProductModel | null;
    inventoryMovement: InventoryMovementModel | null;
    productLot: ProductLotModel | null;
}