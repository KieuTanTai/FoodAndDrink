import type { DetailInventoryModel } from "./detail-inventory-model";
import type { InventoryModel } from "./inventory-model";

export interface ProductLotModel {
    productLotId: number;
    inventoryId: number;
    productLotCreatedDate: string;
    inventories: InventoryModel[] | [];
    detailInventoryMovements: DetailInventoryModel[] | [];
}