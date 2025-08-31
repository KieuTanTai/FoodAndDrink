import { DetailInventoryModel } from "./detailInventoryModel";
import { InventoryModel } from "./inventoryModel";

export interface ProductLotModel {
    productLotId: number;
    inventoryId: number;
    productLotCreatedDate: string;
    inventories: InventoryModel[] | [];
    detailInventoryMovements: DetailInventoryModel[] | [];
}