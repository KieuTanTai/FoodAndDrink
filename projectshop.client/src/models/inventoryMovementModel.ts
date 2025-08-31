import { EInventoryMovementReason } from './../Enums/eInventoryMovementReason';
import { LocationModel } from './locationModel';
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