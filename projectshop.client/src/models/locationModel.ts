import { LocationTypeModel } from './locationTypeModel';
import { LocationDistrictModel } from "./locationDistrictModel";
import { LocationWardModel } from "./locationWardModel";
import { LocationCityModel } from './locationCityModel';
import { InventoryModel } from './inventoryModel';
import { InventoryMovementModel } from './inventoryMovementModel';
import { DisposeProductModel } from './disposeProductModel';

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