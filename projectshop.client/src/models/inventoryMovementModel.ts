import type{ EInventoryMovementReason } from './../enums/eInventoryMovementReason';
import type{ LocationModel } from './locationModel';
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