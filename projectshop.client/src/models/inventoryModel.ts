import { LocationModel } from './locationModel';
export interface InventoryModel {
    inventoryId: number;
    locationId: number;
    inventoryStatus: number;
    inventoryLastUpdatedDate: string;
    location: LocationModel | null;
}