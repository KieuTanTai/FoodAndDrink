import type { LocationModel } from "./location-model";

export interface LocationTypeModel {
    locationTypeId: number;
    locationTypeName: string;
    locationTypeStatus: boolean;
    locations: LocationModel[] | [];
}