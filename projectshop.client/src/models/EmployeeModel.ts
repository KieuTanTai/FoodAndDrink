import type{ LocationCityModel } from "./location-city-model";
import type{ LocationDistrictModel } from "./location-district-model";
import type{ LocationModel } from "./location-model";
import type{ LocationWardModel } from "./location-ward-model";
import type{ PersonModel } from "./person-model";

export interface EmployeeModel extends PersonModel {
    employeeId: number;
    employeeHouseNumber: string;
    employeeStreet: string;
    employeeWardId: number | null;
    employeeDistrictId: number | null;
    employeeCityId: number | null;
    locationId: number;
    employeeWard: LocationWardModel | null;
    employeeDistrict: LocationDistrictModel | null;
    employeeCity: LocationCityModel | null;
    location: LocationModel | null;
}