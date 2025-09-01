import type{ InventoryMovementModel } from './inventoryMovementModel';
import type{ ProductLotModel } from './productLotModel';
import type{ ProductModel } from "./productModel";

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