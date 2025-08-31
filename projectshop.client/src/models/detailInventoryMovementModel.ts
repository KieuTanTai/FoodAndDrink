import { InventoryMovementModel } from './inventoryMovementModel';
import { ProductLotModel } from './productLotModel';
import { ProductModel } from "./productModel";

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