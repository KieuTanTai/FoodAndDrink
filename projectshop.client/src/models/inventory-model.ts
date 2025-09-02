import type{ LocationModel } from './location-model';
export interface InventoryModel {
    inventoryId: number;
    locationId: number;
    inventoryStatus: number;
    inventoryLastUpdatedDate: string;
    location: LocationModel | null;
}