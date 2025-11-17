import type { InventoryMovementModel } from "@/models/InventoryMovementModel";
import type { ProductLotModel } from "@/models/ProductLotModel";
import type { ProductModel } from "@/models/ProductModel";

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
