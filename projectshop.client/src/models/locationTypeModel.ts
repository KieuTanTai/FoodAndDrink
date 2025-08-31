import { LocationModel } from "./locationModel";

export interface LocationTypeModel {
    locationTypeId: number;
    locationTypeName: string;
    locationTypeStatus: boolean;
    locations: LocationModel[] | [];
}