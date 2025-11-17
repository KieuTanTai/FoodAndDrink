import type { DetailInventoryModel } from "@/models/DetailInventoryModel";
import type { InventoryModel } from "@/models/InventoryModel";

export interface ProductLotModel {
  productLotId: number;
  inventoryId: number;
  productLotCreatedDate: string;
  inventories: InventoryModel[] | [];
  detailInventoryMovements: DetailInventoryModel[] | [];
}
