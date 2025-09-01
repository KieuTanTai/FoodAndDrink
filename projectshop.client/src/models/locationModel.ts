import type { LocationTypeModel } from './locationTypeModel';
import type { LocationDistrictModel } from "./locationDistrictModel";
import type { LocationWardModel } from "./locationWardModel";
import type { LocationCityModel } from './locationCityModel';
import type { InventoryModel } from './inventoryModel';
import type { InventoryMovementModel } from './inventoryMovementModel';
import type { DisposeProductModel } from './disposeProductModel';

export interface LocationModel {
    locationId: number;
    locationTypeId: number;
    locationHouseNumber: string;
    locationStreet: string;
    locationWardId: number;
    locationDistrictId: number;
    locationCityId: number;
    locationPhone: string;
    locationEmail: string;
    locationName: string;
    locationStatus: boolean;
    locationDistrict: LocationDistrictModel | null;
    locationWard: LocationWardModel | null;
    locationType: LocationTypeModel | null;
    locationCity: LocationCityModel | null;
    inventories: InventoryModel[] | [];
    sourceInventoryMovements: InventoryMovementModel[] | [];
    destinationInventoryMovements: InventoryMovementModel[];
    disposeProducts: DisposeProductModel[] | [];
}