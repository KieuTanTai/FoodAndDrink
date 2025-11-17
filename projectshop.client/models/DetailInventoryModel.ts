import type { InventoryModel } from "@/models/InventoryModel";
import type { ProductModel } from "@/models/ProductModel";

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
