import type{ EInventoryMovementReason } from './../enums/e-inventory-movement-reason';
import type{ LocationModel } from './location-model';
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