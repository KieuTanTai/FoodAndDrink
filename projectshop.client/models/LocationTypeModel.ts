import type { LocationModel } from "@/models/LocationModel";

export interface LocationTypeModel {
  locationTypeId: number;
  locationTypeName: string;
  locationTypeStatus: boolean;
  locations: LocationModel[] | [];
}
