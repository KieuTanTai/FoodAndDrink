import type { LocationTypeModel } from './location-type-model';
import type { LocationDistrictModel } from "./location-district-model";
import type { LocationWardModel } from "./location-ward-model";
import type { LocationCityModel } from './location-city-model';
import type { InventoryModel } from './inventory-model';
import type { InventoryMovementModel } from './inventory-movement-model';
import type { DisposeProductModel } from './dispose-product-model';

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
    inventory: InventoryModel | null;
    sourceInventoryMovements: InventoryMovementModel[] | [];
    destinationInventoryMovements: InventoryMovementModel[];
    disposeProducts: DisposeProductModel[] | [];
}