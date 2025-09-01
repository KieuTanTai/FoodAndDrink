import type{ LocationCityModel } from "./locationCityModel";
import type{ LocationDistrictModel } from "./locationDistrictModel";
import type{ LocationModel } from "./locationModel";
import type{ LocationWardModel } from "./locationWardModel";
import type{ PersonModel } from "./personModel";

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