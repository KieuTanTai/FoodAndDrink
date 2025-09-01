import type { DetailInventoryModel } from "./detailInventoryModel";
import type { InventoryModel } from "./inventoryModel";

export interface ProductLotModel {
    productLotId: number;
    inventoryId: number;
    productLotCreatedDate: string;
    inventories: InventoryModel[] | [];
    detailInventoryMovements: DetailInventoryModel[] | [];
}