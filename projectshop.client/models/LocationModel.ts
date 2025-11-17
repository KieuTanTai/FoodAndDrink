import type { LocationTypeModel } from "@/models/LocationTypeModel";
import type { LocationDistrictModel } from "@/models/LocationDistrictModel";
import type { LocationWardModel } from "@/models/LocationWardModel";
import type { LocationCityModel } from "@/models/LocationCityModel";
import type { InventoryModel } from "@/models/InventoryModel";
import type { InventoryMovementModel } from "@/models/InventoryMovementModel";
import type { DisposeProductModel } from "@/models/DisposeProductModel";

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
