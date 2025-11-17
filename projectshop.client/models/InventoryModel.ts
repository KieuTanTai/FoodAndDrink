import type { LocationModel } from "@/models/LocationModel";
export interface InventoryModel {
  inventoryId: number;
  locationId: number;
  inventoryStatus: number;
  inventoryLastUpdatedDate: string;
  location: LocationModel | null;
}
