import type { EInventoryMovementReason } from "@/enums/EInventoryMovementReason";
import type { LocationModel } from "@/models/LocationModel";
export interface InventoryMovementModel {
  inventoryMovementId: number;
  sourceLocationId: number;
  destinationLocationId: number;
  inventoryMovementQuantity: number;
  inventoryMovementDate: string;
  inventoryMovementReason: EInventoryMovementReason | null;
  sourceLocation: LocationModel | null;
  destinationLocation: LocationModel | null;
}
